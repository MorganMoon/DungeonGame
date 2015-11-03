using UnityEngine;
using System.Collections;

[System.Serializable]
public class LegItem : GameItem
{

    //Head item advanced stats
    private float defense;
    private float strength;
    private float endurance;

    public LegItem(string itemName, int goldWorth, float defense, float strength, float endurance) : base(itemName, goldWorth)
    {
        SetDefense(defense);
        SetStrength(strength);
        SetEndurance(endurance);
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
}
