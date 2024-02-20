using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMultiplier : BasePowerup
{
    protected override void Awake()
    {
        base.Awake();
    }

    public override void ActivatePowerup()
    {
        ScoreManager.multiplier++;
        base.ActivatePowerup();
    }
}
