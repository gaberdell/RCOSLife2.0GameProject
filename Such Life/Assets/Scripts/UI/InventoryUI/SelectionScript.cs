using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Choose an object to select
public class SelectionScript : MonoBehaviour
{
    private static int numberOfHotBarKeys = 10;

    [SerializeField]
    private SpriteRenderer handItemSprite;

    [SerializeField]
    private StaticInventoryDisplay playerHotBarDisplay;

    [SerializeField]
    private Color newColor;

    private InventorySlot_UI[] slots = new InventorySlot_UI[numberOfHotBarKeys];

    private KeyCode[] keyCodesToCycleThrough = { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3,
                                                 KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6,
                                                 KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9,
                                                 KeyCode.Alpha0};

    private Image[] hotbarImages = new Image[numberOfHotBarKeys];

    private Image highLightedImage = null;
    private InventorySlot_UI slot = null;

    private Color baseColor;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numberOfHotBarKeys; i++)
        {
            slots[i] = null;
            hotbarImages[i] = null;
        }


        int count = 0;
        foreach  (InventorySlot_UI slotUI in playerHotBarDisplay.theSlots)
        {
            slots[count] = slotUI;
            hotbarImages[count] = slotUI.gameObject.GetComponent<Image>();
            count++;
        }
        baseColor = hotbarImages[0].GetComponent<Image>().color;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < numberOfHotBarKeys; i++)
        {
            if (Input.GetKeyDown(keyCodesToCycleThrough[i]))
            {
                if (highLightedImage != null) {
                    highLightedImage.color = baseColor;
                }

                if (highLightedImage != hotbarImages[i])
                {
                    hotbarImages[i].color = newColor;
                    highLightedImage = hotbarImages[i];
                    slot = slots[i];
                }
                else
                {
                    highLightedImage = null;
                    slot = null;
                }
                

            }
        }
        if (slot != null)
        {
            handItemSprite.sprite = slot.ItemSprite.sprite;
        }
        else
        {
            handItemSprite.sprite = null;
        }
    }
}
