using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievements : MonoBehaviour
{

    public class Achievement
    {
        public string name;
        public string description;
        public bool isObtained;
    }

    public Dictionary<string, Achievement> tracker = new Dictionary<string, Achievement> 
    {
        // insert achievements here, initialize to false
        {"Play the game!", new Achievement {name="Play the game!", description="TBD", isObtained=true }  },
        {"Vanquish a Slimmer!", new Achievement {name="Vaniquish a Slimmer!", description="TBD", isObtained=false }  },
        {"Test Achievement", new Achievement{name="Test Achievement", description="TBD", isObtained=false } }
    };
    public GameObject textPrefab;
    public Transform content;

    public void setAchievement(string request)
    {
        if (tracker.ContainsKey(request))
        {
            tracker[request].isObtained = true;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        // initializes the Dictionary which tracks whether the player isObtained an achievement
        // a separate Dictionary can be made to track the associated description,
        // or this one can be expanded.
        float yPos = 0;
        foreach (KeyValuePair<string, Achievement> kvp in tracker) {
            // Instantiate the text prefab
            GameObject achievementTextObject = Instantiate(textPrefab, content);

            // Get the Text component from the instantiated prefab
            Text textComponent = achievementTextObject.GetComponent<Text>();

            // Set the text of the Text component to the achievement name
            textComponent.text = kvp.Value.name;

            // Set the color of the Text component based on the isObtained property of the achievement
            if (kvp.Value.isObtained)
            {
                textComponent.color = Color.green;
            }
            else
            {
                textComponent.color = Color.red;
            }

            // Position the text object below the previous one
            Vector3 position = achievementTextObject.transform.position;
            position.y -= (textComponent.preferredHeight + yPos);
            achievementTextObject.transform.position = position;
            yPos += 30f;
        }
    }

}