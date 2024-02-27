using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObject : BasePowerup
{
    public GameObject RotatingGO;
    float radius = 2.5f; // You can adjust the radius

    protected override void Awake()
    {
        base.Awake();
    }

    public override void ActivatePowerup()
    {
        player.rotatingObjectIndex++;

        // Calculate the angle offset for each new rotating object
        float angleOffset = 45f * (player.rotatingObjectIndex - 1);

        // Convert the angle offset to radians
        float radians = Mathf.Deg2Rad * angleOffset;

        // Calculate the position offset based on the radius and angle
        float xOffset = Mathf.Cos(radians) * radius; // 2f is the radius
        float yOffset = Mathf.Sin(radians) * radius; // 2f is the radius

        // Spawn the rotating object with the calculated offset
        Vector3 spawnPosition = player.transform.position + new Vector3(xOffset, yOffset, 0f);
        GameObject rotatingGO = ObjectPoolManager.instance.SpawnObject(RotatingGO, spawnPosition, Quaternion.identity, player.transform);

        // Set the object index and update rotatingObjectIndex
        rotatingGO.GetComponent<RotateAroundPO>().objectIndex = player.rotatingObjectIndex;
        rotatingGO.GetComponent<RotateAroundPO>().radius = radius;

        base.ActivatePowerup();
    }
}
