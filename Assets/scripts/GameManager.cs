using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{   
    bool gameHasEnded = false;

    public float restartDelay = 1f;

    public GameObject completeLevelUI;

    public PlayerMovement playerMovement; // Reference to the PlayerMovement script

    public void completeLevel() {
        Debug.Log("LEVEL 1");
        completeLevelUI.SetActive(true);
    }

    // Start is called before the first frame update
    public void EndGame () 
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("GAME OVER");
            Invoke("Restart", restartDelay);
        }
    }

    void Restart() {
        // Reset jump status before restarting the level
        if (playerMovement != null)
        {
            playerMovement.ResetJumpStatus();
        }
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}