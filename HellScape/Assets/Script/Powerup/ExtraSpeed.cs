using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraSpeed : BasePowerup
{
    protected override void Awake()
    {
        base.Awake();
    }

    public override void ActivatePowerup()
    {
        player.IncreaseSpeed(0.3f);
        base.ActivatePowerup();
    }
}
