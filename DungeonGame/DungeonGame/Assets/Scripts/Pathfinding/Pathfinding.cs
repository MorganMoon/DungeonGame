using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum Heuristics { Manhattan, Diagonal, MaxDxDy }
[System.Serializable]
public class Pathfinding
{

    #region heuristics

    float D = 1f;
    private static float D2 = Mathf.Sqrt(2);

    private float MaxDXDY(Node position)
    {
        return 2 * (Mathf.Max(Mathf.Abs(position.x - end.x), Mathf.Abs(position.y - end.y)));
    }

    private int Manhattan(Node position)
    {
        return Mathf.Abs(position.x - end.x) + Mathf.Abs(position.y - end.y);
    }

    private float Diagonal(Node node)
    {

        float dx = Mathf.Abs(node.x - end.x);
        float dy = Mathf.Abs(node.y - end.y);

        return (D * (dx + dy) + (D2 - 2 * D) * Mathf.Min(dx, dy));
    }

    private float TieBreak(float h, Node node)
    {

        int dx1 = node.x - end.x;
        int dy1 = node.y - end.y;
        int dx2 = start.x - end.x;
        int dy2 = start.y - end.y;
        int cross = Mathf.Abs(dx1 * dy2 - dx2 * dy1);

        return (int)(h + cross * 0.001f);
    }
    #endregion

    //Serialized Types
    public Heuristics heuristic = Heuristics.Diagonal;
    public bool tieBreaking = true;
    public bool punishDiagonal = true;

    Node start;
    Node end;

    public List<Node> ReturnPath(Node _start, Node _end)
    {
        start = _start;
        end = _end;

        List<Node> open = new List<Node>();
        Dictionary<UInt64, Node> closed = new Dictionary<UInt64, Node>();
        // List<Node> closed = new List<Node>();

        //if start or end is not walkable... we won't find a path! Finished.
        if (!start.isWalkable || !end.isWalkable) { return open; }

        start.g = 0; start.f = 0; start.parent = null; //Reset start values
        open.Add(start);

        Node current = start;
        int currentIndex = 0;
        float punishAmount = heuristic == Heuristics.MaxDxDy ? 2.41f : 1.41f;
        while (open.Count > 0)
        {

            current = open[0]; currentIndex = 0;
            for (int i = 1; i < open.Count; i++)
            {
                if (open[i].f < current.f)
                {
                    current = open[i]; currentIndex = i;
                }
            }

            //foreach (Node searchedNode in open) {
            //    if (searchedNode.f < current.f) { //find lowest fscore
            //        current = searchedNode;
            //    }
            //}

            if (current == end) { break; } //Path found

            open.RemoveAt(currentIndex);
            closed[current.Key] = current;
            int count = current.neighbors.Count;
            for (int i = 0; i < count; i++)
            {
                //foreach (Node neighbor in current.neighbors) {
                Node neighbor = current.neighbors[i];

                if (!neighbor.isWalkable) { continue; } //Not pathable
                if (closed.ContainsKey(neighbor.Key)) continue; //Already calculated this


                float gScore = current.g;
                if (punishDiagonal && i > 3)
                { //If we neighbor is a diagonal, we punish it.
                    gScore += D * punishAmount; //we punish it by 2.41f 2^(1/2) + 1
                }
                else
                {
                    gScore += D; //D is part of heuristic, default it should be 1
                }

                float calculatedHeuristic = 0;
                switch (heuristic)
                { //WORK IN PROGRESS

                    case Heuristics.Diagonal: calculatedHeuristic = Diagonal(neighbor); break;
                    case Heuristics.Manhattan: calculatedHeuristic = Manhattan(neighbor); break;
                    case Heuristics.MaxDxDy: calculatedHeuristic = MaxDXDY(neighbor); break;
                }


                if (!open.Contains(neighbor))
                { //it's new... reset the values

                    open.Add(neighbor);
                    // neighbor.f = gScore + calculatedHeuristic;

                }
                else if (gScore >= neighbor.g) { continue; }
                if (tieBreaking) { calculatedHeuristic = TieBreak(calculatedHeuristic, neighbor); }

                neighbor.parent = current;
                neighbor.g = gScore;
                neighbor.f = neighbor.g + (int)calculatedHeuristic;

            }
        }

        //Reconstruct Path
        List<Node> path = new List<Node> { current };
        if (current.parent != null && current == end)
        {
            while (current != start)
            {
                path.Add(current.parent);
                current = current.parent;
            }
        }
        //Reverse path so [1] is this first step the character has to take
        path.Reverse();

        return path;
    }

    //float isDiagonal(Node current, Node neighbor) {

    //    int dx = Mathf.Abs(current.x - neighbor.x);
    //    int dy = Mathf.Abs(current.y - neighbor.y);

    //    return Mathf.Min(dx, dy) == 0 ? 1 : 2.41f;
    //}
}
