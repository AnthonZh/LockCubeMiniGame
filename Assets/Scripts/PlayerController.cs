using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    public GameStateManager StateManager;
    public Rigidbody2D PlayerBody;
    public SpriteRenderer PlayerSprite;
    public Animator PlayerAnimator;
    public float speed = 5f;
    private bool grounded = false;

    [Header("Sound Effects")]
    public AudioSource PlayerRunning;
    public AudioSource PlayerSounds;
    public AudioClip RunClip;
    public AudioClip JumpClip;
    public AudioClip LandClip;

    void Update()
    {
        if(StateManager.GameOn) {
            //Handle Controls
            Vector2 move = InputSystem.actions["Move"].ReadValue<Vector2>() * speed;
            float moveX = move.x;
            bool jumped = InputSystem.actions["Jump"].WasPressedThisFrame();

            PlayerBody.linearVelocityX = moveX;

            //Flip Sprite if needed
            if(moveX < 0) PlayerSprite.flipX = true;
            else if (moveX > 0) PlayerSprite.flipX = false;

            //Handle Animations
            if(moveX == 0 || !grounded) {
                PlayerAnimator.SetBool("Running", false);
                PlayerRunning.Stop();
            }
            else
            {
                PlayerAnimator.SetBool("Running", true);
                // Loop run sound
                if(!PlayerRunning.isPlaying && grounded)
                    PlayerRunning.Play();
            }

            //Handle Jumping
            if(grounded && jumped)
            {
                PlayerAnimator.SetBool("Jumping", true);
                PlayerBody.AddForceY(15f, ForceMode2D.Impulse);
                grounded = false;
                PlayerRunning.Stop();
                PlayerSounds.PlayOneShot(JumpClip);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.collider.tag == "Ground")
        {
            grounded = true;
            PlayerAnimator.SetBool("Jumping", false);
            PlayerSounds.PlayOneShot(LandClip);
        }

        if(col.collider.tag == "Enemy")
        {
            PlayerSounds.Stop();
            PlayerRunning.Stop();
            StateManager.Reset();
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if(col.collider.tag == "Ground")
        {
            grounded = false;
            PlayerAnimator.SetBool("Jumping", true);
            PlayerRunning.Stop();
        }
    }

    public void SetGrounded(bool grounded)
    {
        this.grounded = grounded;
    }
}
