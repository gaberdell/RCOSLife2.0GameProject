using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelToggle : MonoBehaviour
{
    public GameObject panel;

    public void OpenPanel()
    {
        if (panel != null)
        {
            bool status = panel.activeSelf;
            panel.SetActive(!status);
        }
    }
}
