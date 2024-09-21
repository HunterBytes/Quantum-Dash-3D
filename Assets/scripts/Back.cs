using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Import the SceneManager library

public class Back : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Check if the "Back" key (or any other key you'd like) is pressed
        if (Input.GetKeyDown(KeyCode.Backspace)) // You can change this to any other key or button press
        {
            GoBackToMainMenu();
        }
    }

    // Method to go back to the main menu using the scene index
    public void GoBackToMainMenu()
    {
        SceneManager.LoadScene(0); // Load the main menu scene using the scene index 0
    }
}