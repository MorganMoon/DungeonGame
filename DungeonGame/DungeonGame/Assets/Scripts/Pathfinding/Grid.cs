using UnityEngine;
using System.Collections.Generic;
using System;


[ExecuteInEditMode]
public class Grid : MonoBehaviour {

    #region GridStuff
    public bool updateGrid = false;
    public bool diagonalNeighbors = true;
    public LayerMask wall;
    Node[,] grid;
    public float nodeSize = 0.3f;
    public float characterSize = 1f;
    public Vector2 gridBoxSize = Vector2.zero;
    Vector2 gridSize = Vector2.zero;
    Vector2 adjustedSize = Vector2.zero;
    Vector2 bottomLeft = Vector2.zero;
    public Vector3 nodeDrawSize = new Vector3(0.05f, 0.05f, 0);

    public bool DrawGizmos = false;

    #endregion

    #region Unity CallBacks

    void Start () {
        CreateGrid();
    }

    void Update () {
    }

    void OnDrawGizmos() {

        if (DrawGizmos) {
            Gizmos.DrawWireCube(transform.position, (Vector3)adjustedSize);

            if (grid != null) {
                foreach (Node node in grid) {

                    Gizmos.color = !node.isWalkable ? Color.red : Color.blue;
                    Gizmos.DrawCube(node.position, nodeDrawSize);
                }
            }
        }
    }

    #endregion

    #region Grid Methods

    public Node FindNodeFromPosition(Vector2 position) {

        int x = Mathf.RoundToInt( (position.x - bottomLeft.x) / nodeSize);
        int y = Mathf.RoundToInt( (position.y - bottomLeft.y) / nodeSize);

        if (x > -1 && x < grid.GetLength(0) && y > -1 && y < grid.GetLength(1)) {
            if (grid[x, y].isWalkable)
                return grid[x, y];
            else
            {
                for (int xt = -1; xt < 3; xt++)
                {
                    for (int yt = -1; yt < 3; yt++)
                    {
                        if(grid[x + xt, y + yt].isWalkable)
                            return grid[x + xt, y + yt];
                    }
                }
            }
        }

        return null;
    }

    void CreateGrid() {
        Debug.Log("grid created");
        if (!(gridBoxSize.x < 0 || gridBoxSize.y < 0)) {

            gridSize.x = (float)Mathf.RoundToInt(gridBoxSize.x / nodeSize);
            gridSize.y = (float)Mathf.RoundToInt(gridBoxSize.y / nodeSize);
            adjustedSize.x = gridSize.x * nodeSize;
            adjustedSize.y = gridSize.y * nodeSize;

            grid = new Node[(int)gridSize.x, (int)gridSize.y];

            bottomLeft = (Vector2)transform.position - Vector2.right * (adjustedSize.x * 0.5f - nodeSize * 0.5f) - Vector2.up * (adjustedSize.y * 0.5f - nodeSize * 0.5f);
            
            for (int x = 0; x < (int)gridSize.x; x++) {
                for (int y = 0; y < (int)gridSize.y; y++) {
                    grid[x, y] = new Node(bottomLeft + Vector2.right * x * nodeSize + Vector2.up * y * nodeSize, x, y);
                }
            }
        }
        NodeNeighbors();
        NodeCollisions();
    }

    void NodeCollisions() {

        float _charSize = characterSize * 0.5f;
        Vector2 bottomLeftCorner = -Vector2.right * _charSize - Vector2.up * _charSize;
        Vector2 topRightCorner = Vector2.right * _charSize + Vector2.up * _charSize;

        foreach (Node node in grid) {
            node.isWalkable = !Physics2D.OverlapArea(node.position + bottomLeftCorner, node.position + topRightCorner, wall); //node.position, _charSize);
        }
    }

    void NodeNeighbors() {

        int x;
        int y;

        foreach (Node node in grid) {

            x = node.x - 1; y = node.y; //left
            if (x > -1) { node.neighbors.Add(grid[x, y]); }
            x = node.x + 1; y = node.y; //right
            if (x < grid.GetLength(0)) { node.neighbors.Add(grid[x, y]); }

            x = node.x; y = node.y - 1; //down
            if (y > -1) { node.neighbors.Add(grid[x, y]); }
            x = node.x; y = node.y + 1; //up
            if (y < grid.GetLength(1)) { node.neighbors.Add(grid[x, y]); }

            if (diagonalNeighbors) {
                x = node.x - 1; y = node.y - 1; //down left
                if (y > -1 && x > -1) { node.neighbors.Add(grid[x, y]); }
                x = node.x + 1; y = node.y - 1; //down right
                if (y > -1 && x < grid.GetLength(0)) { node.neighbors.Add(grid[x, y]); }

                x = node.x - 1; y = node.y + 1; //up left
                if (y < grid.GetLength(1) && x > -1) { node.neighbors.Add(grid[x, y]); }
                x = node.x + 1; y = node.y + 1; //up right
                if (y < grid.GetLength(1) && x < grid.GetLength(0)) { node.neighbors.Add(grid[x, y]); }
            }

        }
    }
    #endregion
}