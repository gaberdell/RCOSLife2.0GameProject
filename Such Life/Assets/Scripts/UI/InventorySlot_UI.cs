using UnityEngine;
using UnityEngine.UI;
using TMPro;

/* Base codes provided by: Dan Pos - Game Dev Tutorials! with modification */
public class InventorySlot_UI : MonoBehaviour
{
    [SerializeField] private Image itemSprite;
    [SerializeField] private TextMeshProUGUI itemCount;
    [SerializeField] private InventorySlot assignedInventorySlot;

    private Button button;

    //getter for our private variable
    public InventorySlot AssignedInventorySlot => assignedInventorySlot;
    public InventoryDisplay ParentDisplay { get; private set; }
    private void Awake()
    {
        ClearSlot();

        button = GetComponent<Button>();
        button?.onClick.AddListener(OnUISlotClick);
        
        ParentDisplay = transform.parent.GetComponent<InventoryDisplay>();
    }

    public void Init(InventorySlot slot)
    {
        assignedInventorySlot = slot;
        UpdateUISlot(slot); //update the UI 
    }

    public void UpdateUISlot(InventorySlot slot)
    {
        if(slot.ItemData != null)
        {
            itemSprite.sprite = slot.ItemData.Icon;
            itemSprite.color = Color.white;
            if(slot.StackSize > 1)
            {
                itemCount.text = slot.StackSize.ToString();
            }
            else
            {
                itemCount.text = "";
            }
        }
        else
        {
            ClearSlot();
        }
    }

    //refresh the inventory slot and check for item
    public void UpdateUISlot()
    {
        if(assignedInventorySlot != null)
        {
            UpdateUISlot(assignedInventorySlot); 
        }
    }


    public void ClearSlot()
    {
        assignedInventorySlot?.ClearSlot(); //Clear the slot in the UI and also the system as well
        itemSprite.sprite = null;
        itemSprite.color = Color.clear;
        itemCount.text = "";
    }

    public void OnUISlotClick()
    {
        //Access display class function
        ParentDisplay?.SlotClicked(this);
    }
}
