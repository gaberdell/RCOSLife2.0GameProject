using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class handleHeight : MonoBehaviour
{
    [SerializeField] private Tilemap rocksMap;
    private List<string> entranceTileNames;
    private List<Tile> entranceTiles;
    private List<List<Vector3Int>> tileGroups;
    private List<Vector3Int> entranceTilesPos;
    // Start is called before the first frame update
    void Start()
    {
        //define entrance tiles;
        entranceTileNames = new List<string>();
        tileGroups = new List<List<Vector3Int>>();
        entranceTiles = new List<Tile>();
        entranceTileNames.Add("TilesetExample_20");
        createEntranceBoundingBoxes();
    }

    private void createEntranceBoundingBoxes()
    {
        //find all 'entrance tiles' in the rock layer
        Tilemap tilemap = rocksMap;
        int x, y;
        for (x = tilemap.cellBounds.min.x; x < tilemap.cellBounds.max.x; x++)
        {
            for (y = tilemap.cellBounds.min.y; y < tilemap.cellBounds.max.y; y++)
            {

                Tile T = tilemap.GetTile<Tile>(new Vector3Int(x, y, 0));
                if (T != null && entranceTileNames.Contains(T.name))
                {
                    print(new Vector3Int(x, y, 0));
                    //add tile to list
                    entranceTiles.Add(T);
                    //add tile to pos list
                    entranceTilesPos.Add(new Vector3Int(x, y, 0));
                }
                //store tile in a 2d array
            }

        }
        int i, j, k;
        bool added = false;
        //for every individual entrance tile
        for(i = 0; i < entranceTilesPos.Count; i++)
        {
            Vector3Int TPos = entranceTilesPos[i];
            List<Vector3Int> adjacentTiles = new List<Vector3Int>();
            //get adjacent entrance tiles - above, below, left, right
            if (entranceTilesPos.Contains(TPos + new Vector3Int(1, 0, 0))) adjacentTiles.Add(TPos + new Vector3Int(1, 0, 0));
            if (entranceTilesPos.Contains(TPos + new Vector3Int(-1, 0, 0))) adjacentTiles.Add(TPos + new Vector3Int(-1, 0, 0));
            if (entranceTilesPos.Contains(TPos + new Vector3Int(0, 1, 0))) adjacentTiles.Add(TPos + new Vector3Int(0, 1, 0));
            if (entranceTilesPos.Contains(TPos + new Vector3Int(0, -1, 0))) adjacentTiles.Add(TPos + new Vector3Int(0, -1, 0));

            //check if adjacent entranceTiles are in any lists;
            for (j = 0; j<tileGroups.Count; j++)
            {
                if (added) continue;
                for(k = 0; k<adjacentTiles.Count; k++)
                {
                    if(added) continue;
                    if (tileGroups[j].Contains(adjacentTiles[k]))
                    {
                        //add current tile to that tile group
                        tileGroups[j].Add(entranceTilesPos[i]);
                        //stop looping
                        added = true;
                    }
                }
            }

            //if not, then add this tile as its own list 
            if (!added)
            {
                //create a new list with just this tile
                List<Vector3Int> newList = new List<Vector3Int>();
                newList.Add(entranceTilesPos[i]);
                //add list to groups
                tileGroups.Add(newList);
            }
        }

        //create bounding box on each group of entrance tiles (attribute of rock tilemap
        //could be expanded to dynamically create bounding boxes for rock formations too? - following this, could dynamically
        //determine entrance tiles so they dont have to be hardcoded anymore

        //for each group, determine a height and width for its bounding box
        for (i = 0; i < tileGroups.Count; i++)
        {
            //get highest, leftest, lowest, and rightest tile
            int highest = tileGroups[i][0].y;
            int lowest = tileGroups[i][0].y;
            int leftest = tileGroups[i][0].x;
            int rightest = tileGroups[i][0].x;
            for (j = 0; j < tileGroups[i].Count; j++)
            {
                Vector3Int T = tileGroups[i][j];
                if(T.y > highest) highest = T.y;
                if(T.y < lowest) lowest = T.y;
                if(T.x < leftest) leftest = T.x;
                if(T.x > rightest) rightest = T.x;

            }
            //subtract these from each other to get the width and the height of their box
            int width = rightest - leftest;
            int height = highest - lowest;
            //create bounding box
        }
    }
}
