using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character : MonoBehaviour {

    //Basic control variables
    private float speed = 5;

    //Basic player stats
    private float maxhp = 10.0f; //Player max health
    private float curhp; //Player current health
    private int level = 1; //Player Level
    

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

    void Awake()
    {

    }

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //Methods
    public void SetStats() //Method SetStats takes all of the stats from equipt armor and applies it to the player
    {
        //reset stats
        SetDefense(0); SetStrength(0); SetEndurance(0); SetCrit(0);
        //Set Helmet stats
        if (GetHelmet() != null)
        {
            SetDefense(GetDefense() + GetHelmet().GetDefense());
            SetStrength(GetStrength() + GetHelmet().GetStrength());
            SetEndurance(GetEndurance() + GetHelmet().GetEndurance());
            SetCrit(GetCrit() + GetHelmet().GetCrit());
        }
        if (GetChest() != null)
        {
            SetDefense(GetDefense() + GetChest().GetDefense());
            SetStrength(GetStrength() + GetChest().GetStrength());
            SetEndurance(GetEndurance() + GetChest().GetEndurance());
            SetCrit(GetCrit() + GetChest().GetCrit());
        }
        if (GetLegs() != null)
        {
            SetDefense(GetDefense() + GetLegs().GetDefense());
            SetStrength(GetStrength() + GetLegs().GetStrength());
            SetEndurance(GetEndurance() + GetLegs().GetEndurance());
            SetCrit(GetCrit() + GetLegs().GetCrit());
        }
        if (GetWeapon() != null)
        {

            SetDefense(GetDefense() + GetWeapon().GetDefense());
            SetStrength(GetStrength() + GetWeapon().GetStrength());
            SetEndurance(GetEndurance() + GetWeapon().GetEndurance());
            SetCrit(GetCrit() + GetWeapon().GetCrit());
        }
    }
    public void UseEndurance() //Method UseEndurance takes eache endurance point and adds it to the players float maxHP
    {
        SetMaxHP(GetMaxHP() + GetEndurance());
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
    public float GetSpeed() //Gets current float 'speed'
    {
        return this.speed;
    }

    //setters
    public void SetMaxHP(float maxhp) //Sets current float 'maxhp'
    {
        this.maxhp = maxhp;
    }
    public void SetCurHP(float curhp) //Sets current float 'curhp'
    {
        this.curhp = curhp;
    }
    public void SetLevel(int level) //Sets current int 'level'
    {
        this.level = level;
    }
    public void SetDefense(float defense) //Sets current float 'defense'
    {
        this.defense = defense;
    }
    public void SetStrength(float strength) //Sets current float 'strength'
    {
        this.strength = strength;
    }
    public void SetCrit(float crit) //Sets current float 'crit'
    {
        this.crit = crit;
    }
    public void SetEndurance(float endurance) // Sets current float 'endurance'
    {
        this.endurance = endurance;
    }
    public void SetHelmet(HeadItem helmet) //Sets current HeadItem 'helmet'
    {
        this.helmet = helmet;
    }
    public void SetChest(ChestItem chext) //Sets current ChestItem 'chest'
    {
        this.chest = chext;
    }
    public void SetLegs(LegItem legs) //Sets current LegItem 'legs'
    {
        this.legs = legs;
    }
    public void SetWeapon(WeaponItem weapon) //Sets current WeaponItem 'weapon'
    {
        this.weapon = weapon;
    }
    public void SetSpeed(float speed) //Sets current float 'speed'
    {
        this.speed = speed;
    }
}
