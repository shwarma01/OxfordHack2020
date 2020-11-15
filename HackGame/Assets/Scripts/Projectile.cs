using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;

public class Projectile : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] private int projectileDamage;

    private PlayerStats _playerStats;
    
    private Color[] colors =
    {
        new Color(0,1,0,1), new Color(1,0,0,1), 
        new Color(0,0,1,1), new Color(1, 0.5f, 0f, 1), 
        new Color(1,1,0,1), new Color(0.1f, 0.8f, 0.8f, 1f), 
    };
    void Start()
    {
        var randIndex = Random.Range(0, 6);
        GetComponent<SpriteRenderer>().color = colors[randIndex];
        Destroy(gameObject, 3f);
        _playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(projectileDamage * _playerStats.damageModifier);
            TriggerAnim();
        }
        else if (other.gameObject.CompareTag("Projectile"))
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<CapsuleCollider2D>(), other.gameObject.GetComponent<CapsuleCollider2D>());
        }
        else
        {
            TriggerAnim();
        }
    }

    private void TriggerAnim()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<CapsuleCollider2D>().enabled = false;
        animator.SetTrigger("Impact");
        Destroy(gameObject, 0.367f);
        
    }
}
