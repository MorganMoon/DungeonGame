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
    private GameItem helmet; //Player Helmet Gear
    private GameItem chest; //Player Chest Gear
    private GameItem legs; //Player Leg Gear
    private GameItem weapon; //Players weapon

	// Use this for initialization
	void Start () {
        this.curhp = GetMaxHP();

        //temporary
        helmet = new GameItem(5);
        chest = new GameItem(5);
        legs = new GameItem(5);
        weapon = new GameItem(5);
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
    public GameItem GetHelmet() //Gets current HeadItem 'helmet'
    {
        return this.helmet;
    }
    public GameItem GetChest() //Gets current ChestItem 'chest'
    {
        return this.chest;
    }
    public GameItem GetLegs() //Gets current LegItem 'legs'
    {
        return this.legs;
    }
    public GameItem GetWeapon() //Gets current WeaponItem 'weapon'
    {
        return this.weapon;
    }
}
