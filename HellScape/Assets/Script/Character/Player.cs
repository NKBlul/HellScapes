using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseCharacter
{
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
        
    }
}
