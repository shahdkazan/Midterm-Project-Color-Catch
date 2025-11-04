using UnityEngine;

public class Rotator : MonoBehaviour
{
    // Rotation speed multiplier
    public float speed = 5;

    //Rotate the object around its local Y-axis at a frame-rate-independent speed
    void Update()
    {
        transform.Rotate(new Vector3(0, 10, 0) * Time.deltaTime * speed);
    }
}