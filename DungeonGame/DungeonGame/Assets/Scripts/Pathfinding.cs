using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pathfinding : MonoBehaviour {

    Grid grid;
    void Awake()
    {
        grid = GetComponent<Grid>();
        
    }
	// Use this for initialization
	void Start () {
        
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
        openNodes.Add(startPos);

        while (openNodes.Count > 0)
        {
            Node currentNode = openNodes[0];
            float f = currentNode.f;
            for (int i = 1; i < openNodes.Count; i++)
            {
                if (openNodes[i].f < f)
                {
                    currentNode = openNodes[i];
                    f = currentNode.f;
                }
            }

            openNodes.Remove(currentNode);
            closedNodes.Add(currentNode);

            foreach (Node neighbor in currentNode.neighbors)
            {
                if (closedNodes.Contains(neighbor)) continue;
                if (!neighbor.getWalkable()) continue;
                if (neighbor.parent == null)
                {
                    neighbor.g = currentNode.g + GetDistance(neighbor, currentNode);
                    neighbor.parent = currentNode;
                    neighbor.h = GetDistance(neighbor, endPos);
                    openNodes.Add(neighbor);
                }
                else
                {
                    if (currentNode.g + 10 < neighbor.g)
                    {
                        neighbor.parent = currentNode;
                        neighbor.g = currentNode.g + 10;
                    }
                }
            }

            if (currentNode == endPos)
            {
                while (currentNode != startPos)
                {
                    path.Add(currentNode);
                    currentNode = currentNode.parent;
                }
                path.Reverse();
            }
        }
        return path;
    }

    int GetDistance(Node node1, Node node2)
    {
        int distX = Mathf.Abs((int)node1.GetGridPos().x - (int)node2.GetGridPos().x);
        int distY = Mathf.Abs((int)node1.GetGridPos().y - (int)node2.GetGridPos().y);

        if (distX > distY)
        {
            return 14 * distY + 10 * (distX - distY);
        }
        return 14 * distX + 10 * (distY - distX);
    }
}
