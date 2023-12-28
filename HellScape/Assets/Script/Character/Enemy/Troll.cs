using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troll : BaseEnemy
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        maxHp = 10f;
        moveSpeed = 3f;
        damage = 1f;

        currentHp = maxHp;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
