using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Troll : BaseEnemy
{
    private bool isRegenHp;
    [SerializeField]private float regenCooldown = 2f;
    [SerializeField] GameObject healPopup;
    public ParticleSystem regenParticle;
    int regenAmount = 2;
    private float lastRegenTime;

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

        if (currentHp != maxHp && isRegenHp && Time.time - lastRegenTime >= regenCooldown)
        {
            RegenHp();
            lastRegenTime = Time.time;
        }
    }

    private void RegenHp()
    {
        AudioManager.instance.PlaySFX("Troll_Regen");
        regenParticle.Play();
        ShowHealText(regenAmount);
        currentHp += regenAmount;
        currentHp = Mathf.Min(currentHp, maxHp);
        if (currentHp == maxHp)
        {
            isRegenHp = false;
        }
    }

    private void ShowHealText(float healText)
    {
        GameObject popup = Instantiate(healPopup, transform.position, Quaternion.identity);
        popup.GetComponentInChildren<TextMeshPro>().text = "+" + healText.ToString();
        Destroy(popup, 0.7f);
    }

    protected override void Die()
    {
        AudioManager.instance.PlaySFX("Troll_Hit");

        base.Die();
    }
}
