using UnityEngine;

public class JumperScript : MonoBehaviour
{
    public Animator JumperAnimator;
    public PlayerController Player;
    public float force;
    public AudioSource BoingPlayer;
    public AudioClip Boing;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            JumperAnimator.SetTrigger("Jump");
            collision.collider.attachedRigidbody.AddForceY(force, ForceMode2D.Impulse);
            Player.SetGrounded(false);
            BoingPlayer.PlayOneShot(Boing);
        }   
    }
}
