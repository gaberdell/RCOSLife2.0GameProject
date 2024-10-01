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
    [SerializeField]
    private InventoryDisplay manuallySetDisplay;


    private Button button;

    public Image ItemSprite => itemSprite;

    [SerializeField]
    private ItemType itemTypeCheck;

    //getter for our private variable
    public InventorySlot AssignedInventorySlot { get { return assignedInventorySlot; }
        set { 
            if (assignedInventorySlot != null)
            {
                assignedInventorySlot.ItemTypeCheck = (ItemType) 0b111111111;
                assignedInventorySlot.itemSlotUpdated -= UpdateUISlot;
            }
            assignedInventorySlot = value;
            assignedInventorySlot.ItemTypeCheck = itemTypeCheck;
            assignedInventorySlot.itemSlotUpdated += UpdateUISlot;
        }
    }
    
    public InventoryDisplay ParentDisplay { get; private set; }
    private void Awake()
    {
        ClearSlot();

        itemSprite.preserveAspect = true;

        if (manuallySetDisplay == null)
            ParentDisplay = transform.parent.GetComponent<InventoryDisplay>(); // prob the issue is right here
        else
            ParentDisplay = manuallySetDisplay;
    }

    public void OnEnable()
    {
        if (assignedInventorySlot != null) {
            assignedInventorySlot.itemSlotUpdated += UpdateUISlot;
        }
        EventManager.inventorySlotPressed += OnUISlotClick;
    }

    public void OnDisable()
    {
        if (assignedInventorySlot != null) {
            assignedInventorySlot.itemSlotUpdated -= UpdateUISlot;
        }
        EventManager.inventorySlotPressed -= OnUISlotClick;
    }

    public void AssignInventorySlotTo(InventorySlot slot)
    {
        AssignedInventorySlot = slot;
        UpdateUISlot(slot); //update the UI 
    }

    private void UpdateUISlot(InventorySlot slot)
    {
        if (slot.ItemData != null)
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
    private void UpdateUISlot()
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
            ParentDisplay?.SlotClicked(this, itemTypeCheck);
        }
    }
}
