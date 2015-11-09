using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pathfinding : MonoBehaviour {

	// Use this for initialization
	void Start () {
        List<Node> path = FindPath(GetComponent<Grid>().grid[0,0], GetComponent<Grid>().grid[10,15]);
        Debug.Log(path.Count);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public List<Node> FindPath(Node startPos, Node endPos)
    {
        List<Node> path = new List<Node>();
        List<Node> openNodes = new List<Node>();
        List<Node> closedNodes = new List<Node>();
        startPos.g = 0;
        startPos.f = Vector2.Distance(startPos.GetPosition(), endPos.GetPosition()); // change to grid distance
        openNodes.Add(startPos);
        Node currentNode = new Node(Vector2.zero);
        while (openNodes.Count > 0)
        {
            float lowestScore = float.MaxValue;
            foreach(Node node in openNodes){
                if (node.f < lowestScore)
                {
                    currentNode = node;
                    lowestScore = currentNode.f;
                }
            }
            if (currentNode == endPos)
            {
                closedNodes.Add(currentNode);
                break;
            }
            else
            {
                closedNodes.Add(currentNode);
                openNodes.Remove(currentNode);
                foreach (Node neighbor in currentNode.neighbors)
                {
                    if (neighbor.parent == null)
                    {
                        neighbor.g = neighbor.c + currentNode.g;
                        neighbor.h = Vector2.Distance(neighbor.GetPosition(), endPos.GetPosition());
                        neighbor.f = neighbor.g + neighbor.h;
                        neighbor.parent = currentNode;
                        openNodes.Add(neighbor);
                    }
                    else
                    {
                        if (currentNode.g + neighbor.c < neighbor.g)
                        {
                            neighbor.g = neighbor.c + currentNode.g;
                            neighbor.h = Vector2.Distance(neighbor.GetPosition(), endPos.GetPosition());
                            neighbor.f = neighbor.g + neighbor.h;
                            neighbor.parent = currentNode;
                        }
                    }
                }
            }
        }
        path.Add(currentNode);
        while (currentNode.parent != null)
        {
            currentNode = currentNode.parent;
            path.Add(currentNode);
        }

        return path;
    }
}
