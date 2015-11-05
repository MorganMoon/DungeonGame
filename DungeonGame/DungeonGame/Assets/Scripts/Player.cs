using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

    //Basic player stats
    private float maxhp = 10.0f; //Player max health
    private float curhp; //Player current health
    private int level = 1; //Player Level
    private int gold = 1; //Player money

    //Advanced player stats
    private float defense; //Player defense stat -- health doesnt go down as much when hit?
    private float endurance; //Player endurance stat -- gives more max hp?
    private float strength; //Player stength stat  -- hits harder?
    private float crit; // Player crit stat -- chance of multiplying hit by 2?
    

    //Player gear
    private HeadItem helmet; //Player Helmet Gear
    private ChestItem chest; //Player Chest Gear
    private LegItem legs; //Player Leg Gear
    private WeaponItem weapon; //Players weapon

    private List<GameItem> inventory = new List<GameItem>(); //Players inventory of gear/crap

	// Use this for initialization
	void Start () {
        this.curhp = GetMaxHP(); //heals player all the way at start

        //all stuff below temporary for testing
        helmet = new HeadItem("Worn Leather Helmet", 1, 0, 0, 0, 0);
        chest = new ChestItem("Worn Leather Chest", 1, 0, 0, 0, 0);
        //legs = new LegItem("Steel Plated Legs", 5, 2, 7, 3);
        weapon = new WeaponItem("Worn Practice Sword", 1, 0, 0, 0, 0, 1, 2);

        inventory.Add(new LegItem("Steel Plated Legs", 5, 2, 7, 3, 4));
        legs = (LegItem)inventory[0];

        UseEndurance();
        CheckLevelReq();
        SetStats(); 
	}
	
	// Update is called once per frame
	void Update () {
        RegenHP();
	}

    //Methods
    public void SetStats() //Method SetStats takes all of the stats from equipt armor and applies it to the player
    {
        this.defense = helmet.GetDefense() + chest.GetDefense() + legs.GetDefense() + weapon.GetDefense();
        this.strength = helmet.GetStrength() + chest.GetStrength() + legs.GetStrength() + weapon.GetStrength();
        this.endurance = helmet.GetEndurance() + chest.GetEndurance() + legs.GetEndurance() + weapon.GetEndurance();
        this.crit = helmet.GetCrit() + chest.GetCrit() + legs.GetCrit() + weapon.GetCrit();
    }
    public void UseEndurance() //Method UseEndurance takes eache endurance point and adds it to the players float maxHP
    {
        maxhp += this.endurance;
    }
    public void RegenHP() //Method RegenHP checks if curHp is less than max HP and if it is, it heals the player and stops at maxhp
    {
        if (GetCurHP() < GetMaxHP())
        {
            this.curhp += Time.deltaTime / 5;
        }
        else if(GetCurHP() >= GetMaxHP())
        {
            this.curhp = GetMaxHP();
        }
    }
    public void CheckLevelReq()
    {
        if (GetHelmet().GetReqLevel() > GetLevel())
        {
            Debug.Log(GetHelmet().GetItemName() + "required level: " + GetHelmet().GetReqLevel() + " Player is only level: " + GetLevel());
            inventory.Add(GetHelmet());
            this.helmet = null;
        }
        if (GetChest().GetReqLevel() > GetLevel())
        {
            Debug.Log(GetChest().GetItemName() + " required level: " + GetChest().GetReqLevel() + " Player is only level: " + GetLevel());
            inventory.Add(GetChest());
            this.chest = null;
        }
        if (GetLegs().GetReqLevel() > GetLevel())
        {
            Debug.Log(GetLegs().GetItemName() + " required level: " + GetLegs().GetReqLevel() + " Player is only level: " + GetLevel());
            inventory.Add(GetLegs());
            this.legs = null;
        }
        if (GetWeapon().GetReqLevel() > GetLevel())
        {
            Debug.Log(GetWeapon().GetItemName() + " required level: " + GetWeapon().GetReqLevel() + " Player is only level: " + GetLevel());
            inventory.Add(GetWeapon());
            this.weapon = null;
        }
    }


    //Getters
    public float GetMaxHP() //Gets current float 'maxhp'
    {
        return this.maxhp;
    }
    public float GetCurHP() //Gets current float 'curhp'
    {
        return this.curhp;
    }
    public int GetLevel() //Gets current int 'level'
    {
        return this.level;
    }
    public int GetGold() //Gets current int 'gold'
    {
        return this.gold;
    }
    public float GetDefense() //Gets current float 'defense'
    {
        return this.defense;
    }
    public float GetStrength() //Gets current float 'strength'
    {
        return this.strength;
    }
    public float GetCrit() //Gets current float 'crit'
    {
        return this.crit;
    }
    public float GetEndurance() // Gets current float 'endurance'
    {
        return this.endurance;
    }
    public HeadItem GetHelmet() //Gets current HeadItem 'helmet'
    {
        return this.helmet;
    }
    public ChestItem GetChest() //Gets current ChestItem 'chest'
    {
        return this.chest;
    }
    public LegItem GetLegs() //Gets current LegItem 'legs'
    {
        return this.legs;
    }
    public WeaponItem GetWeapon() //Gets current WeaponItem 'weapon'
    {
        return this.weapon;
    }
}
