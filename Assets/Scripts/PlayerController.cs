


using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody rb;
    public AudioSource audioSource;
    public AudioClip correctSound;
    public AudioClip wrongSound;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveX, 0, moveZ);
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    Color lastPickupColor;




    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Pickup"))
    //    {
    //        var renderer = other.GetComponent<Renderer>();
    //        if (renderer != null)
    //        {
    //            Color pickupColor = renderer.material.color;
    //            GameManager.Instance.HandlePickup(pickupColor);
    //        }

    //        other.gameObject.SetActive(false);
    //    }
    //}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            var renderer = other.GetComponent<Renderer>();
            if (renderer != null)
            {
                Color pickupColor = renderer.material.color;
                GameManager.Instance.HandlePickup(pickupColor);

                if (pickupColor == GameManager.Instance.targetColor)
                    audioSource.PlayOneShot(correctSound);
                else
                    audioSource.PlayOneShot(wrongSound);
            }

            other.gameObject.SetActive(false);
        }
    }
}
