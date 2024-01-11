using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troll : BaseEnemy
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        maxHp = 8f;
        moveSpeed = 2f;
        damage = 2f;

        currentHp = maxHp;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void Die()
    {
        AudioManager.instance.PlaySFX("Troll_Hit");

        base.Die();
    }
}
