using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BlockInteraction : MonoBehaviour
{

    protected Tilemap placingTileMap;

    [SerializeField]
    protected float playerReach = 6;

    [SerializeField]
    protected Vector3 tileOffset = new Vector3(0.5f, 0.5f, 0);

    [SerializeField]
    protected float testIfBlockStepSize = 0.1f;

    List<Vector3> listOfStepPoints = new List<Vector3>();

    //Dumb stupid dictionairy is the only way we can tell what is block and what is other thing
    [SerializeField]
    protected static SerializableDictionary<TileBase, GameObject> tileMapToItem;


    virtual protected void Start()
    {
        placingTileMap = GameObject.Find("Tilemap_placeables").GetComponent<Tilemap>();
    }


    protected Vector3 isTilePartOfRay(Tilemap currentTileMap, Vector3 origin, Vector3 directionOfVector, float stepSize, out Vector3 vector3WithNoBlock)
    {

        float sizeOfVector = directionOfVector.magnitude;

       

        Vector3 stepAddVector = directionOfVector.normalized * stepSize;


        Vector3 previousStepThrough = origin;
        bool successfulReachedEnd = true;
        listOfStepPoints.Clear();
        Vector3 stepThroughVector;
        for (stepThroughVector = origin; (stepThroughVector - origin).magnitude < sizeOfVector; stepThroughVector += stepAddVector)
        {
            if ((currentTileMap.GetTile(currentTileMap.WorldToCell(stepThroughVector))) != null)
            {
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
            return stepThroughVector;
        }
        else
        {
            vector3WithNoBlock = origin;
            return stepThroughVector;
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
}
