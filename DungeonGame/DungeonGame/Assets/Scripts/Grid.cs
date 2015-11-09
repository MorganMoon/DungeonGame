using UnityEngine;
using System.Collections;
public class Grid : MonoBehaviour {

    Node[,] grid;
    public float nodeSize;
    public Vector2 size;
    private int gridSizeX, gridSizeY;
    private Vector2 adjustedSize;
    public LayerMask walls;
    void Awake()
    {
        gridSizeX = Mathf.RoundToInt(size.x / (nodeSize));
        gridSizeY = Mathf.RoundToInt(size.y / (nodeSize ));
        adjustedSize = new Vector2(gridSizeX * nodeSize, gridSizeY * nodeSize);
        CreateGrid();
        CalculateCollision();
    }

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        //foreach (Node neighbor in grid[5, 5].neighbors) Debug.DrawLine(grid[0, 0].GetPosition(), neighbor.GetPosition());
	
	}
    

    //methods
    void FindNeighbors(int x, int y)
    {
        if (grid[x + 1, y] != null) grid[x, y].neighbors.Add(grid[x + 1, y]); //right
        if (grid[x - 1, y] != null) grid[x, y].neighbors.Add(grid[x - 1, y]); //left
        if (grid[x, y + 1] != null) grid[x, y].neighbors.Add(grid[x, y + 1]); //top
        if (grid[x, y - 1] != null) grid[x, y].neighbors.Add(grid[x, y - 1]); //bottom
        if (grid[x + 1, y + 1] != null) grid[x, y].neighbors.Add(grid[x + 1, y + 1]); //top right
        if (grid[x - 1, y - 1] != null) grid[x, y].neighbors.Add(grid[x - 1, y - 1]); // bottom left
        if (grid[x + 1, y - 1] != null) grid[x, y].neighbors.Add(grid[x + 1, y - 1]); // bottom right
        if (grid[x - 1, y + 1] != null) grid[x, y].neighbors.Add(grid[x - 1, y + 1]); // top left   
    }
    void CalculateCollision()
    {
        foreach (Node node in grid)
        {
            bool walkable = !Physics2D.OverlapCircle(node.GetPosition(), 0.5f, walls);
            node.setWalkable(walkable);
        }
    }
    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector2 gridBottomLeft = (Vector2)transform.position - Vector2.right * (adjustedSize.x / 2  + nodeSize* 1.5f) - Vector2.up * (adjustedSize.y / 2 + nodeSize* 1.5f);

        for(int x = 0; x < gridSizeX; x++){
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector2 worldPosition = gridBottomLeft + Vector2.right * (x * nodeSize + nodeSize * 2) + Vector2.up * (y * nodeSize + nodeSize * 2);
                grid[x, y] = new Node(worldPosition);
                FindNeighbors(x, y);
            }
        }
    }

    //visual debugging
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(adjustedSize.x, adjustedSize.y, 1));
        if (grid != null)
        {
            foreach (Node node in grid)
            {
                Gizmos.color = node.getWalkable() ? Color.red : Color.blue;
                Gizmos.DrawCube(node.GetPosition(), new Vector3(0.1f, 0.1f, 1));
            }
        }
    }
}
