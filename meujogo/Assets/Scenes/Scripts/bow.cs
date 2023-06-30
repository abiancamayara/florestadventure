using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class bow : MonoBehaviour
{
    private Rigidbody2D rig;

    public float speed;
    public int Damage;
    public bool isRight;
    
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isRight)
        {
            rig.velocity = Vector2.right * speed; 
        }
        else
        {
            rig.velocity = Vector2.left * speed; 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Collision.gameObject.tag == "Inimigo")
        {
            Collision.GetComponent<Inimigo>().Damage(damage);
        }
    }
}
