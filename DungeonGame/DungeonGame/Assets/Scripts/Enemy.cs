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

    //Pathfinding
    private Pathfinding pathfinding = new Pathfinding();
    private Grid grid;

    Node characterNode; // this
    Node remCharacterNode;
    Node targetNode; // target / player
    Node remTargetNode;
    Node[] path; //The path
    private float pathTimer = 0;
    private int pathIndex = 0;

    // Use this for initialization
    void Start()
    {
        grid = (Grid)GameObject.FindGameObjectWithTag("Pathfinder").GetComponent<Grid>();
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

    // Update is called once per frame
    void Update()
    {
        if (pathTimer <= 0)
        {
            FindPath();
            pathTimer = 1f;
        }
        pathTimer -= Time.deltaTime;
    }

    void FixedUpdate()
    {
        seeTarget = CanSeeTarget();
        LookAtTarget();
        FollowPath();
    }

    //Methods 
    void FollowPath()
    {
        characterNode = grid.FindNodeFromPosition(transform.position);
        if (path == null) return;
        if (pathIndex < path.Length)
        {
            transform.position = Vector2.MoveTowards(transform.position, path[pathIndex].position, Time.deltaTime * Random.Range(1, 6));
        }
        if (characterNode.position == path[pathIndex].position)
        {
            pathIndex++;
        }
    }
    //Findpath fills Node[] path with a path of nodes
    void FindPath()
    {
        if (target)
            targetNode = grid.FindNodeFromPosition(target.transform.position);
        if (characterNode != null && targetNode != null && (remCharacterNode != characterNode || remTargetNode != targetNode))
        {
            path = pathfinding.ReturnPath(characterNode, targetNode).ToArray();
            remCharacterNode = characterNode;
            remTargetNode = targetNode;
        }
    }
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
