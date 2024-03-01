using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelToggle : MonoBehaviour
{
    public GameObject panel;

    // Toggle panel active status
    public void togglePanel()
    {
        if (panel != null)
        {
            bool status = panel.activeSelf;
            panel.SetActive(!status);
        }
    }
}
