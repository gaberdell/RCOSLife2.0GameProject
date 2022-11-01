using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Base codes provided by: Dan Pos - Game Dev Tutorials!*/

namespace SaveLoadSystem
{
    [System.Serializable]
    //This class will save whatever it is in your game
    /* Things to save in your game: Player's position, Chest inventory data, player's backpack
     * Structure position, player's health and stats, all of the player's interaction,
     * NPC status and what they have been up to, NPC position,...
     * 
     * 
     */
    public class SaveData
    {
        public int index = 1;
        [SerializeField] private float myFloat = 5.8f;
        public bool ourBool = false;
        public Vector3 ourVector = new Vector3(0, 10, 99);
    }
}

