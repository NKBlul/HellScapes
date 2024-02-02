using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : BasePowerup
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
