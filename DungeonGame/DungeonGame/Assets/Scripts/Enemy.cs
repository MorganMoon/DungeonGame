using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : Character
{

    //Enemy Control variables
    private GameObject target;
    private bool seeTarget;
    public GameObject loot;
    public LayerMask walls;
    List<PathFinderNode> pathFind = new List<PathFinderNode>();
    private PathSeeker seeker;

    private float pathTimer = 0;

    // Use this for initialization
    void Start()
    {
        seeker = GetComponent<PathSeeker>();
        seeker.OnPathComplete += this.OnFastPathComplete;
        target = GameObject.FindGameObjectWithTag("Player");
        SetLevel(target.GetComponent<Player>().GetLevel());

        SetHelmet(new HeadItem(GetLevel()));
        SetChest(new ChestItem(GetLevel()));
        SetLegs(new LegItem(GetLevel()));
        SetWeapon(new WeaponItem(GetLevel()));

        SetStats();
        UseEndurance();
        SetCurHP(GetMaxHP()); //heals player all the way at start

        //seeker.FindPath(seeker.UseGrid.WorldPositionToNode(target.transform.position), OnPathComplete);
    }

    private void OnPathComplete(List<Node> path)
    {
        //pathFind = path;
        //Debug.Log("Found path: " + path.Count);
    }

    // Update is called once per frame
    void Update()
    {
        if (pathTimer <= 0)
        {
            //seeker.FindPath(seeker.UseGrid.WorldPositionToNode(target.transform.position), OnPathComplete);
            seeker.FindPathFast(transform.position, target.transform.position);
            pathTimer = 0.5f;
        }
        pathTimer -= Time.deltaTime;
    }

    private void OnFastPathComplete(List<PathFinderNode> nodes)
    {
        pathFind = nodes;
        Debug.Log("Found fast path: " + nodes.Count);
    }

    void FixedUpdate()
    {
        seeTarget = CanSeeTarget();
        LookAtTarget();

        for (int i = 0; i < pathFind.Count - 1; i++)
        {
            Debug.Log(seeker.UseGrid.NodeToWorldPosition(pathFind[i].GetParentPosition()));
            Debug.DrawLine(seeker.UseGrid.NodeToWorldPosition(pathFind[i].GetPosition()), seeker.UseGrid.NodeToWorldPosition(pathFind[i + 1].GetPosition()), Color.green);
        }

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
