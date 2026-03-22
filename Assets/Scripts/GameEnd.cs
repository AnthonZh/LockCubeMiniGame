using UnityEngine;

public class GameEnd : MonoBehaviour
{
    public GameStateManager StateManager;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            StateManager.Win();
        }        
    }
}
