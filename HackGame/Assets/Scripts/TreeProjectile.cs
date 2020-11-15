using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class TreeProjectile : MonoBehaviour
{
    [SerializeField] private float damageRange;
    [SerializeField] private int damage;

    private void OnCollisionEnter2D(Collision2D other)
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        //play particle effect
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().PlayExplosion();
        GetComponent<ParticleSystem>().Play();
        //invisible
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        // If hits enemy, instantly kill it
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(10000);
        }
        
        // deal splash damage
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, damageRange);
        foreach (var hitCollider in hitColliders) 
        {
            if (hitCollider.gameObject.CompareTag("Enemy"))
            {
                hitCollider.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
        
        Destroy(gameObject, 2);
    }
}
