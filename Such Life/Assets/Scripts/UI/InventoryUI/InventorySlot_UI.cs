using UnityEngine;
using UnityEngine.UI;
using TMPro;

/* Base codes provided by: Dan Pos - Game Dev Tutorials! with modification */
/// <summary>
/// Class <c>InventorySlot_UI</c> has an inventory slot but provides an image
///                               and text for the count of the slot.
///                                      
/// Relationship status : 
/// <c>MonoBehaviour</c> based class.
/// <c>InventorySlot</c> is what it gets info from and 
/// <c>DynamicTextControl</c> calls this script to update itself despite it already updating itself..
/// <c>InventoryUIController</c> Is the thing that actually starts using the 
///                              public RefreshDynamicInventory method alongside
///                              passing in an InventorySystem to show
/// <c>mouseBoxFollow</c> Uses this to gather info on what name and description to set
/// <c>MouseOver</c> Does a similar thing to mouse follow
/// <c>DynamicInventoryDisplay</c> Like InventoryDisplay but with an assign thingy
/// <c>StaticInventoryDisplay</c> Similar to the above
/// </summary>
public class InventorySlot_UI : MonoBehaviour
{
    [SerializeField] 
    private Image itemSprite;
    [SerializeField] 
    private TextMeshProUGUI itemCount;
    [SerializeField] 
    private InventorySlot assignedInventorySlot;

    private Button button;

    public Image ItemSprite => itemSprite;

    //getter for our private variable
    public InventorySlot AssignedInventorySlot => assignedInventorySlot;
    public InventoryDisplay ParentDisplay { get; private set; }
    private void Awake()
    {
        ClearSlot();

        itemSprite.preserveAspect = true;
        
        ParentDisplay = transform.parent.GetComponent<InventoryDisplay>();
    }

    public void OnEnable()
    {
        EventManager.inventorySlotPressed += OnUISlotClick;
    }

    public void OnDisable()
    {
        EventManager.inventorySlotPressed -= OnUISlotClick;
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

    public void OnUISlotClick(GameObject isUs)
    {
        if (isUs == gameObject)
        {
            //Access display class function
            ParentDisplay?.SlotClicked(this);
        }
    }
}
