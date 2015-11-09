using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node {
    private Vector2 position;
    public List<Node> neighbors = new List<Node>();
    private bool walkable = true;
    public float g, h, c = 1;
    public float f;
    public Node parent = null;

    //Constructor 1-arg
    public Node(Vector2 position)
    {
        this.position = position;
    }

    //getters
    public Vector2 GetPosition() { return this.position; }
    public bool GetWalkable() { return this.walkable;  }
    public bool getWalkable() { return this.walkable; }

    //setters
    public void setWalkable(bool walkable)
    {
        this.walkable = walkable;
    }
    public void setG(float g) { this.g = g; }
    public void setH(float h) { this.h = h; }

}
