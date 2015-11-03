using UnityEngine;
using System.Collections;

[System.Serializable]
public class GameItem {
    //Basic item stats
    private int goldWorth;

    public GameItem(int goldWorth)
    {
        this.goldWorth = goldWorth;
    }

    //getters
    public int GetGoldWorth()
    {
        return this.goldWorth;
    }
    
    //setters
    public void SetGoldWorth(int goldWorth)
    {
        this.goldWorth = goldWorth;
    }
}
