using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraBullet : BasePowerup
{
    protected override void Awake()
    {
        base.Awake();
    }

    public override void ActivatePowerup()
    {
        player.numOfBullet++;
        base.ActivatePowerup();
    }
}
