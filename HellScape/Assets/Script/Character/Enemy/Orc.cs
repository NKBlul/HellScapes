using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc : BaseEnemy
{
    Spawner spawner;

    // Start is called before the first frame update
    protected override void Start()
    {
        spawner = GameObject.Find("Spawner").GetComponent<Spawner>();

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

        if (IsAnimationFinished("Orc_Dead"))
        {
            UpdateScoreWhenDead(10);
            SpawnEnemyOnDead(2);
            FinishDeadAnim();
        }
    }

    protected override void Die()
    {
        AudioManager.instance.PlaySFX("Orc_Hit");

        base.Die();
    }

    public void SpawnEnemyOnDead(int numOfMinion)
    {
        spawner.SpawnMinion(numOfMinion, transform.position);
        spawner.totalEnemyThisWave += numOfMinion;
    }
}
