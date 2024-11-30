using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MazeGenerator : MonoBehaviour
{
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

        GenerateCell(0, 0, sizeOfMaze.x, sizeOfMaze.y);

        GenerateCellToTileMap();
    }

    void GenerateCell(int currentX, int currentY, int maxX, int maxY)
    {
        if (basicCellMazeArray[currentX, currentY].cellData.HasFlag(OpenCells.Visited))
        {
            return;
        } 
        else {
            basicCellMazeArray[currentX, currentY].cellData |= OpenCells.Visited;
        }

        if (currentX + 1 < maxX)
        {
            basicCellMazeArray[currentX, currentY].cellData |= OpenCells.EastOpen;
            basicCellMazeArray[currentX+1, currentY].cellData |= OpenCells.WestOpen;
            GenerateCell(currentX + 1, currentY, maxX, maxY);
        }

        if (currentY + 1 < maxY)
        {
            basicCellMazeArray[currentX, currentY].cellData |= OpenCells.NorthOpen;
            basicCellMazeArray[currentX, currentY + 1].cellData |= OpenCells.SouthOpen;
            GenerateCell(currentX, currentY + 1, maxX, maxY);
        }

        if (currentX - 1 >= 0)
        {
            basicCellMazeArray[currentX, currentY].cellData |= OpenCells.WestOpen;
            basicCellMazeArray[currentX - 1, currentY].cellData |= OpenCells.EastOpen;
            GenerateCell(currentX - 1, currentY, maxX, maxY);
        }

        if (currentY - 1 >= 0)
        {
            basicCellMazeArray[currentX, currentY].cellData |= OpenCells.SouthOpen;
            basicCellMazeArray[currentX, currentY - 1].cellData |= OpenCells.NorthOpen;
            GenerateCell(currentX, currentY - 1, maxX, maxY);
        }
    }

    void ifCheck(int i, int j)
    {
        //Flippin annoying but I see no other way
        if (!basicCellMazeArray[i, j].cellData.HasFlag(OpenCells.NorthOpen))
        {
            tilesToPlaceOn.SetTile(new Vector3Int(3 * i, -3 * j - 2, 0), tileToPlace);
            tilesToPlaceOn.SetTile(new Vector3Int(3 * i + 1, -3 * j - 2, 0), tileToPlace);
            tilesToPlaceOn.SetTile(new Vector3Int(3 * i + 2, -3 * j - 2, 0), tileToPlace);
        }

        if (!basicCellMazeArray[i, j].cellData.HasFlag(OpenCells.SouthOpen))
        {
            tilesToPlaceOn.SetTile(new Vector3Int(3 * i, -3 * j, 0), tileToPlace);
            tilesToPlaceOn.SetTile(new Vector3Int(3 * i + 1, -3 * j, 0), tileToPlace);
            tilesToPlaceOn.SetTile(new Vector3Int(3 * i + 2, -3 * j, 0), tileToPlace);
        }

        if (!basicCellMazeArray[i, j].cellData.HasFlag(OpenCells.EastOpen))
        {
            tilesToPlaceOn.SetTile(new Vector3Int(3 * i + 2, -3 * j - 2, 0), tileToPlace);
            tilesToPlaceOn.SetTile(new Vector3Int(3 * i + 2, -3 * j - 1, 0), tileToPlace);
            tilesToPlaceOn.SetTile(new Vector3Int(3 * i + 2, -3 * j, 0), tileToPlace);
        }

        if (!basicCellMazeArray[i, j].cellData.HasFlag(OpenCells.WestOpen))
        {
            tilesToPlaceOn.SetTile(new Vector3Int(3 * i, -3 * j - 2, 0), tileToPlace);
            tilesToPlaceOn.SetTile(new Vector3Int(3 * i, -3 * j - 1, 0), tileToPlace);
            tilesToPlaceOn.SetTile(new Vector3Int(3 * i, -3 * j, 0), tileToPlace);
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
