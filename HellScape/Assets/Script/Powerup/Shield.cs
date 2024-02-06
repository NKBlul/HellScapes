using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : BasePowerup
{
    [SerializeField] GameObject shieldPrefab;
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void ActivatePowerup()
    {
        base.ActivatePowerup();
    }
}
