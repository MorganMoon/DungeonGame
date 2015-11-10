using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pathfinding : MonoBehaviour {

	// Use this for initialization
	void Start () {
        List<Node> path = FindPath(GetComponent<Grid>().grid[0, 0], GetComponent<Grid>().grid[35, 52]);
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
        openNodes.Add(startPos);
        while (openNodes.Count > 0)
        {
            Node currentNode = openNodes[0];
            for (int i = 1; i < openNodes.Count; i++)
            {
                if (openNodes[i].f < currentNode.f || openNodes[i].f == currentNode.f && openNodes[i].h < currentNode.h)
                {
                    currentNode = openNodes[i];
                }
            }
            openNodes.Remove(currentNode);
            closedNodes.Add(currentNode);

            if (currentNode == endPos)
            {
                while (currentNode != startPos)
                {
                    path.Add(currentNode);
                    currentNode = currentNode.parent;
                }
                path.Reverse();
            }

            foreach(Node neighbor in currentNode.neighbors){
                if (!neighbor.getWalkable() || closedNodes.Contains(neighbor))
                {
                    continue;
                }

                int costToNeighbor = currentNode.g + GetDistance(currentNode, neighbor);
                if(costToNeighbor < neighbor.g || openNodes.Contains(neighbor)){
                    neighbor.g = costToNeighbor;
                    neighbor.h = GetDistance(neighbor, endPos);
                    neighbor.parent = currentNode;

                    if(!openNodes.Contains(neighbor)){
                        openNodes.Add(neighbor);
                    }
                }

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
