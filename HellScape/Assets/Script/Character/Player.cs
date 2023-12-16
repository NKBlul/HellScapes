using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseCharacter
{
    [Header("Inputs:")]
    Inputs inputActions;
    [SerializeField] private Vector2 moveInput;
    [SerializeField] private Vector2 mouseInput;

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
    }

    private void Move()
    {
        moveInput = inputActions.Player.Movement.ReadValue<Vector2>();
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
