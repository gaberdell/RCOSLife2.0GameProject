using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Flags]
public enum ItemType
{
                //This funky text is a binary literal. Essentially its so we can set
                //each number to represent a flag option
    Normal      = 0b000000001,
    Helmet      = 0b000000010,
    Chestplate  = 0b000000100,
    Leggings    = 0b000001000,
    Boots       = 0b000010000,
    Eyes        = 0b000100000,
    Face        = 0b001000000,
    Ring        = 0b010000000,
    Glove       = 0b100000000,
}