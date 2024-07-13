using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver;
    public float floatForce;
    private float gravityModifier = 1.5f;
    public float yRangeMax = 15F;
    public float yRangeMin = 2F;
    private Rigidbody playerRb;
    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;
    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();
        gameOver = false;
        // Apply a small upward force at the start of the game
        playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);

    }

    // Update is called once per frame
    void Update()
    {
        // While space is pressed and player is low enough, float up
        if (Input.GetKeyDown(KeyCode.Space) && !gameOver && transform.position.y <= yRangeMax)
        {
            playerRb.AddForce(Vector3.up * floatForce, ForceMode.Impulse);

            if(transform.position.y >= yRangeMax)
            {
                transform.Translate(0, yRangeMax - transform.position.y, 0);
                playerRb.velocity = Vector3.zero;
            }

            if(transform.position.y <= yRangeMin){
                transform.Translate(0, yRangeMin + transform.position.y, 0);
                playerRb.velocity = Vector3.zero;
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 0.6f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
        } 

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 0.6f);
            Destroy(other.gameObject);

        }

    }

}
