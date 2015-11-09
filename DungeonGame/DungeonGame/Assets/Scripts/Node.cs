using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node {
    private Vector2 position;
    public List<Node> neighbors;
    private bool walkable = true;
    private float g, h, c = 1;

    //Constructor 1-arg
    public Node(Vector2 position)
    {
        this.position = position;
    }

    //getters
    public Vector2 GetPosition() { return this.position; }
    public bool GetWalkable() { return this.walkable;  }
    public float GetG() { return this.g; }
    public float GetH() { return this.h; }
    public float GetC() { return this.c; }
    public float GetF() { return GetG() + GetH(); }
    public bool getWalkable() { return this.walkable; }

    //setters
    public void setWalkable(bool walkable)
    {
        this.walkable = walkable;
    }

}
