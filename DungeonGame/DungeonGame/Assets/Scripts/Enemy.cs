using UnityEngine;
using System.Collections;

public class Enemy : Character {

    //Enemy Control variables
    private GameObject target;
    private bool seeTarget;
    public GameObject loot;
    public LayerMask walls;

	// Use this for initialization
	void Start () {
        target = GameObject.FindGameObjectWithTag("Player");
        SetLevel(target.GetComponent<Player>().GetLevel());

        SetHelmet(new HeadItem(GetLevel()));
        SetChest(new ChestItem(GetLevel()));
        SetLegs(new LegItem(GetLevel()));
        SetWeapon(new WeaponItem(GetLevel()));

        SetStats();
        UseEndurance();
        SetCurHP(GetMaxHP()); //heals player all the way at start
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void FixedUpdate()
    {
        seeTarget = CanSeeTarget();
        LookAtTarget();
    }

    //Methods 
    //CanSeeTarget returns bool value based off whether or not the target is visible to Enemy
    bool CanSeeTarget()
    {
        return !Physics2D.Linecast(transform.position, target.transform.position, walls);
    }
    //LookAtTarget looks at the target when it is visible
    void LookAtTarget()
    {
        if (seeTarget)
            transform.rotation = RotateTowardObject(target);
        else transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(Vector3.zero), Time.deltaTime * GetSpeed() / 2);
    }
    //returns a quaternion rotating on Z toward arg 1 'target'
    Quaternion RotateTowardObject(GameObject target)
    {
        return Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, Mathf.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x) * Mathf.Rad2Deg), Time.deltaTime * GetSpeed() / 2);
    }

    //visual debugging
    void OnDrawGizmos()
    {
        
    }
}
