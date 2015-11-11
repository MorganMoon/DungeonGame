using UnityEngine;
using System.Collections.Generic;
using System.Threading;

public delegate void PathCompleteHandler(List<PathFinderNode> path);
public class PathSeeker : MonoBehaviour
{
    public event PathCompleteHandler OnPathComplete;
    public Grid UseGrid;
    public PathFinderFast finder;

    void Start()
    {
        finder = new PathFinderFast(UseGrid.mGrid);
    }

    public void FindPathFast(Vector2 start, Vector2 end)
    {
        if(finder == null)
            finder = new PathFinderFast(UseGrid.mGrid);

        finder.FindPath(UseGrid.GetPos(start), UseGrid.GetPos(end), FindPathCallback);
    }

    private void FindPathCallback(List<PathFinderNode> path)
    {
        if(OnPathComplete != null)
        {
            OnPathComplete(path);
        }
    }

    

    public void FindPath(Node endPos, System.Action<List<Node>> callback)
    {
        Node startPos = UseGrid.WorldPositionToNode(transform.position);
        new Thread(() =>
        {
            System.Diagnostics.Stopwatch time = new System.Diagnostics.Stopwatch();
            time.Start();

            List<Node> path = new List<Node>();
            List<Node> openNodes = new List<Node>();
            List<Node> closedNodes = new List<Node>();
            Dictionary<Node, Node> parents = new Dictionary<Node, Node>();
            openNodes.Add(startPos);

            while (openNodes.Count > 0)
            {
                Node currentNode = openNodes[0];

                if (currentNode == endPos)
                {
                    while (currentNode != startPos)
                    {
                        path.Add(currentNode);
                        currentNode = parents[currentNode];
                    }
                    path.Reverse();
                    break;
                }

                openNodes.Remove(currentNode);
                closedNodes.Add(currentNode);
                //Debug.Log(currentNode.neighbors.Count);
                foreach (Node neighbor in currentNode.neighbors)
                {
                    if (closedNodes.Contains(neighbor)) continue;
                    if (!neighbor.getWalkable()) continue;
                    //Debug.Log(neighbor.parent == null ? "yes":"no");
                    if (!parents.ContainsKey(neighbor))
                    {
                        neighbor.g = currentNode.g + GetDistance(neighbor, currentNode);
                        parents.Add(neighbor, currentNode);
                        openNodes.Add(neighbor);
                    }
                    else
                    {
                        if (currentNode.g + 10 < neighbor.g)
                        {
                            parents[neighbor] = currentNode;
                            neighbor.g = currentNode.g + 10;
                        }
                    }
                }
                //Debug.Log("Middle: " + (time.ElapsedMilliseconds / (float)1000));
                
            }
            time.Stop();
            Debug.Log("Path completed in " + (time.ElapsedMilliseconds / (float)1000) + " Seconds");
            callback(path);
        }).Start();
    }

    private int GetDistance(Node node1, Node node2)
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
