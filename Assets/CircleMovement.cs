using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMovement : MonoBehaviour
{
    public Vector3 centerPoint = Vector3.zero; // Center of the circle
    public float radius = 5f; // Radius of the circle
    public float speed = 2f; // Speed of movement

    private float angle = 0f;

    void Update()
    {
        // Calculate the target position on the circle
        float x = centerPoint.x + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        float y = centerPoint.y;
        float z = centerPoint.z + radius * Mathf.Sin(angle * Mathf.Deg2Rad);

        // Update the object's position
        transform.position = new Vector3(x, y, z);

        // Increment the angle based on speed
        angle += speed * Time.deltaTime;

        // Keep the angle within 360 degrees
        if (angle > 360f)
        {
            angle -= 360f;
        }
    }
}
