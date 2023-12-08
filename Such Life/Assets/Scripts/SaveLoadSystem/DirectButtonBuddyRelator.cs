using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Kind of a hot fix scripts that communicates to the
 * button component in Unity without making the
 * scripts use new methods*/
public class DirectButtonBuddyRelator : MonoBehaviour
{
    public void UseSaveGameButton()
    {
        EventManager.SaveGame(EventManager.LoadData());
    }

    public void UseLoadGameButton()
    {
        EventManager.LoadData();
    }

    public void DeleteDataButton()
    {
        EventManager.DeleteData();
    }
}
