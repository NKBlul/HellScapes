using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : BaseEnemy
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        maxHp = 5f;
        moveSpeed = 5f;
        damage = 1f;

        currentHp = maxHp;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
