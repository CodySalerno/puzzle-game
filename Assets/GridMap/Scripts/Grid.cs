using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private float cellSize;
    private int[,] gridArray;
    public GameObject blockspawner;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        gridArray = new int[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 10f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 10f);
            }
        }

        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 10f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 10f);

        /*SetCellValue(1, 1, 10);
        SetCellValue(width, height, 20);
        SetCellValue(width + 1, height, 30);
        SetCellValue(width, height + 1, 40);
        SetCellValue(-1, height, 50);
        SetCellValue(0, -1, 60);*/

        populateGrid();
    }

    private Vector2 GetWorldPosition(int x, int y)
    {
        return new Vector2(x, y) * cellSize;
    }
    private Vector2 GetCenterPosition(int x, int y)
    {
        return new Vector2(x, y) * cellSize + new Vector2(1, 1) * cellSize/2;
    }

    private void SetCellValue(int x, int y, int value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = value;
        }
    }

    public void populateGrid()
    {
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                GameObject block = Instantiate(blockspawner, GetCenterPosition(x, y), Quaternion.identity);
                // Additional setup for block if needed
            }
        }
    }
}
