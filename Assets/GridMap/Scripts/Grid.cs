using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;
using UnityEngine.UIElements;

public class Grid : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private float cellSize;
    public GameObject[,] gridArray;
    public GameObject blockspawner;
    public GameObject cursorSpawner;
    private int update_counter = 0;
    double tolerance = 1e-5; // You can adjust the tolerance as needed
    // Awake is called when the script instance is being loaded
    void Awake()
    {
        gridArray = new GameObject[width, height];

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

        for (int i = 0; i < 6; i++)
        {
            CreateRowStart(i);
        }
        Vector2 cursorVector2 = GetCenterPosition(3, 4);
        Vector3 cursorVector3 = new Vector3(cursorVector2.x-.5f, cursorVector2.y, -1);
        cursorSpawner.transform.position = cursorVector3;
    }

    private void Start()
    {

        BreakBlocks();
    }

    private void PrintArray()
    {

    }
    private Vector2 GetWorldPosition(int x, int y)
    {
        return new Vector2(x, y) * cellSize;
    }
    private Vector2 GetCenterPosition(int x, int y)
    {
        return new Vector2(x, y) * cellSize + new Vector2(1, 1) * cellSize/2;
    }

    private void SetCellValue(int x, int y, GameObject block)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = block;
        }
    }

    public void CreateRowStart(int y)
    {
        
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            /*for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                GameObject block = Instantiate(blockspawner, GetCenterPosition(x, y), Quaternion.identity);
                // Additional setup for block if needed
            }*/
            GameObject block = Instantiate(blockspawner, GetCenterPosition(x, y), Quaternion.identity);
            SetCellValue(x, y, block);
        }

    }
    public void CreateRow()
    {
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            /*for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                GameObject block = Instantiate(blockspawner, GetCenterPosition(x, y), Quaternion.identity);
                // Additional setup for block if needed
            }*/
            int y = 0;
            GameObject block = Instantiate(blockspawner, GetCenterPosition(x, y), Quaternion.identity);
            SetCellValue(x, y, block);
        }
    }

    public void IncreaseRow()
    {
        Debug.Log("increasing row");
        for (int x = gridArray.GetLength(0)-1; x >= 0; x--)
        {
            for (int y = gridArray.GetLength(1)-1; y >= 0; y--)
            {
                if (gridArray[x, y])
                {
                    try
                    {
                        gridArray[x, y + 1] = gridArray[x, y];
                    }
                    catch (IndexOutOfRangeException e)
                    {

                        Debug.Log("KILL ME! y = " + y);
                        UnityEditor.EditorApplication.isPlaying = false;
                    }
                    catch (Exception e)
                    {
                        Debug.LogException(e);
                    }
                }
            }
        }
        //check for block breaking
        BreakBlocks();
        CreateRow();
    }

    public void BreakBlocks()
    {
        Debug.Log("running break blocks");
        bool[,] blocksToBreak = new bool[width, height];
        for (int x = gridArray.GetLength(0) - 1; x >= 0; x--)
        {
            for (int y = gridArray.GetLength(1) - 1; y >= 1; y--)
            {
                if (gridArray[x, y])
                {
                    if (!(x == 0 || x == width - 1))
                    {
                        try
                        {
                            if (gridArray[x - 1, y].GetComponent<SpriteRenderer>().sprite == gridArray[x, y].GetComponent<SpriteRenderer>().sprite &&
                            gridArray[x, y].GetComponent<SpriteRenderer>().sprite == gridArray[x + 1, y].GetComponent<SpriteRenderer>().sprite)
                            {
                                blocksToBreak[x, y] = true;
                                blocksToBreak[x - 1, y] = true;
                                blocksToBreak[x + 1, y] = true;
                            }
                        }
                        catch (NullReferenceException e)
                        {

                        }
                    }
                    if (!(y == 1 || y == height - 1))
                    {
                        try
                        {
                            if (gridArray[x, y - 1].GetComponent<SpriteRenderer>().sprite == gridArray[x, y].GetComponent<SpriteRenderer>().sprite &&
                            gridArray[x, y].GetComponent<SpriteRenderer>().sprite == gridArray[x, y + 1].GetComponent<SpriteRenderer>().sprite)
                            {
                                blocksToBreak[x, y] = true;
                                blocksToBreak[x, y - 1] = true;
                                blocksToBreak[x, y + 1] = true;
                            }
                        }
                        catch (NullReferenceException e)
                        {

                        }
                    }
                }
            }
        }
        for (int x = blocksToBreak.GetLength(0) - 1; x >= 0; x--)
        {
            for (int y = blocksToBreak.GetLength(1) - 1; y >= 0; y--)
            {
                if (blocksToBreak[x, y])
                {
                    Destroy(gridArray[x, y]);
                    gridArray[x, y] = null;
                }
            }
        }
        //make blocks fall
        foreach (bool block in blocksToBreak)
        {
            if (block)
            {
                FallBlocks();
                break;
            }
        }
    }

    public void FallBlocks()
    {
        Debug.Log("running fall blocks");

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 2; y < gridArray.GetLength(1); y++)
            {
                if (gridArray[x, y])
                {
                    if (!gridArray[x,y-1])
                    {
                        int count = -1;
                        while (gridArray[x, y + count] == null)
                        {
                            count--;
                        }
                        count++;
                        gridArray[x, y].transform.position += new Vector3(0f, count, 0);
                        gridArray[x, y + count] = gridArray[x, y];
                        gridArray[x, y] = null;
                    }
                }
            }
        }
        BreakBlocks();
    }

    public void MoveBlocks(float[] coordinates)
    {
        int x = (int)Math.Round(coordinates[0]-1);
        int y = (int)Math.Round(coordinates[1] - .9);


        Debug.Log($"x: {coordinates[0]}, y: {coordinates[1]} mathed to be x: {x}, y: {y}");
        try
        {
            gridArray[x, y].transform.position += new Vector3(1f, 0, 0);
        }
        catch (Exception e)
        {
        }
        try
        {
            gridArray[x + 1, y].transform.position += new Vector3(-1f, 0, 0);
        }
        catch (Exception e)
        {
        }
        GameObject tempObject = gridArray[x+1, y];
        gridArray[x+1, y] = gridArray[x, y];
        gridArray[x, y] = tempObject;
        FallBlocks();
        /*
        if (resultx)
        {
            if (coordinates[1] >= 1.5 && coordinates[0] < 2.5)
            {
                gridArray[0, 1].transform.position += new Vector3(1f, 0, 0);
                gridArray[1, 1].transform.position += new Vector3(-1f, 0, 0);
                GameObject tempObject = gridArray[1, 1];
                gridArray[1, 1] = gridArray[0, 1];
                gridArray[0, 1] = tempObject;
            }
        }
        */
    }

}
