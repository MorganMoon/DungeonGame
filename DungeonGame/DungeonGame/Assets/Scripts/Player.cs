using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    //Basic player stats
    private float maxhp; //Player max health
    private float curhp; //Player current health
    private int level = 1; //Player Level
    private int gold = 1; //Player money

    //Advanced player stats
    private float defense; //Player defense stat -- health doesnt go down as much when hit?
    private float strength; //Player stength stat  -- hits harder?
    private float endurance; //Player endurance stat -- gives more max hp?

    //Player gear
    private HeadItem helmet; //Player Helmet Gear
    private ChestItem chest; //Player Chest Gear
    private LegItem legs; //Player Leg Gear
    private WeaponItem weapon; //Players weapon

    private GameItem[] inventory = new GameItem[20]; //Players inventory of gear/crap

	// Use this for initialization
	void Start () {
        this.curhp = GetMaxHP(); //heals player all the way at start

        //temporary for testing
        helmet = new HeadItem("Helmet Of Doom", 5, 3, 2, 1);
        chest = new ChestItem("Chain Mail Chest", 5, 5, 1, 3);
        legs = new LegItem("Steel Plated Legs", 5, 2, 7, 3);
        weapon = new WeaponItem("Sword Of Smite", 5, 2, 5, 1);

        inventory[2] = new LegItem("Worthless pants", 1, 2, 3, 2);
	}
	
	// Update is called once per frame
	void Update () {
	
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
