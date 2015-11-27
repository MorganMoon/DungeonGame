using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Node
{

    public Vector2 position;
    public int x, y;
    public bool isWalkable = true;
    public float f, g, h, c = 1;
    public Node parent = null;

    public List<Node> neighbors = new List<Node>();

    public UInt64 Key { get { return (((UInt64)(UInt32)x) << 32) | (UInt64)(UInt32)y; } }

    public Node(Vector2 pos, int _x, int _y)
    {
        position = pos;
        x = _x;
        y = _y;
    }
}
