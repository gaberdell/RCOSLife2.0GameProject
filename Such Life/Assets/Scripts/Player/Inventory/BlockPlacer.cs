using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BlockPlacer : BlockInteraction
{

    InventoryItemData currentlyHeldItem;
    InventorySlot currentInventorySlot;

    Tilemap placingTileMap;
    GameObject currentInstaitedPrefabParent;

    [SerializeField]
    GameObject blockPreview;

    SpriteRenderer previewRenderer;

    [SerializeField]
    Vector3 tileOffset = new Vector3(0.5f,0.5f,0);

    [SerializeField]
    float playerReach = 6;

    [SerializeField]
    float testIfBlockStepSize = 0.1f;

    List<Vector3> listOfStepPoints = new List<Vector3>();

    private void OnEnable()
    {
        EventManager.currentSlotSelected += UpdateCurrentlySelectedSlot;
    }

    private void OnDisable()
    {
        EventManager.currentSlotSelected -= UpdateCurrentlySelectedSlot;
    }

    void Start()
    {
        previewRenderer = blockPreview.GetComponent<SpriteRenderer>();
        placingTileMap = GameObject.Find("Tilemap_placeables").GetComponent<Tilemap>();
        currentInstaitedPrefabParent = GameObject.Find("InstantiatedPlacedObjects");
    }


    void UpdateCurrentlySelectedSlot(IInventorySlot inventorySlot)
    {
        currentInventorySlot = (InventorySlot) inventorySlot;
    }


    // Update is called once per frame
    void Update()
    {
        if (currentInventorySlot != null && currentInventorySlot.StackSize > 0)
        {
            currentlyHeldItem = currentInventorySlot.ItemData;

            if (currentlyHeldItem && currentlyHeldItem.placeObject && currentlyHeldItem.placeObject.placeTile)
            {

                Vector3 mousePos = Input.mousePosition;

                Vector3 realWorldSpace = Camera.main.ScreenToWorldPoint(mousePos);

                realWorldSpace.z = transform.position.z;

                //Directional Vector
                Vector3 distanceFromPlayerVector = (realWorldSpace - transform.position);

                float lengthOfDistanceVector = distanceFromPlayerVector.magnitude;


                if (lengthOfDistanceVector > playerReach)
                {
                    distanceFromPlayerVector = (distanceFromPlayerVector.normalized * playerReach);
                }


                isTilePartOfRay(placingTileMap, transform.position, distanceFromPlayerVector, testIfBlockStepSize, out realWorldSpace);

                Vector3Int tileToPlace = placingTileMap.WorldToCell(realWorldSpace);

                blockPreview.transform.position = tileOffset + tileToPlace;

                previewRenderer.sprite = currentlyHeldItem.placeObject.placeTile.sprite;

                if (Input.GetKeyDown(KeyCode.Mouse1))
                {

                    currentInventorySlot.RemoveFromStack(1);

                    placingTileMap.SetTile(tileToPlace, currentlyHeldItem.placeObject.placeTile);
                    GameObject currentlyHeldPrefab = currentlyHeldItem.placeObject.placeGameObject;
                    if (currentlyHeldPrefab)
                    {
                        Instantiate(currentlyHeldPrefab, blockPreview.transform.position, Quaternion.identity, currentInstaitedPrefabParent.transform);
                    }
                }

            }
        }
        else
        {
            previewRenderer.sprite = null;
            blockPreview.transform.localPosition = Vector3.zero;
        }
    }
}
