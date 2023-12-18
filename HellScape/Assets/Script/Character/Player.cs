using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : BaseCharacter
{
    [Header("Inputs:")]
    Inputs inputActions;
    [SerializeField] private Vector2 moveInput;
    [SerializeField] private Vector2 mouseInput;

    [Header("Shoot")]
    public GameObject bulletPrefab;
    public Transform aim;
    public Transform gunShoot;
    public Transform gun;

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
    }

    private void Move()
    {
        moveInput = inputActions.Player.Movement.ReadValue<Vector2>().normalized;
    }

    private void Mouse()
    {
        mouseInput = Camera.main.ScreenToWorldPoint(inputActions.Player.Mouse.ReadValue<Vector2>());
        if (mouseInput.x > 0)
        {
            transform.localScale = new Vector3(1, 1 ,1);
            aim.localScale = new Vector3(1, 1, 1);
            gun.localScale = new Vector3(1, 1, 1);
        }
        else if (mouseInput.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            aim.localScale = new Vector3(-1, 1, 1);
            gun.localScale = new Vector3(1, -1, 1);
        }
    }

    private void Aim()
    {
        //aim.position = new Vector3(mouseInput.x - transform.position.x, mouseInput.y - transform.position.y, transform.position.z);
        Vector3 aimDir = new Vector3(mouseInput.x - transform.position.x, mouseInput.y - transform.position.y, transform.position.z).normalized;
        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
        aim.eulerAngles = new Vector3(0, 0, angle);
    }

    private void Shoot()
    {
        if (inputActions.Player.Shoot.IsPressed())
        {
            GameObject bullet = Instantiate(bulletPrefab, gunShoot.position, gunShoot.rotation);
            Destroy(bullet, 2.0f);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = moveInput * moveSpeed;
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
    }

    private void OnDisable() 
    {
        inputActions.Player.Disable();
    }
}
