using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Changing the menu page,will likely move to event manager
 * Must interact/integrated with TabScript
 * 
 */
public class PanelToggle : MonoBehaviour
{
    [SerializeField] GameObject panel;

    // Toggle panel active status, set to "close" and "menu" buttons
    public void togglePanel()
    {
        if (panel != null)
        {
            bool status = panel.activeSelf;
            panel.SetActive(!status);
        }
    }
}
