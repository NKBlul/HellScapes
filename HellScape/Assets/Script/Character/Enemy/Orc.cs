using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc : BaseEnemy
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        maxHp = 21f;
        moveSpeed = 3f;
        damage = 6f;

        currentHp = maxHp;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
