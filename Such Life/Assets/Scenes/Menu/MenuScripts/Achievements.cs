using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievements : MonoBehaviour
{
    public Dictionary<string, Achievement> tracker;

    public class Achievement
    {
        public string name;
        public string description;
        public bool has;
    }

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
        // initializes the Dictionary which tracks whether the player has an achievement
        // a separate Dictionary can be made to track the associated description,
        // or this one can be expanded.
        tracker = new Dictionary<string, Achievement>
        {
            // insert achievements here, initialize to false
            {"Play the game!", new Achievement {name="Play the game!", description="TBD", has=true }  },
            {"Vanquish a Slimmer!", new Achievement {name="Vaniquish a Slimmer!", description="TBD", has=false }  },
            {"Test Achievement", new Achievement{name="Test Achievement", description="TBD", has=false } }
        };
    }
}