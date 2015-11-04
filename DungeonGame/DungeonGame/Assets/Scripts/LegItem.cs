using UnityEngine;
using System.Collections;

[System.Serializable]
public class LegItem : GameItem
{

    //Head item advanced stats
    private float defense;
    private float strength;
    private float endurance;
    private float crit;
    private int reqLevel;

    public LegItem(string itemName, int goldWorth, float defense, float endurance, float strength, float crit) : base(itemName, goldWorth)
    {
        SetDefense(defense);
        SetStrength(strength);
        SetEndurance(endurance);
        SetCrit(crit);
        SetReqLevel((int)Mathf.Max(Mathf.Max(defense, endurance), Mathf.Max(strength, crit)));
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
        return this.endurance;
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
