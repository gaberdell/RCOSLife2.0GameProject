using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//Just a single script that holds a bit of Data that can be implemented easily
//Denotes what tile or tiles create this object.
public class CanBeCreatedBy : MonoBehaviour
{
    [SerializeField]
    private TileBase[] tilesThatCanCreateThisObject;

    [SerializeField]
    private bool isMadeByPlayer;

    [SerializeField]
    private TileBase tileBaseThatWasMadeWith;

    public TileBase[] TilesCanMakeThisObj => tilesThatCanCreateThisObject;
    public bool IsMadeByPlayer { get  {return isMadeByPlayer;} set {isMadeByPlayer = value;} }

    public TileBase TileMadeWith { get { return tileBaseThatWasMadeWith; } set { tileBaseThatWasMadeWith = value; } }

}
