using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabScript : MonoBehaviour
{
    // Store pages/tabs
    [SerializeField] GameObject[] pages;

    // Toggle specific tab, must assign to tab button
    public void TabToggle(int tabID)
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(false);
        }
        pages[tabID - 1].SetActive(true);
    }
}
