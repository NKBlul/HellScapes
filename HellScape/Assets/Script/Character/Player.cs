using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Player : BaseCharacter
{
    [Header("Inputs:")]
    Inputs inputActions;
    [SerializeField] private Vector2 moveInput;
    [SerializeField] private Vector2 mouseInput;

    [Header("Shoot: ")]
    public GameObject bulletPrefab;
    public Transform aim;
    public Transform gunShoot;
    public Transform gun;
    public GameObject muzzle;
    public float offset;
    public float fireRate = 3f; // Adjust this value for the desired fire rate
    private float nextFireTime;
    public int numOfBullet = 1;
    public float bulletAngle;
    float bulletLiveTime = 2f;

    [Header("Dodge: ")]
    private bool dodge = false;
    private float dodgeSpeed = 7f;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] LayerMask nothing;

    ParticleController particleController;
    public int rotatingObjectIndex = -1;

    private void Awake()
    {
        inputActions = new Inputs();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        maxHp = 12f;
        moveSpeed = 5f;
        damage = 3f;

        currentHp = maxHp;

        particleController = GetComponentInChildren<ParticleController>();
    }


    // Update is called once per frame
    void Update()
    {
        Move();
        Mouse();
        Aim();
        Shoot();
        Dodge();
        SetAnimatorValue();
        PauseGame();
        if (IsAnimationFinished("Player_Dodge"))
        {
            FinishDodging();
        }
    }

    private void Move()
    {
        moveInput = inputActions.Player.Movement.ReadValue<Vector2>();
        if (moveInput.x != 0 || moveInput.y != 0)
        {
            float moveMagnitude = Mathf.Max(Mathf.Abs(moveInput.x), Mathf.Abs(moveInput.y));
            animator.SetFloat("Move", moveMagnitude);
        }
        else
        {
            animator.SetFloat("Move", 0);
        }
    }

    private void Mouse()
    {
        Vector2 mouseScreenPosition = inputActions.Player.Mouse.ReadValue<Vector2>();
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

        mouseInput = ((Vector2)mouseWorldPosition - rb.position).normalized;

        if (mouseInput.x > 0)
        {
            FlipScale(1);
        }
        else if (mouseInput.x < 0)
        {
            FlipScale(-1);
        }
    }

    private void FlipScale(float scale)
    {
        transform.localScale = new Vector3(scale, 1, 1);
        aim.localScale = new Vector3(scale, 1, 1);
        gun.localScale = new Vector3(1, scale, 1);
    }

    private void Aim()
    {
        Vector2 aimDir = aim.position - Camera.main.ScreenToWorldPoint(inputActions.Player.Mouse.ReadValue<Vector2>());
        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
        aim.eulerAngles = new Vector3(0, 0, angle + offset);
    }

    private void Shoot()
    {
        if (Time.time >= nextFireTime)
        {
            if (inputActions.Player.Shoot.IsPressed() && !dodge)
            {
                ShootBullet();
            }
        }   
    }

    private void ShootBullet()
    {
        AudioManager.instance.PlaySFX("Shoot");
        muzzle.gameObject.SetActive(true);
        ShootBulletAmount(numOfBullet);
        StartCoroutine(DisableMuzzle());
    }

    IEnumerator DisableMuzzle()
    {
        yield return new WaitForSeconds(0.5f);
        muzzle.gameObject.SetActive(false);
    }

    private void PauseGame()
    {
        if (inputActions.Player.Pause.triggered)
        {
            if (!Pause.instance.isPause)
            {
                Pause.instance.PauseGame();
                OnDisable();
                inputActions.Player.Pause.Enable();
            }
            else
            {
                Pause.instance.ContinueGame();
                OnEnable();
            }
        }
    }

    private void ShootBulletAmount(int amount)
    {
        if (amount != 0) // odd
        {
            for (int i = 0; i < amount; i++) //spread shot
            {
                GameObject bullet = ObjectPoolManager.instance.SpawnObject(bulletPrefab, gunShoot.position, Quaternion.identity);

                // Calculate rotation based on the player's gunShoot rotation
                Quaternion bulletRotation = gunShoot.rotation;
    
                // Adjust the rotation based on the loop index and total bullet count
                float angleOffset = (amount - 1) * (bulletAngle/2); // Half of the total angle spread
                float angle = (i * bulletAngle) - angleOffset;
                bulletRotation *= Quaternion.Euler(0f, 0f, angle);

                // Set the bullet's rotation
                bullet.transform.rotation = bulletRotation;

                ObjectPoolManager.instance.ReturnObjectToPool(bullet, bulletLiveTime);
            }
        }
        nextFireTime = Time.time + 1f / fireRate; // Calculate next allowed fire time based on fire rate (higher fireRate faster shoot)
    }

    private void Dodge()
    {
        if (inputActions.Player.Dodge.triggered && !dodge)
        {
            AudioManager.instance.PlaySFX("Player_Dodge");
            aim.gameObject.SetActive(false);
            dodge = true;
            col.excludeLayers = enemyLayer;
            particleController.PlayDodgeParticle();
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = moveInput * moveSpeed;
        if (dodge)
        {
            if (moveInput == Vector2.zero)
            {
                rb.velocity = new Vector2(transform.localScale.x, 0) * dodgeSpeed;
            }
            else
            {
                rb.velocity = moveInput * dodgeSpeed;
            }          
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            PlayerDead();
        }
    }

    private void PlayerDead()
    {
        AudioManager.instance.PlayMusic("Lose");
        col.enabled = false;
        aim.gameObject.SetActive(false);
        Timer.instance.isCounting = false;
        PlayDeadAnim();
        SetAllEnemySpeed(0f);
        OnDisable();
    }

    public void ChangePlayerSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    public void ResetPlayerSpeed()
    {
        moveSpeed = 5f;
    }

    public void OnDie()
    {
        UIManager.Instance.losePanel.SetActive(true);
        UIManager.Instance.loseScore.text = $"Score: {ScoreManager.score.ToString()}";
        UIManager.Instance.loseTimer.text = UIManager.Instance.timerText.text;
    }

    protected override void Die()
    {
        AudioManager.instance.PlayMusic("Lose");
        col.enabled = false;
        aim.gameObject.SetActive(false);
        Timer.instance.isCounting = false;
        base.Die();
        SetAllEnemySpeed(0f);
        OnDisable();
    }

    public void OnEnable()
    {
        inputActions.Player.Enable();
    }

    public void OnDisable() 
    {
        inputActions.Player.Disable();
    }

    #region animationHelperFunction
    private void SetAnimatorValue()
    {
        animator.SetBool("Dodge", dodge);
    }
    public void FinishDodging()
    {
        aim.gameObject.SetActive(true);
        dodge = false;
        col.excludeLayers = nothing;
        rb.velocity = Vector2.zero;
    }

    public void FinishHit()
    {
        animator.SetBool("Hit", false);
        col.enabled = true;
    }
    #endregion

    public void IncreaseSpeed(float increaseSpeed)
    {
        moveSpeed += increaseSpeed;
        Debug.Log(moveSpeed);
    }

    public void IncreaseDamage(float increaseDamage)
    {
        damage += increaseDamage;
        Debug.Log(damage);
    }

    public void IncreaseSpread(int increaseAngle)
    {
        bulletAngle += increaseAngle;
        Debug.Log(bulletAngle);
    }
}
