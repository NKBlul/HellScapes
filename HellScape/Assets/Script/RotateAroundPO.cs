using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundPO : MonoBehaviour
{
    Transform parent;

    private void Start()
    {
        parent = GetComponentInParent<Player>().transform;
        Debug.Log(parent.gameObject.name);
    }

    void Update()
    {
        //Rotate1();
        Rotate2();
    }

    private void Rotate1()
    {
        // Calculate the desired position in a circular orbit
        Vector3 offset = new Vector2(Mathf.Cos(Time.time * 5), Mathf.Sin(Time.time * 5));
        transform.position = parent.position + offset;
        
        // Adjust the rotation based on the orbit
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void Rotate2()
    {
        // Calculate the rotation angle based on time and rotation speed
        float angle = Time.time * 5;

        // Calculate the position of the orbiting object around the center object
        float x = parent.position.x + Mathf.Cos(angle) * 2f; // You can adjust the radius (2f in this example)
        float y = parent.position.y + Mathf.Sin(angle) * 2f; // You can adjust the radius (2f in this example)

        // Set the position of the orbiting object
        transform.position = new Vector3(x, y, transform.position.z);
    }
}
