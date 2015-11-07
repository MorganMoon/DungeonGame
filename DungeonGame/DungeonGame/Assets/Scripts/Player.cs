using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Player : MonoBehaviour {

    //Player Control variables
    private float speed = 5;
    Vector3 mouseCamera = Vector3.zero;

    //Basic player stats
    private float maxhp = 10.0f; //Player max health
    private float curhp; //Player current health
    private int level = 5; //Player Level
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
        SetHelmet(new HeadItem(1));
        SetChest(new ChestItem(1));
        SetWeapon(new WeaponItem(1));
        SetLegs(new LegItem(1));

        CheckLevelReq();
        SetStats();
        UseEndurance();
	}
	
	// Update is called once per frame
	void Update () {
        RegenHP();
        GiveControl();

        //TESTING BELOW -- TEMPORARY
        if (Input.GetKeyDown(KeyCode.T))
        {
            Equip(inventory[0]);
        }

        
        
	}

    //Methods
    public void AddToInventory(GameItem item)
    {
        inventory.Add(item);
    }
    public float AngleLookAt(Vector3 p1, Vector3 p2) 
    { 
        float deltaY = p2.y - p1.y;
        float deltaX = p2.x - p1.x;
        return Mathf.Atan2(deltaY, deltaX) * 180 / Mathf.PI + 180; 
    }
    public void GiveControl()
    {
        mouseCamera = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
        mouseCamera.z = 0;
        float arrowAngle = AngleLookAt(mouseCamera, transform.position);
        //transform.rotation = Quaternion.Euler(new Vector3(0, 0, arrowAngle));
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(0, 0, arrowAngle)), Time.deltaTime * speed/2);
        GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Lerp(0, Input.GetAxis("Horizontal") * speed, 0.7f), Mathf.Lerp(0, Input.GetAxis("Vertical") * speed, 0.7f));
    }
    public void SetStats() //Method SetStats takes all of the stats from equipt armor and applies it to the player
    {
        //reset stats
        this.defense = 0; this.strength = 0; this.endurance = 0; this.crit = 0;
        //Set Helmet stats
        if (GetHelmet() != null)
        {
            this.defense += GetHelmet().GetDefense();
            this.strength += GetHelmet().GetStrength();
            this.endurance += GetHelmet().GetEndurance();
            this.crit += GetHelmet().GetCrit();
        }
        if (GetChest() != null)
        {
            this.defense += GetChest().GetDefense();
            this.strength += GetChest().GetStrength();
            this.endurance += GetChest().GetEndurance();
            this.crit += GetChest().GetCrit();
        }
        if (GetLegs() != null)
        {
            this.defense += GetLegs().GetDefense();
            this.strength += GetLegs().GetStrength();
            this.endurance += GetLegs().GetEndurance();
            this.crit += GetLegs().GetCrit();
        }
        if (GetWeapon() != null)
        {
            this.defense += GetWeapon().GetDefense();
            this.strength += GetWeapon().GetStrength();
            this.endurance += GetWeapon().GetEndurance();
            this.crit += GetWeapon().GetCrit();
        }
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
            SetCurHP(GetMaxHP());
        }
    }
    public void CheckLevelReq() //Method CheckLevelReq gets the required level of each peice of equipt armor and will put it back in the inventory if it is too high of a level
    {
        if (GetHelmet() != null && GetHelmet().GetReqLevel() > GetLevel())
        {
            Debug.Log(GetHelmet().GetItemName() + "required level: " + GetHelmet().GetReqLevel() + " Player is only level: " + GetLevel());
            inventory.Add(GetHelmet());
            SetHelmet(null);
        }
        if (GetChest() != null && GetChest().GetReqLevel() > GetLevel())
        {
            Debug.Log(GetChest().GetItemName() + " required level: " + GetChest().GetReqLevel() + " Player is only level: " + GetLevel());
            inventory.Add(GetChest());
            SetChest(null);
        }
        if (GetLegs() != null && GetLegs().GetReqLevel() > GetLevel())
        {
            Debug.Log(GetLegs().GetItemName() + " required level: " + GetLegs().GetReqLevel() + " Player is only level: " + GetLevel());
            inventory.Add(GetLegs());
            SetLegs(null);
        }
        if (GetWeapon() != null && GetWeapon().GetReqLevel() > GetLevel())
        {
            Debug.Log(GetWeapon().GetItemName() + " required level: " + GetWeapon().GetReqLevel() + " Player is only level: " + GetLevel());
            inventory.Add(GetWeapon());
            SetWeapon(null);
        }
    }
    public void Equip(GameItem item)
    {
        if (item is HeadItem)
        {
            inventory.Remove(item);
            if (GetHelmet() != null)
            {
                inventory.Add(GetHelmet());
            }
            SetHelmet((HeadItem)item);
        }
        else if (item is ChestItem)
        {
            inventory.Remove(item);
            if (GetChest() != null)
            {
                inventory.Add(GetChest());
            }
            SetChest((ChestItem)item);
        }
        else if (item is LegItem)
        {
            inventory.Remove(item);
            if (GetLegs() != null)
            {
                inventory.Add(GetLegs());
            }
            SetLegs((LegItem)item);
        }
        else if (item is WeaponItem)
        {
            inventory.Remove(item);
            if (GetWeapon() != null)
            {
                inventory.Add(GetWeapon());
            }
            SetWeapon((WeaponItem)item);
        }
        else
        {
            throw new Exception("Don't know how to equip " + item.GetType() + " " + item.GetItemName() +" Yet!");
        }
    }

    //Debug Methods
    void OnDrawGizmos()
    { 
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 0.1f);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(mouseCamera, 0.2f); 
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
    public float GetSpeed()
    {
        return this.speed;
    }

    //setters
    public void SetMaxHP(float maxhp) //Gets current float 'maxhp'
    {
        this.maxhp = maxhp;
    }
    public void SetCurHP(float curhp) //Gets current float 'curhp'
    {
        this.curhp = curhp;
    }
    public void SetLevel(int level) //Gets current int 'level'
    {
        this.level = level;
    }
    public void SetGold(int gold) //Gets current int 'gold'
    {
        this.gold = gold;
    }
    public void SetDefense(float defense) //Gets current float 'defense'
    {
        this.defense = defense;
    }
    public void SetStrength(float strength) //Gets current float 'strength'
    {
        this.strength = strength;
    }
    public void SetCrit(float crit) //Gets current float 'crit'
    {
        this.crit = crit;
    }
    public void SetEndurance(float endurance) // Gets current float 'endurance'
    {
        this.endurance = endurance;
    }
    public void SetHelmet(HeadItem helmet) //Gets current HeadItem 'helmet'
    {
        this.helmet = helmet;
    }
    public void SetChest(ChestItem chext) //Gets current ChestItem 'chest'
    {
        this.chest = chext;
    }
    public void SetLegs(LegItem legs) //Gets current LegItem 'legs'
    {
        this.legs = legs;
    }
    public void SetWeapon(WeaponItem weapon) //Gets current WeaponItem 'weapon'
    {
        this.weapon = weapon;
    }
    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

}
