using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BlockDestroyer : BlockInteraction
{

    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
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

            if (placingTileMap.GetTile(destroyPos) != null)
            {
                placingTileMap.SetTile(destroyPos, null);
            }
        }
    }
}
