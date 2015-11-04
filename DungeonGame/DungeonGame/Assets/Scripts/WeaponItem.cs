using UnityEngine;
using System.Collections;

[System.Serializable]
public class WeaponItem : GameItem
{

    //Head item advanced stats
    private float defense; //Weapon defense stat
    private float strength; //Weapon strength stat
    private float endurance; //Weapon endurance stat
    private float crit; //Weapon crit stat
    private int reqLevel; // Weapon required level to equip

    //weapon stats
    private Vector2 damage; //Weapon damage ranges (#-#)

    //WeaponItem constructor
    public WeaponItem(string itemName, int goldWorth, float defense, float endurance, float strength, float crit, float minDamage, float maxDamage) : base(itemName, goldWorth)
    {
        SetDefense(defense);
        SetStrength(strength);
        SetEndurance(endurance);
        SetCrit(crit);
        SetDamage(minDamage, maxDamage);
        SetReqLevel((int)Mathf.Max(Mathf.Max(defense, endurance), Mathf.Max(strength, crit)));
    }

    //getters
    public float GetDefense() //Gets current float 'defense'
    {
        return this.defense;
    }
    public float GetStrength() //Gets current float 'strength'
    {
        return this.strength;
    }
    public float GetEndurance() //Gets current float 'endurance'
    {
        return this.endurance;
    }
    public float GetCrit() //Gets current float 'crit'
    {
        return this.endurance;
    }
    public Vector2 getDamage() //Gets current Vector2 'damage'
    {
        return this.damage;
    }
    public int GetReqLevel() //Gets current int 'reqLevel'
    {
        return this.reqLevel;
    }

    //setters
    public void SetDefense(float defense) //Sets float 'defense'
    {
        this.defense = defense;
    }
    public void SetStrength(float strength) //Sets float 'strength'
    {
        this.strength = strength;
    }
    public void SetEndurance(float endurance) //Sets float 'endurance'
    {
        this.endurance = endurance;
    }
    public void SetCrit(float crit) //Sets float 'crit'
    {
        this.crit = crit;
    }
    public void SetDamage(float minDamage, float maxDamage) //sets Vector2 'damage'
    {
        this.damage = new Vector2(minDamage, maxDamage);
    }
    public void SetReqLevel(int reqLevel) //Sets int 'reqLevel'
    {
        this.reqLevel = reqLevel;
    }
}
