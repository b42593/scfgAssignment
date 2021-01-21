using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string name;
    public float time;

    public PlayerData(GameManager player) 
    {
        name = player.name;
        time = player.completetimervalue;
    }

}
