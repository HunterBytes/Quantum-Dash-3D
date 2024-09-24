using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Import the SceneManager library

public class Menu : MonoBehaviour
{
    // Method to start the game (loads the next scene)
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Method to go to the Rule scene (index 5)
    public void GoToRules()
    {
        SceneManager.LoadScene(5); // Load the Rule scene using the scene index 5
    }
}
