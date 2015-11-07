using UnityEngine;
using System.Collections;

[System.Serializable]
public class GameItem {

    //Basic item stats
    private int goldWorth; //The amount of gold this item is worth
    private string itemName; //The name of the item

    public GameItem(string itemName, int goldWorth) //Class constructor 2 args
    {
        SetItemName(itemName);
        SetGoldWorth(goldWorth);
    }

    public GameItem() //Class constructor 0 args
    {
        SetItemName(RandomName());
        SetGoldWorth(Random.Range(1, 11));
    }

    //Methods
    public virtual string RandomName()
    {
        string[] first = {"Trashy ", "Old ", "Used ", "Crappy ", "Colorful ", "Broken ", "New "};
        string[] second = {"Trash ", "Cloth ", "Garbage ", "Toy ", "Picture ", "Item ", "Flower", "Thing "};
        string name = string.Concat(first[Random.Range(0, first.Length - 1)], second[Random.Range(0, second.Length - 1)]);
        return name;
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

    //ToString method
    public override string ToString()
    {
        return GetItemName() + " is worth: " + GetGoldWorth();
    }
}
