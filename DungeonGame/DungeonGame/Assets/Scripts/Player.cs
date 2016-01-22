using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Player : Character {

    //Player Control variables
    Vector3 mouseCamera = Vector3.zero;

    //Basic Player stats
    private int gold = 1; //Player money

    //Player Inventory
    private List<GameItem> inventory = new List<GameItem>(); //Players inventory of gear/crap

	// Use this for initialization
	void Start () {
        SetCurHP(GetMaxHP()); //heals player all the way at start
        //all stuff below temporary for testing
        SetHelmet(new HeadItem(1));
        SetChest(new ChestItem(1));
        SetLegs(new LegItem(1));
        SetWeapon(new WeaponItem(1));

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
    ///<summary>
    /// Adds GameItem 'item' into the player List<GameItem> inventory
    ///</summary>
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
    ///<summary>
    /// Gives control to the player, should be in FixedUpdate()
    ///</summary>
    public void GiveControl()
    {
        mouseCamera = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
        mouseCamera.z = 0;
        float arrowAngle = AngleLookAt(mouseCamera, transform.position);
        //transform.rotation = Quaternion.Euler(new Vector3(0, 0, arrowAngle));
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(0, 0, arrowAngle)), Time.deltaTime * GetSpeed()/2);
        GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Lerp(0, Input.GetAxis("Horizontal") * GetSpeed(), 0.7f), Mathf.Lerp(0, Input.GetAxis("Vertical") * GetSpeed(), 0.7f));
    }
    ///<summary>
    /// Method RegenHP checks if curHp is less than max HP and if it is, it heals the player and stops at maxhp
    ///</summary>
    public void RegenHP()
    {
        if (GetCurHP() < GetMaxHP())
        {
            SetCurHP(GetCurHP() + Time.deltaTime / 5);
        }
        else if(GetCurHP() >= GetMaxHP())
        {
            SetCurHP(GetMaxHP());
        }
    }
    ///<summary>
    /// Method CheckLevelReq gets the required level of each peice of equipt armor and will put it back in the inventory if it is too high of a level
    ///</summary>
    public void CheckLevelReq() 
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
    ///<summary>
    /// Attempts to put GameItem 'item' as an equipped item on the player
    ///</summary>
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

        CheckLevelReq();
    }

    //Debug Methods
    void OnDrawGizmos()
    { 
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 0.1f);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(mouseCamera, 0.2f); 
    }


    //getters
    public int GetGold() //Gets current int 'gold'
    {
        return this.gold;
    }

    //setters
    public void SetGold(int gold) //Gets current int 'gold'
    {
        this.gold = gold;
    }
}
