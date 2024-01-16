using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : BaseEnemy
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        maxHp = 6f;
        moveSpeed = 4f;
        damage = 1f;

        currentHp = maxHp;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();   
        
        if (IsAnimationFinished("Bat_Dead"))
        {
            UpdateScoreWhenDead(5);
            FinishDeadAnim();
        }
    }

    protected override void Die()
    {
        AudioManager.instance.PlaySFX("Bat_Hit");

        base.Die();
    }
}
