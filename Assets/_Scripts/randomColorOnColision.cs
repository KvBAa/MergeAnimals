using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomColorOnColision : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        spriteRenderer.color = new Color(Random.value, Random.value, Random.value, 1f);
    }
}
