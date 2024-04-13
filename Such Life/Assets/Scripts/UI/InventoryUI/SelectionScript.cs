using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Choose an object to select
public class SelectionScript : MonoBehaviour
{
    private static int numberOfHotBarKeys = 10;

    [SerializeField]
    StaticInventoryDisplay playerHotBarDisplay;

    private InventorySlot_UI[] slots = new InventorySlot_UI[numberOfHotBarKeys];

    private KeyCode[] keyCodesToCycleThrough = { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3,
                                                 KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6,
                                                 KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9,
                                                 KeyCode.Alpha0};

    private GameObject[] gameObjects = new GameObject[numberOfHotBarKeys];


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numberOfHotBarKeys; i++)
        {
            slots[i] = null;
            gameObjects[i] = null;
        }


        int count = 0;
        foreach  (InventorySlot_UI slotUI in playerHotBarDisplay.theSlots)
        {
            slots[count] = slotUI;
            gameObjects[count] = slotUI.gameObject;
            count++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < numberOfHotBarKeys; i++)
        {
            if (Input.GetKeyDown(keyCodesToCycleThrough[i]))
            {
                gameObjects[i].GetComponent<Image>().color = Color.white;
            }
        }
    }
}
