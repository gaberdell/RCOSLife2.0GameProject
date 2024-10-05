using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BlockInteraction : MonoBehaviour
{

    List<Vector3> listOfStepPoints = new List<Vector3>();

    protected bool isTilePartOfRay(Tilemap currentTileMap, Vector3 origin, Vector3 directionOfVector, float stepSize, out Vector3 vector3WithNoBlock)
    {
        TileBase currentHitTile = null;

        float sizeOfVector = directionOfVector.magnitude;


        Vector3 stepAddVector = directionOfVector.normalized * stepSize;


        Vector3 previousStepThrough = origin;
        bool successfulReachedEnd = true;
        listOfStepPoints.Clear();
        for (Vector3 stepThroughVector = origin; (stepThroughVector - origin).magnitude < sizeOfVector; stepThroughVector += stepAddVector)
        {
            if ((currentHitTile = currentTileMap.GetTile(currentTileMap.WorldToCell(stepThroughVector))) != null)
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
}
