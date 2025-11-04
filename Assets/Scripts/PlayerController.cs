


using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
   
    public float speed;                      // Movement speed
    private Rigidbody rb;                     // Player Rigidbody
    private float movementX;                  // Horizontal input
    private float movementY;                  // Vertical input

    //audio
    public AudioSource audioSource;
    public AudioClip bomb;
    public AudioClip CrowCaw;

    //particles
    public GameObject bombExplosionEffect;
    public GameObject collectableparticles;

 
   

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

   //called automatically when input action occured and the coverts it to vector 
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    //creates a direction vector and applies force on player rb to move it 
    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);

    }

  // This function is called automatically when this GameObject's collider enters a trigger collider 
    void OnTriggerEnter(Collider other)
    {
        // This checks the tag of the GameObject the collider belongs to
        if (other.CompareTag("Pickup"))
        {
            //try to get the Renderer component from the pickup object
            var renderer = other.GetComponent<Renderer>();
            if (renderer != null)
            {
                // Get the color of the pickup
                Color pickupColor = renderer.material.color;

                //call handlepickup function from gamemanager
                GameManager.Instance.HandlePickup(pickupColor);

            
            }
            //Creates a new instance of the collectableparticles prefab at the position of the pickup
            GameObject collection = Instantiate(collectableparticles, other.transform.position, Quaternion.identity);

            // Get the ParticleSystem component and play it
            ParticleSystem ps = collection.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                ps.Play();
            }
            //deactivate the collectable object
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


        
            // Define the push direction  (backward + slight upward)
            Vector3 pushDirection = new Vector3(0, 0.5f, -1f).normalized; 


            // Apply strong impulse force
            float pushStrength = 9f; // stronger push
            rb.AddForce(pushDirection * pushStrength, ForceMode.Impulse);
            

            // Call the GameManager function to reduce scorez
            GameManager.Instance.ReduceScore();

            //play a sound or visual effect
            audioSource.PlayOneShot(bomb);

            // Deactivate the bomb object
            other.gameObject.SetActive(false);
        }
        else if (other.CompareTag("Enemy"))
        {
            // Play sound for enemy collision
            audioSource.PlayOneShot(CrowCaw); 

            // Reduce score
            GameManager.Instance.ReduceScore();

           
        }
    }
}
