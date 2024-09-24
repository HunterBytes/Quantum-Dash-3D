using UnityEngine;
using TMPro;  // Make sure you are using this namespace

public class Score : MonoBehaviour {
    public Transform player;
    public TextMeshProUGUI scoreText;  // This must be TextMeshProUGUI

    // Update is called once per frame
    void Update() {
         // Add an offset to ensure the score starts at a higher value (e.g., adding 5 to the player position)
        float scoreValue = player.position.z + 5;
        
        // Display the score, formatting it to remove decimals if needed
        scoreText.text = Mathf.Max(0, scoreValue).ToString("0");
    }
}
