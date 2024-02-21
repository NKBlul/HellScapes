using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObject : BasePowerup
{
    public GameObject RotatingGO;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void ActivatePowerup()
    {
        ObjectPoolManager.instance.SpawnObject(RotatingGO, player.transform);
        base.ActivatePowerup();
    }
}
