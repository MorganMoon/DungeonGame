using UnityEngine;
using System.Collections;
using System;

public class Loot : MonoBehaviour {

    private Rigidbody2D physics;
    private GameItem loot;
    private int level = 1;

    void Awake()
    {
        physics = GetComponent<Rigidbody2D>();
    }
	// Use this for initialization
	void Start () {
        SetLoot();
        Spin();
	}

	// Update is called once per frame
	void Update () {

	}
    void FixedUpdate()
    {
        
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        Pickup(coll);   
    }

    //Methods
    void Pickup(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            coll.GetComponent<Player>().AddToInventory(loot);
            Destroy(this.gameObject);
        }
    }
    void Spin()
    {
        physics.AddTorque(20);
    }
    void SetLoot()
    {
        int rand = UnityEngine.Random.Range(1, 11);
        if (rand <= 6) { SetLoot(new GameItem()); }
        else if (rand == 7) { SetLoot(new HeadItem(level)); }
        else if (rand == 8) { SetLoot(new ChestItem(level)); }
        else if (rand == 9) { SetLoot(new LegItem(level)); }
        else if (rand == 10) { SetLoot(new WeaponItem(level)); }
        else { throw new Exception("Variable rand is: " + rand + " and it shouldnt be");  }
    }

    //getters
    public GameItem GetLoot()
    {
        return this.loot;
    }
    public int GetLevel()
    {
        return this.level;
    }

    //setters
    public void SetLoot(GameItem loot)
    {
        this.loot = loot;
    }
    public void SetLevel(int level)
    {
        this.level = level;
    }
}
