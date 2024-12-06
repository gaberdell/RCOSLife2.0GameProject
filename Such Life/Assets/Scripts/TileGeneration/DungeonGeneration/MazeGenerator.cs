using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MazeGenerator : MonoBehaviour
{
    [Range(0f,1f)]
    [SerializeField]
    float directionChangeChance;

    [Range(0f, 1f)]
    [SerializeField]
    float splitChangeChance;

    [SerializeField]
    Vector2Int sizeOfMaze;

    [SerializeField]
    Tilemap tilesToPlaceOn;

    [SerializeField]
    TileBase tileToPlace;

    BasicMazeCell[,] basicCellMazeArray;

    // Start is called before the first frame update
    void Start()
    {
        basicCellMazeArray = new BasicMazeCell[sizeOfMaze.x, sizeOfMaze.y];

        for (int i = 0; i < sizeOfMaze.x; i++)
        {
            for (int j = 0; j < sizeOfMaze.y; j++)
            {
                basicCellMazeArray[i, j] = new BasicMazeCell();
            }
        }

        GenerateCell(9, 9, 8, 9, sizeOfMaze.x, sizeOfMaze.y);

        GenerateCellToTileMap();
    }

    void GenerateCell(int currentX, int currentY, int prevX, int prevY, int maxX, int maxY)
    {
        if (basicCellMazeArray[currentX, currentY].cellData.HasFlag(OpenCells.Visited))
        {
            return;
        } 
        else {
            basicCellMazeArray[currentX, currentY].cellData |= OpenCells.Visited;
        }

        //Sanity Check Moves
        if (currentX + 1 == maxX)
        {
            basicCellMazeArray[currentX-1, currentY].cellData |= OpenCells.EastOpen;
            basicCellMazeArray[currentX, currentY].cellData |= OpenCells.WestOpen;
            GenerateCell(currentX, currentY, currentX, currentY, maxX, maxY);
        }
        else if (currentY + 1 == maxY)
        {
            basicCellMazeArray[currentX, currentY-1].cellData |= OpenCells.NorthOpen;
            basicCellMazeArray[currentX, currentY].cellData |= OpenCells.SouthOpen;
            GenerateCell(currentX, currentY, currentX, currentY, maxX, maxY);
        }
        else if (currentX - 1 == 0)
        {
            basicCellMazeArray[currentX, currentY].cellData |= OpenCells.WestOpen;
            basicCellMazeArray[currentX - 1, currentY].cellData |= OpenCells.EastOpen;
            GenerateCell(currentX - 1, currentY, currentX, currentY, maxX, maxY);
        }
        else if (currentY - 1 == 0)
        {
            basicCellMazeArray[currentX, currentY].cellData |= OpenCells.SouthOpen;
            basicCellMazeArray[currentX, currentY - 1].cellData |= OpenCells.NorthOpen;
            GenerateCell(currentX, currentY - 1, currentX, currentY, maxX, maxY);
        }
        else
        {
            
            if (currentX - prevX > 0)
            {
                basicCellMazeArray[currentX - 1, currentY].cellData |= OpenCells.EastOpen;
                basicCellMazeArray[currentX, currentY].cellData |= OpenCells.WestOpen;
            }
            if (currentX - prevX < 0)
            {
                basicCellMazeArray[currentX, currentY].cellData |= OpenCells.WestOpen;
                basicCellMazeArray[currentX - 1, currentY].cellData |= OpenCells.EastOpen;
            }

            if (currentY - prevY > 0)
            {
                basicCellMazeArray[currentX, currentY - 1].cellData |= OpenCells.NorthOpen;
                basicCellMazeArray[currentX, currentY].cellData |= OpenCells.SouthOpen;
            }
            if (currentY - prevY < 0)
            {
                basicCellMazeArray[currentX, currentY].cellData |= OpenCells.SouthOpen;
                basicCellMazeArray[currentX, currentY - 1].cellData |= OpenCells.NorthOpen;
            }

            //Mutation Chance where we the dungeon crawler has a random chance to change direction
            float randomVal = Random.Range(0f,1f);

            if (randomVal <= directionChangeChance) {
                GenerateCell(currentX + currentY - prevY , currentY + currentX - prevX, currentX, currentY, maxX, maxY);
                if (randomVal <= splitChangeChance)
                {
                    GenerateCell(currentX + currentX - prevX, currentY + currentY - prevY, currentX, currentY, maxX, maxY);
                }
            }
            else
            {
                GenerateCell(currentX + currentX - prevX, currentY + currentY - prevY, currentX, currentY, maxX, maxY);
            }
                

            //GenerateCell(currentX + currentX - prevX, currentY + currentY - prevY, currentX, currentY, maxX, maxY);
        }
    }

    void ifCheck(int i, int j)
    {
        if (basicCellMazeArray[i, j].cellData.HasFlag(OpenCells.Visited))
        {
            tilesToPlaceOn.SetTile(new Vector3Int(3 * i, -3 * j - 2, 0), tileToPlace);
            tilesToPlaceOn.SetTile(new Vector3Int(3 * i + 2, -3 * j - 2, 0), tileToPlace);

            tilesToPlaceOn.SetTile(new Vector3Int(3 * i, -3 * j, 0), tileToPlace);
            tilesToPlaceOn.SetTile(new Vector3Int(3 * i + 2, -3 * j, 0), tileToPlace);

            tilesToPlaceOn.SetTile(new Vector3Int(3 * i + 2, -3 * j - 2, 0), tileToPlace);
            tilesToPlaceOn.SetTile(new Vector3Int(3 * i + 2, -3 * j, 0), tileToPlace);

            tilesToPlaceOn.SetTile(new Vector3Int(3 * i, -3 * j - 2, 0), tileToPlace);
            tilesToPlaceOn.SetTile(new Vector3Int(3 * i, -3 * j, 0), tileToPlace);
        }

        //Flippin annoying but I see no other way
        if (!basicCellMazeArray[i, j].cellData.HasFlag(OpenCells.NorthOpen))
        { 
            tilesToPlaceOn.SetTile(new Vector3Int(3 * i + 1, -3 * j - 2, 0), tileToPlace);
        }

        if (!basicCellMazeArray[i, j].cellData.HasFlag(OpenCells.SouthOpen))
        {
            
            tilesToPlaceOn.SetTile(new Vector3Int(3 * i + 1, -3 * j, 0), tileToPlace);
            
        }

        if (!basicCellMazeArray[i, j].cellData.HasFlag(OpenCells.EastOpen))
        {
            
            tilesToPlaceOn.SetTile(new Vector3Int(3 * i + 2, -3 * j - 1, 0), tileToPlace);
            
        }

        if (!basicCellMazeArray[i, j].cellData.HasFlag(OpenCells.WestOpen))
        {
            
            tilesToPlaceOn.SetTile(new Vector3Int(3 * i, -3 * j - 1, 0), tileToPlace);
            
        }
    }


    void GenerateCellToTileMap()
    {
        for (int i = 0; i < sizeOfMaze.x; i++)
        {

            for (int j = 0; j < sizeOfMaze.y; j++)
            {
                if (basicCellMazeArray[i, j].cellData.HasFlag(OpenCells.Visited))
                {
                    ifCheck(i, j);
                }
                else
                {
                    Debug.Log("Fill in");
                    tilesToPlaceOn.BoxFill(new Vector3Int(3 * i, 3 * j, 0), tileToPlace, 0, 0, 3, 3);
                }
            }
        }
    }
}
