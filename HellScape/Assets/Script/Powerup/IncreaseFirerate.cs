using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseFirerate : BasePowerup
{
    protected override void Awake()
    {
        base.Awake();
    }

    public override void ActivatePowerup()
    {
        player.fireRate += 0.3f;
        base.ActivatePowerup();
    }
}
