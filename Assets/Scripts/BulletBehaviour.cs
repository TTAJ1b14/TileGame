using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    Rigidbody2D bulletRigidbody;
    PlayerMovement player;
    float xSpeed;
    
    [SerializeField] float bulletSpeed = 1f;

    

    void Start()
    {
    bulletRigidbody = GetComponent<Rigidbody2D>();
    player = FindObjectOfType<PlayerMovement>();
    xSpeed = player.transform.localScale.x * bulletSpeed;    
    }

    void Update()
    {
        bulletRigidbody.velocity = new Vector2 (xSpeed,0f);
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        Debug.Log("BulletTouchedTheWall");
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "EnemyTag")
        Destroy(other.gameObject);
        Destroy(gameObject);
    }


}
