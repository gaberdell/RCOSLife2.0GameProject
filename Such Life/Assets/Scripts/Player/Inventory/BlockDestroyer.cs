using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BlockDestroyer : BlockInteraction
{
    [SerializeField]
    BlockItemDictionary blockItemDictionary;

    [SerializeField]
    GameObject blockPreview;

    [SerializeField]
    Vector3 destroyPlaceOffset = new Vector3(0.5f, 0.5f, 0.5f);

    Vector3 destroyObjectPos = Vector3.zero;

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


        if (lengthOfDistanceVector > playerReach)
        {
            distanceFromPlayerVector = (distanceFromPlayerVector.normalized * playerReach);
        }

        Vector3 tileToDestroy = isTilePartOfRay(placingTileMap, transform.position, distanceFromPlayerVector, testIfBlockStepSize, out realWorldSpace);

        Vector3Int destroyPos = placingTileMap.WorldToCell(tileToDestroy);

        TileBase checkedTile;

        destroyObjectPos = destroyPos + destroyPlaceOffset;
        if ((checkedTile = placingTileMap.GetTile(destroyPos)) != null)
        {

        }
        else
        {
            blockPreview.SetActive(false);
        }
        blockPreview.transform.position = destroyObjectPos;

        //If right click down then check if the destroyable tile would place an object
        // if so desetroy it
        if (Input.GetKeyDown(KeyCode.Mouse0)) 
        {
            if ((checkedTile = placingTileMap.GetTile(destroyPos)) != null)
            {
                Debug.Log(checkedTile.name);
                placingTileMap.SetTile(destroyPos, null);
                if (blockItemDictionary.tileToItemPrefabDict[checkedTile])
                {
                    GameObject newBlockItem = Instantiate(blockItemDictionary.tileToItemPrefabDict[checkedTile]);
                    newBlockItem.transform.position = destroyObjectPos;
                    
                }
                
            }
        }
    }
}
