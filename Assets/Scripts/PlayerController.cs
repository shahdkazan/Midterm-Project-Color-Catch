


using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    public float speed = 5f;
    private Rigidbody rb;
    public AudioSource audioSource;
    public AudioClip correctSound;
    public AudioClip wrongSound;
    public AudioClip bomb;

    public GameObject bombExplosionEffect;
    public GameObject collectableparticles;

    private bool canMove = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); 
            return;
        }

        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        if (!canMove) return;
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveX, 0, moveZ);
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    public void StopMove()
    {
        canMove = false;
        rb.linearVelocity = Vector3.zero;       
        rb.angularVelocity = Vector3.zero; 
    }

    Color lastPickupColor;





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
            GameObject collection = Instantiate(collectableparticles, other.transform.position, Quaternion.identity);

            // Get the ParticleSystem component and play it
            ParticleSystem ps = collection.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                ps.Play();
            }

            other.gameObject.SetActive(false);
        }
        else if (other.CompareTag("Bomb"))
        {
            // Spawn particle effect at bomb position
            GameObject explosion = Instantiate(bombExplosionEffect, other.transform.position, Quaternion.identity);

            // Get the ParticleSystem component and play it
            ParticleSystem ps = explosion.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                ps.Play();
            }

            
            //Calculate push direction opposite to where the player is facing
           Vector3 pushDirection = -transform.forward;
            pushDirection.y = 0.5f; // slight upward lift

            // Apply strong impulse force
            float pushStrength = 9f; // stronger push
            rb.AddForce(pushDirection * pushStrength, ForceMode.Impulse);
            


            // Call the GameManager function to reduce scorez
            GameManager.Instance.ReduceScore();

            //play a sound or visual effect
            audioSource.PlayOneShot(bomb);

            // Deactivate or destroy the bomb object
            other.gameObject.SetActive(false);
        }
    }
}
