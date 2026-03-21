using UnityEditor.Callbacks;
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
    void Start()
    {
        
    }

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
            if(moveX == 0) PlayerAnimator.SetBool("Running", false);
            else PlayerAnimator.SetBool("Running", true);

            //Handle Jumping
            if(grounded && jumped)
            {
                PlayerAnimator.SetBool("Jumping", true);
                PlayerBody.AddForceY(7.5f, ForceMode2D.Impulse);
                grounded = false;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.collider.tag == "Ground")
        {
            grounded = true;
            PlayerAnimator.SetBool("Jumping", false);
        }

        if(col.collider.tag == "Enemy")
        {
            StateManager.Reset();
        }
    }
}
