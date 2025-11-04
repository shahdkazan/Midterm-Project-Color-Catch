


using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    public float speed = 5f;
    private Rigidbody rb;
    private float movementX;
    private float movementY;


    public AudioSource audioSource;
    public AudioClip correctSound;
    public AudioClip wrongSound;
    public AudioClip bomb;
    public AudioClip CrowCaw;

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
    //void Update()
    //{
    //    movementX = Input.GetAxis("Horizontal");
    //    movementY = Input.GetAxis("Vertical");
    //}
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }
    void FixedUpdate()
    {
        //if (!canMove) return;
        //float moveX = Input.GetAxis("Horizontal");
        //float moveZ = Input.GetAxis("Vertical");

        //Vector3 movement = new Vector3(moveX, 0, moveZ);
        //rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        //----------------
        //if (!canMove) return;

        //float moveX = Input.GetAxis("Horizontal");
        //float moveZ = Input.GetAxis("Vertical");

        //Vector3 movement = new Vector3(moveX, 0, moveZ).normalized;

        //rb.AddForce(movement * speed, ForceMode.Force);
        //----------------
        Vector3 movement = new Vector3(movementX, 0f, movementY).normalized;
        rb.AddForce(movement * speed, ForceMode.Force);
    }

    //public void StopMove()
    //{
    //    canMove = false;
    //    // stop current motion immediately (classic Rigidbody API)
    //    rb.linearVelocity = Vector3.zero;
    //    rb.angularVelocity = Vector3.zero;

    //    rb.linearDamping = 10f;      // kills residual sliding
    //    rb.angularDamping = 10f;

    //}


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
        else if (other.CompareTag("Enemy"))
        {
            // Play sound for enemy collision
            audioSource.PlayOneShot(CrowCaw); // Make sure you have an AudioClip called enemyHitSound

            // Reduce score
            GameManager.Instance.ReduceScore();

           
        }
    }
}
