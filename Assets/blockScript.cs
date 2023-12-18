using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockScript : MonoBehaviour
{
    public Rigidbody2D body;
    public SpriteRenderer spriteRenderer;
    private int int_y_floor;
    [SerializeField] List<Sprite> spriteList;
    // Start is called before the first frame update
    private int update_counter = 0;
    void Start()
    {

        spriteRenderer.sprite = spriteList[Random.Range(0, spriteList.Count)];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        update_counter++;
        if (update_counter % 100 == 0) 
        {
            int_y_floor = (int)transform.position.y;
            transform.position += new Vector3(0f, 0.2f, 0);
        }
        
    }
}
