using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : BaseCharacter
{
    [Header("Inputs:")]
    Inputs inputActions;
    [SerializeField] private Vector2 moveInput;
    [SerializeField] private Vector2 mouseInput;

    [Header("Shoot: ")]
    public Transform aim;
    public Transform gunShoot;
    public Transform gun;
    public GameObject muzzle;
    public float offset;
    public float fireRate = 0.5f; // Adjust this value for the desired fire rate
    private float nextFireTime;
    public int numOfBullet = 1;

    [Header("Dodge: ")]
    private bool dodge = false;
    private float dodgeSpeed = 7f;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] LayerMask nothing;

    ParticleController particleController;

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
        if (inputActions.Player.Shoot.IsPressed() && !dodge && Time.time >= nextFireTime)
        {
            AudioManager.instance.PlaySFX("Shoot");
            muzzle.gameObject.SetActive(true);
            ShootBulletAmount(numOfBullet);
            StartCoroutine(DisableMuzzle());
        }
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
        if (amount % 2 != 0) // odd
        {
            for (int i = 0; i < amount; i++) //spread shot
            {
                GameObject bullet = ObjectPoolManager.instance.GetPooledObject();

                // Calculate rotation based on the player's gunShoot rotation
                Quaternion bulletRotation = gunShoot.rotation;

                // Adjust the rotation based on the loop index and total bullet count
                float angleOffset = (amount - 1) * 22.5f; // Half of the total angle spread
                float angle = (i * 45f) - angleOffset;
                bulletRotation *= Quaternion.Euler(0f, 0f, angle);

                // Instantiate the bullet with the calculated rotation 
                if (bullet != null)
                {
                    bullet.transform.position = gunShoot.position;
                    bullet.transform.rotation = bulletRotation;
                    bullet.SetActive(true);
                    StartCoroutine(ReturnBulletToPoolAfter(bullet, 2f));
                }
            }
        }
        else //even
        {
            for (int i = 0; i < amount; i++)
            {
                // Instantiate a new bullet for each iteration
                GameObject bullet = ObjectPoolManager.instance.GetPooledObject();

                Vector2 newBulletStartPos = new Vector2(gunShoot.position.x, gunShoot.position.y + 0.15f);
                Vector2 newBulletPos = new Vector2(gunShoot.position.x, newBulletStartPos.y - 0.3f * i);

                // Instantiate the bullet with the calculated position
                if (bullet != null)
                {
                    bullet.transform.position = newBulletPos;
                    bullet.transform.rotation = gunShoot.rotation;
                    bullet.SetActive(true);
                    StartCoroutine(ReturnBulletToPoolAfter(bullet, 2f));
                }
            }
        }
        nextFireTime = Time.time + 1f / fireRate; // Calculate next allowed fire time based on fire rate (higher fireRate faster shoot)
    }

    private IEnumerator ReturnBulletToPoolAfter(GameObject bullet, float time)
    {
        yield return new WaitForSeconds(time);
        if (bullet != null && bullet.activeSelf)
        {
            bullet.SetActive(false);
        }
    }

    private void Dodge()
    {
        if (inputActions.Player.Dodge.triggered && !dodge)
        {
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
            AudioManager.instance.PlayMusic("Lose");
            col.enabled = false;
            aim.gameObject.SetActive(false);
            Timer.instance.isCounting = false;        
            PlayDeadAnim();
            SetAllEnemySpeed(0f);
            OnDisable();         
        }
    }

    public void OnDie()
    {
        UIManager.Instance.losePanel.SetActive(true);
        UIManager.Instance.loseScore.text = $"Score: {ScoreManager.score.ToString()}";
        UIManager.Instance.loseTimer.text = UIManager.Instance.timerText.text;
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
}
