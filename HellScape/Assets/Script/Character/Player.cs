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
    public GameObject bulletPrefab;
    public Transform aim;
    public Transform gunShoot;
    public Transform gun;
    public float offset;

    [Header("Dodge: ")]
    private bool dodge = false;
    private float dodgeSpeed = 7f;

    private void Awake()
    {
        inputActions = new Inputs();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        maxHp = 10f;
        moveSpeed = 5f;
        damage = 3f;

        currentHp = maxHp;
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
        if (inputActions.Player.Shoot.triggered)
        {
            ShootBulletAmount(3);
        }
    }

    private void PauseGame()
    {
        if (inputActions.Player.Pause.triggered)
        {
            if (!Pause.instance.isPause)
            {
                Pause.instance.PauseGame();

                inputActions.Player.Movement.Disable();
                inputActions.Player.Mouse.Disable();
                inputActions.Player.Dodge.Disable();
                inputActions.Player.Shoot.Disable();
                inputActions.Player.Pause.Enable();
            }
            else
            {
                Pause.instance.ContinueGame();
                inputActions.Player.Movement.Enable();
                inputActions.Player.Mouse.Enable();
                inputActions.Player.Dodge.Enable();
                inputActions.Player.Shoot.Enable();
                inputActions.Player.Pause.Enable();
            }
        }
    }

    private void ShootBulletAmount(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            // Calculate rotation based on the player's gunShoot rotation
            Quaternion bulletRotation = gunShoot.rotation;

            // Adjust the rotation based on the loop index and total bullet count
            float angleOffset = (amount - 1) * 22.5f; // Half of the total angle spread
            float angle = (i * 45f) - angleOffset;
            bulletRotation *= Quaternion.Euler(0f, 0f, angle);

            // Instantiate the bullet with the calculated rotation
            GameObject bullet = Instantiate(bulletPrefab, gunShoot.position, bulletRotation);
            Destroy(bullet, 2.0f);
        }
    }

    private void Dodge()
    {
        if (inputActions.Player.Dodge.triggered && !dodge)
        {
            dodge = true;
            col.enabled = false;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = moveInput * moveSpeed;
        if (dodge)
        {
            rb.velocity = mouseInput * dodgeSpeed;
        }
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
        dodge = false;
        col.enabled = true;
        rb.velocity = Vector2.zero;
    }

    public void FinishHit()
    {
        animator.SetBool("Hit", false);
        col.enabled = true;
    }
    #endregion
}
