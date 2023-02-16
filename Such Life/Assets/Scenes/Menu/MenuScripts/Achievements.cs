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
        public bool has;
    }

    public Dictionary<string, Achievement> tracker = new Dictionary<string, Achievement> {
        // insert achievements here, initialize to false
        {"Play the game!", new Achievement {name="Play the game!", description="TBD", has=true }  },
        {"Vanquish a Slimmer!", new Achievement {name="Vaniquish a Slimmer!", description="TBD", has=false }  },
        {"Test Achievement", new Achievement{name="Test Achievement", description="TBD", has=false } }
    };
    public GameObject textPrefab;
    public Transform content;

    public void setAchievement(string request)
    {
        if (tracker.ContainsKey(request))
        {
            tracker[request].has = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Instantiate the text prefab once
        GameObject newText = Instantiate(textPrefab, content);

        // Get the text component of the new text object
        Text textComponent = newText.GetComponent<Text>();

        // initializes the Dictionary which tracks whether the player has an achievement
        // a separate Dictionary can be made to track the associated description,
        // or this one can be expanded.
        foreach (KeyValuePair<string, Achievement> kvp in tracker) {
            textComponent.text += kvp.Key + '\n';
        }
    }
}