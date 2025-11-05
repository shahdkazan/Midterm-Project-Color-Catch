using UnityEngine;

public class Rotator : MonoBehaviour
{
   

    //Rotate the object around its local Y-axis 
    void Update()
    {
        transform.Rotate(new Vector3(0, 50 , 0) * Time.deltaTime );
    }
}