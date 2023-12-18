using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursorScript : MonoBehaviour
{
    public Rigidbody2D body;
    public SpriteRenderer spriteRenderer;
    public Sprite cursor;
    public GameObject grid;
    public KeyCode moveUp;
    public KeyCode moveDown;
    public KeyCode moveLeft;
    public KeyCode moveRight;
    public KeyCode enter;
    public KeyCode space;
    private int update_counter = 0;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.sprite = cursor;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(moveUp)) 
        {
            if (transform.position.y < 12.5) transform.position += new Vector3(0f, 1f, 0);
        }
        if (Input.GetKeyDown(moveDown))
        {
            if (transform.position.y >= 2.5) transform.position += new Vector3(0f, -1f, 0);
        }
        if (Input.GetKeyDown(moveRight))
        {
            if (transform.position.x < 5) transform.position += new Vector3(1f, 0f, 0);
        }
        if (Input.GetKeyDown(moveLeft))
        {
            if (transform.position.x > 1) transform.position += new Vector3(-1f, 0f, 0);
        }
        if (Input.GetKeyDown(enter) || Input.GetKeyDown(space))
        {
            float[] coordinates = new float[2];
            coordinates[0] = transform.position.x;
            coordinates[1] = transform.position.y;
            grid.SendMessage("MoveBlocks", coordinates);
        }
    }

    void FixedUpdate()
    {
        update_counter++;
        if (update_counter % 100 == 0)
        {
            transform.position += new Vector3(0f, 0.2f, 0);
        }
    }
}
