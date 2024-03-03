using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{ 
    enum Move
    {
        LEFT,
        UP,
        RIGHT,
        DOWN
    }

    public string itemName = "sample item";
    public Sprite icon;

    //sample move board. ¡ç¡è¡æ¡é
    Move[] moveBoard;

    void Start()
    {
        for(int i = 0; i<4; i++)
        {

        }
    }
}
