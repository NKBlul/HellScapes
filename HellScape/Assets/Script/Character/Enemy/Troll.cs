using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troll : BaseEnemy
{
    private bool isRegenHp;
    [SerializeField]private float regenCooldown = 2f;
    public ParticleSystem regenParticle;

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

        if (IsAnimationFinished("Troll_Dead"))
        {
            UpdateScoreWhenDead(5);
            FinishDeadAnim();
        }

        if (currentHp != maxHp && !isRegenHp)
        {
            StartCoroutine(RegenHp());        
        }
    }

    IEnumerator RegenHp()
    {
        isRegenHp = true;
        yield return new WaitForSeconds(regenCooldown);
        regenParticle.Play();
        currentHp += 2;
        currentHp = Mathf.Min(currentHp, maxHp);
        isRegenHp = false;
    }

    protected override void Die()
    {
        AudioManager.instance.PlaySFX("Troll_Hit");

        base.Die();
    }
}
