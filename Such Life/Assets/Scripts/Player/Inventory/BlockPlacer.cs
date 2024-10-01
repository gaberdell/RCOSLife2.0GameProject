using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BlockPlacer : MonoBehaviour
{

    InventoryItemData currentlyHeldItem;
    InventorySlot currentInventorySlot;

    Tilemap currentTileMap;
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
        currentTileMap = GameObject.Find("Tilemap_placeables").GetComponent<Tilemap>();
        currentInstaitedPrefabParent = GameObject.Find("InstantiatedPlacedObjects");
    }


    void UpdateCurrentlySelectedSlot(IInventorySlot inventorySlot)
    {
        currentInventorySlot = (InventorySlot) inventorySlot;
    }


    bool isTilePartOfRay(Vector3 origin, Vector3 directionOfVector, float stepSize, out Vector3 vector3WithNoBlock)
    {
        

        float sizeOfVector = directionOfVector.magnitude;


        Vector3 stepAddVector = directionOfVector.normalized * stepSize;


        Vector3 previousStepThrough = origin;
        bool successfulReachedEnd = true;
        listOfStepPoints.Clear();
        for (Vector3 stepThroughVector = origin; (stepThroughVector-origin).magnitude < sizeOfVector; stepThroughVector+=stepAddVector)
        {
            if (currentTileMap.GetTile(currentTileMap.WorldToCell(stepThroughVector)) != null)
            {
                TileBase currentHitTile = currentTileMap.GetTile(currentTileMap.WorldToCell(stepThroughVector));
                successfulReachedEnd = false;
                break;
                
            }

            listOfStepPoints.Add(previousStepThrough);
            previousStepThrough = stepThroughVector;
        }

        if (successfulReachedEnd == true)
        {
            Vector3 totalVector = origin + directionOfVector;
            if (currentTileMap.GetTile(currentTileMap.WorldToCell(totalVector)) == null)
            {
                listOfStepPoints.Add(previousStepThrough);
                previousStepThrough = totalVector;
            }
        }

        

        if (previousStepThrough != origin)
        {
            vector3WithNoBlock = previousStepThrough;
            return true;
        }
        else
        {
            vector3WithNoBlock = origin;
            return false;
        }
        
    }

    void OnDrawGizmos()
    {
        foreach (var item in listOfStepPoints)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawIcon(item, "TestGizmo.tiff", true, Color.red);
        }
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


                isTilePartOfRay(transform.position, distanceFromPlayerVector, testIfBlockStepSize, out realWorldSpace);

                Vector3Int tileToPlace = currentTileMap.WorldToCell(realWorldSpace);

                blockPreview.transform.position = tileOffset + tileToPlace;

                previewRenderer.sprite = currentlyHeldItem.placeObject.placeTile.sprite;

                if (Input.GetKeyDown(KeyCode.Mouse1))
                {

                    currentInventorySlot.RemoveFromStack(1);

                    currentTileMap.SetTile(tileToPlace, currentlyHeldItem.placeObject.placeTile);
                    GameObject currentlyHeldPrefab = currentlyHeldItem.placeObject.placeGameObject;
                    if (currentlyHeldPrefab)
                    {
                        Instantiate(currentlyHeldPrefab, blockPreview.transform.position, Quaternion.identity, currentInstaitedPrefabParent.transform);
                    }
                }

            }
            else if (currentlyHeldItem.placeObject == null)
            {
                previewRenderer.sprite = null;
                blockPreview.transform.localPosition = Vector3.zero;
            }
        }
        else
        {
            previewRenderer.sprite = null;
            blockPreview.transform.localPosition = Vector3.zero;
        }
    }
}
