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

    public override void ActivatePowerup()
    {
        GameObject shield = ObjectPoolManager.instance.SpawnObject(shieldPrefab, player.transform);
        base.ActivatePowerup();
    }
}
