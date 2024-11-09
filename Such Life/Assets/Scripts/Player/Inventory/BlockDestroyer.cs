using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//https://discussions.unity.com/t/how-do-i-make-an-object-look-at-an-another-object-in-top-down-2d-c/104849
//For the rotation info thanks!

//Info on branchlessly doing stuff in https://dev.to/jobinrjohnson/branchless-programming-does-it-really-matter-20j4
//Info on converting 

public class BlockDestroyer : BlockInteraction
{
    [SerializeField]
    BlockItemDictionary blockItemDictionary;

    [SerializeField]
    GameObject blockPreview;

    [SerializeField]
    Vector3 destroyPlaceOffset = new Vector3(0.5f, 0.5f, 0.5f);

    Vector3 destroyObjectPos = Vector3.zero;

    float previewAngleOffset = -90;
    float roundPreviewAnglesTo = 90;

    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;

        Vector3 realWorldSpace = Camera.main.ScreenToWorldPoint(mousePos);

        realWorldSpace.z = transform.position.z;

        //Directional Vector
        Vector3 distanceFromPlayerVector = (realWorldSpace - transform.position);

        float lengthOfDistanceVector = distanceFromPlayerVector.magnitude;

        
        //Branchless way of trying to find the max
        distanceFromPlayerVector =    System.Convert.ToInt32(lengthOfDistanceVector <= playerReach) * distanceFromPlayerVector 
                                    + System.Convert.ToInt32(lengthOfDistanceVector > playerReach) * (distanceFromPlayerVector.normalized * playerReach);

        Vector3 tileToDestroy = isTilePartOfRay(placingTileMap, transform.position, distanceFromPlayerVector, testIfBlockStepSize, out realWorldSpace);

        Vector3Int destroyPos = placingTileMap.WorldToCell(tileToDestroy);

        TileBase checkedTile;

        destroyObjectPos = destroyPos + destroyPlaceOffset;
        if ((checkedTile = placingTileMap.GetTile(destroyPos)) != null)
        {
            blockPreview.SetActive(true);
            blockPreview.transform.position = destroyObjectPos;
            //Change this slow ass math later dawg we gotta do atan for this
            float angle = Mathf.Round(((Mathf.Atan2(distanceFromPlayerVector.y, distanceFromPlayerVector.x) * Mathf.Rad2Deg) + previewAngleOffset) / roundPreviewAnglesTo) * roundPreviewAnglesTo;
            blockPreview.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            blockPreview.SetActive(false);
        }
        

        //If right click down then check if the destroyable tile would place an object
        // if so desetroy it
        if (Input.GetKeyDown(KeyCode.Mouse0)) 
        {
            if (checkedTile != null)
            {
                Debug.Log(checkedTile.name);
                placingTileMap.SetTile(destroyPos, null);
                if (blockItemDictionary.tileToItemPrefabDict[checkedTile])
                {
                    GameObject newBlockItem = Instantiate(blockItemDictionary.tileToItemPrefabDict[checkedTile]);
                    newBlockItem.transform.position = destroyObjectPos;
                    
                }
                // Alright Check if the block above it is related to the check tile. If so then Destroy it

                
            }
        }
    }
}
