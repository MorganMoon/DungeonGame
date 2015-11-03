using UnityEngine;
using System.Collections;

[System.Serializable]
public class GameItem {
    //Basic item stats
    private int goldWorth; //The amount of gold this item is worth
    private string itemName; //The name of the item

    public GameItem(string itemName, int goldWorth) //Class constructor
    {
        SetItemName(itemName);
        SetGoldWorth(goldWorth);
    }

    //getters
    public int GetGoldWorth() //Gets int 'goldWorth'
    {
        return this.goldWorth;
    }
    public string GetItemName() //Gets string 'itenName'
    {
        return this.itemName;
    }
    
    //setters
    public void SetGoldWorth(int goldWorth) //Sets int 'goldWorth'
    {
        this.goldWorth = goldWorth;
    }
    public void SetItemName(string itemName) //Sets string 'itemName'
    {
        this.itemName = itemName;
    }
}
