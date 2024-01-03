using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troll : BaseEnemy
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        maxHp = 9f;
        moveSpeed = 2f;
        damage = 2f;

        currentHp = maxHp;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
