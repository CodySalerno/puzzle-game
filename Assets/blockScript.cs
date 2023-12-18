using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class blockScript : MonoBehaviour
{
    public Rigidbody2D body;
    public SpriteRenderer spriteRenderer;
    private int int_y_floor;
    [SerializeField] List<Sprite> spriteList;
    // Start is called before the first frame update
    private int update_counter = 0;
    public GameObject grid;
    void Start()
    {

        spriteRenderer.sprite = spriteList[UnityEngine.Random.Range(0, spriteList.Count)];
    }

    void FixedUpdate()
    {
        update_counter++;
        if (update_counter % 100 == 0) 
        {
            double tolerance = 1e-5; // You can adjust the tolerance as needed

            transform.position += new Vector3(0f, 0.2f, 0);
            bool resultx = Math.Abs(transform.position.x - 0.5) < tolerance;
            bool resulty = Math.Abs(transform.position.y - 1.5) < tolerance;

            if (resultx && resulty)
            {
                grid.SendMessage("IncreaseRow");
            }
        }
    }

    
    public void Fall(GameObject[,] gridArray)
    {
       
        int x = (int)Math.Round(transform.position.x);
        int y = (int)Math.Round(transform.position.y);

        int count = 0;
        while (gridArray[x, y+count] == null)
        {
            count--;
        }
        transform.position += new Vector3(0f, count, 0);
        gridArray[x, y + count] = gridArray[x, y];
        gridArray[x, y] = null;
    }
    
    /*
    public void Fall()
    {
        transform.position += new Vector3(0f, -1f, 0);
    }
    */
}
