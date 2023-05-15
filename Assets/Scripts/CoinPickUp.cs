using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    
    [SerializeField] int pointsForCoinPickUp = 100;
    [SerializeField] AudioClip coinPickUpSFX;

    void Start()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {


        if (other.tag == "Player" && other.GetType().Equals(typeof(CapsuleCollider2D)))
        {
            FindObjectOfType<GameSession>().AddToScore(pointsForCoinPickUp);
        AudioSource.PlayClipAtPoint(coinPickUpSFX, Camera.main.transform.position);
        Destroy(gameObject); 
        Debug.Log("CoinPickUP Sc is working");   
        }
    }
}
