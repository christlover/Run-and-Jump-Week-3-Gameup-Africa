using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb; //Get Character Rigid Body

    public float jumpForce; //Control Jump Force From Inspector 
    public float gravityController; //Control Gravity From Inspector

    public bool isOnGround = true; //Check is Player is on The Ground

    public bool gameOver = false; //Has game ended?

    private Animator playerAnim; //Control Player Animations

    public ParticleSystem explosionParticle; //Control Explosion Particles
    public ParticleSystem dirtParticle; //Control Dirt Particles

    public AudioClip jumpSound;
    public AudioClip crashSound;

    private AudioSource playerAudio;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityController;

        playerAnim = GetComponent<Animator>();

        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver) { //Jump when space is pressed
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig"); //Disable jump if player hits obstacle

            dirtParticle.Stop(); //Stop dirt particle animation when player dies

            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
    }

    private void OnCollisionEnter(Collision collision) { //Function to contol boolean
        if (collision.gameObject.CompareTag("Ground")) { //If player touches ground, continue game
            isOnGround = true;
            dirtParticle.Play(); //Play dirt particle animation when player is alive
        } else if (collision.gameObject.CompareTag("Obstacle")) { //If player touches obstacle, end game
            gameOver = true;
            Debug.Log("Game Over!");

            playerAnim.SetBool("Death_b", true); //Play death animation if obstacle is hit
            playerAnim.SetInteger("DeathType_int", 1); //Play death animation if obstacle is hit

            explosionParticle.Play(); //Play explosion particle animation when player dies
            dirtParticle.Stop(); //Stop dirt particle animation when player dies

            playerAudio.PlayOneShot(crashSound, 1.0f);
        }
    }
}
