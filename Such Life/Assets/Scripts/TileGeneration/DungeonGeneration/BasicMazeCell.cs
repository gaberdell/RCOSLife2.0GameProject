using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Flags] public enum OpenCells
{
    NorthOpen = 0b1,
    EastOpen =  0b10,
    SouthOpen = 0b100,
    WestOpen =  0b1000,
    Visited =   0b10000,
}

[Flags] //UnusedForNow Might use for later
public enum RoomStyle
{
}

public class BasicMazeCell
{
    public OpenCells cellData;
    public ushort intensity;
    public RoomStyle roomStyle;

    public BasicMazeCell()
    {
        Debug.Log("Hello I was created");
        cellData = 0;
        intensity = 0;
        roomStyle = 0;
    }
}
