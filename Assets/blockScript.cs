using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockScript : MonoBehaviour
{
    public Rigidbody2D body;
    public SpriteRenderer spriteRenderer;
    [SerializeField] List<Sprite> spriteList;
    // Start is called before the first frame update
    void Start()
    {

        spriteRenderer.sprite = spriteList[Random.Range(0, spriteList.Count)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
