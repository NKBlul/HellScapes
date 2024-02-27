using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundPO : MonoBehaviour
{
    Transform parent;
    float rotateSpeed = 5f;
    float offset = 0.5f;
    public int objectIndex;
    public float radius;

    private void Start()
    {
        parent = GetComponentInParent<Player>().transform;
    }

    private float posOffset()
    {
        return objectIndex * offset;
    }

    void Update()
    {
        Rotate();
    }

    //private void Rotate()
    //{
    //    // Calculate the rotation angle based on time and rotation speed
    //    float angle = Time.time * rotateSpeed;
    //
    //    // Calculate the position of the orbiting object around the center object
    //    float x = -parent.position.x + (Mathf.Cos(angle) * radius); // You can adjust the radius (2f in this example)
    //    float y = parent.position.y + (Mathf.Sin(angle) * radius); // You can adjust the radius (2f in this example)
    //
    //    // Set the position of the orbiting object
    //    // +x +y CCW Rotation
    //    // -x +y CW Rotation
    //    // +x -y CW Rotation
    //    // -x -y CCW Rotation
    //    transform.position = new Vector3(-x, y , transform.position.z);
    //}

    private void Rotate()
    {
        // Calculate the rotation angle based on time and rotation speed
        float angle = Time.time * rotateSpeed;

        // Calculate the position of the orbiting object around the center object
        float x = Mathf.Cos(angle + posOffset()) * radius;
        float y = Mathf.Sin(angle + posOffset()) * radius;

        // Set the position of the orbiting object
        // +x +y CCW Rotation
        // -x +y CW Rotation
        // +x -y CW Rotation
        // -x -y CCW Rotation
        transform.position = parent.position + new Vector3(-x, y, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<BaseEnemy>().TakeDamage(1.5f);
        }
    }
}
