using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraDamage : BasePowerup
{
    protected override void Awake()
    {
        base.Awake();
    }

    public override void ActivatePowerup()
    {
        Debug.Log($"{gameObject.name} powerup activated");
    }
}
