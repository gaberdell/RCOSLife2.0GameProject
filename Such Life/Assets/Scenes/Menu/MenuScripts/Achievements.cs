using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievements : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // initializes the Dictionary which tracks whether the player has an achievement
        // a separate Dictionary can be made to track the associated description,
        // or this one can be expanded.
        private Dictionary<string, bool> tracker = new Dictionary<string, bool>
        {
            // insert achievements here, initialize to false
            {"Play the game!", true},
            {"Vanquish a Slimmer!", false},
            {"Test Achievement", false}
        };
    }

    public void setAchievement(string request)
    {
        if(tracker.ContainsKey(request))
        {
            tracker[request] = true;
        }
    }
    
    
}
