using UnityEngine;
using System.Collections;

[System.Serializable]
public class ChestItem : GameItem
{

    //Head item advanced stats
    private float defense;
    private float strength;
    private float endurance;
    private float crit;
    private int reqLevel;

    //Chestitem constructor 6 arg
    public ChestItem(string itemName, int goldWorth, float defense, float endurance, float strength, float crit) : base(itemName, goldWorth)
    {
        SetDefense(defense);
        SetStrength(strength);
        SetEndurance(endurance);
        SetCrit(crit);
        SetReqLevel((int)Mathf.Max(Mathf.Max(defense, endurance), Mathf.Max(strength, crit)));
    }
    //Chest item constructor 1 arg
    public ChestItem(int level)
    {  
        SetItemName(RandomName());
        SetDefense(Random.Range(0, level+1));
        SetStrength(Random.Range(0, level+1));
        SetEndurance(Random.Range(0, level+1));
        SetCrit(Random.Range(0, level+1));
        SetReqLevel((int)Mathf.Max(Mathf.Max(defense, endurance), Mathf.Max(strength, crit)));
        SetGoldWorth(Random.Range(1, GetReqLevel()));
    }

    //methods
    public override string RandomName()
    {
        string[] first = { "Golden ", "Thick ", "Strong ", "Metal ", "Leather ", "Chain Mail ", "Wool ", "Scrappy ", "Worn ", "Used ", "Plate ", "Steel ", "Great "};
        string[] second = { "Chest Peice ", "Torso Armor ", "Pauldrons ", "Cuirass ", "Chest Armor ", "Personal Armor ", "Shirt ", "Plackart ", "Tunic ", "Breastplate ", "Vest ", "Chestplate "};
        string name = string.Concat(first[Random.Range(0, first.Length - 1)], second[Random.Range(0, second.Length - 1)]);
        return name;
    }

    //getters
    public float GetDefense()
    {
        return this.defense;
    }
    public float GetStrength()
    {
        return this.strength;
    }
    public float GetEndurance()
    {
        return this.endurance;
    }
    public float GetCrit()
    {
        return this.crit;
    }
    public int GetReqLevel()
    {
        return this.reqLevel;
    }

    //setters
    public void SetDefense(float defense)
    {
        this.defense = defense;
    }
    public void SetStrength(float strength)
    {
        this.strength = strength;
    }
    public void SetEndurance(float endurance)
    {
        this.endurance = endurance;
    }
    public void SetCrit(float crit)
    {
        this.crit = crit;
    }
    public void SetReqLevel(int reqLevel)
    {
        this.reqLevel = reqLevel;
    }
}
