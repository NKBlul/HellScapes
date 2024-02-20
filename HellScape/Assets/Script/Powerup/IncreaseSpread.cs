using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseSpread : BasePowerup
{
    protected override void Awake()
    {
        base.Awake();
    }

    public override void ActivatePowerup()
    {
        player.IncreaseSpread(2);
        base.ActivatePowerup();
    }
}
