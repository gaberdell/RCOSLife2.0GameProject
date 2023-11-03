using UnityEngine;

//Script originate from:
//https://www.youtube.com/watch?v=9tePzyL6dgc&list=PLPV2KyIb3jR4KLGCCAciWQ5qHudKtYeP7&index=3


//This class will define what is an interactable object 
public class Interactable : MonoBehaviour
{
    //how close a player need to get in order to interact with the object.
    public float radius = 3f;
    public Transform interactionTransform;

    //This bool will check if the player is close enough to interact with the object or not.
    bool isFocused = false;

    Transform player;

    bool hasInteracted = false;

    public virtual void Interact()
    {
        //This method is meant to be overwritten by other interactable item
        Debug.Log("Interactng with " + transform.name);
    }

    void Update()
    {
        if (isFocused && !hasInteracted)
        {
            //check the distance to the player
            float playerDistanceToObj = Vector2.Distance(player.position, interactionTransform.position);
            if(playerDistanceToObj <= radius)
            {
                Interact();
                Debug.Log("INTERACT");
                hasInteracted = true;
            }
        }
    }


    //If the player is within pick up radius of an object, it's considered to be OnFocused
    public void OnFocused(Transform playerTransform)
    {
        isFocused = true;
        player = playerTransform;
        hasInteracted = false;
    }

    //If the player is outside of the pick up radius of an object, it's considered to be "Defocused"
    public void OnDefocused()
    {
        isFocused = false;
        player = null;
        hasInteracted = false;
    }



    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
