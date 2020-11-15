using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public int health;
    [SerializeField] private float meleeRange;
    [SerializeField] private int attackDamage;
    [SerializeField] private float attackCooldownTime;
    [Header("Drops")]
    [SerializeField] private GameObject ammoDrop;
    [SerializeField] private GameObject healthDrop;
    [SerializeField] private int ammoMin;
    [SerializeField] private int ammoMax;
    [SerializeField] private float nukeChance;
    [SerializeField] private GameObject nukeDrop;
    [SerializeField] private float infinitePaintChance;
    [SerializeField] private GameObject infinitePaintDrop;
    [SerializeField] private float instaKillChance;
    [SerializeField] private GameObject instaKillDrop;

    private SpriteRenderer _spriteRenderer;
    private GameObject _player;
    private Vector3 _playerPos;
    private Vector3 _vectorToPlayer;
    private Animator _animator;
    private bool _canAttack = true;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _animator = GetComponent<Animator>();
        StartCoroutine(ZombieSound());
    }

    
    void Update()
    {
        GetPlayerVector();
        if (health <= 0)
        {
            GetComponent<CapsuleCollider2D>().enabled = false;
            StartCoroutine(Die());
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, -5);
    }
    
    public void TakeDamage(int damage)
    {
        StartCoroutine(DamageAnim());
        health -= damage;
    }

    private IEnumerator DamageAnim()
    {
        for (int i = 255; i >= 0; i-=25)
        {
            _spriteRenderer.color = new Color32(255, (byte)i, (byte)i, 255);
            yield return new WaitForSeconds(0.0001f);
        }
        for (int i = 0; i <= 255; i+=25)
        {
            _spriteRenderer.color = new Color32(255, (byte)i, (byte)i, 255);
            yield return new WaitForSeconds(0.0001f);
        }
    }

    private IEnumerator Die()
    {
        for (int i = 255; i > 0; i-=10)
        {
            _spriteRenderer.color = new Color32(255, 0, 0, (byte)i);
            yield return new WaitForSeconds(0.0001f);
        }
        CalculateDrops();
        _player.GetComponent<PlayerStats>().AddKill();
        Destroy(gameObject);
    }

    private void CalculateDrops()
    {
        HealthDrop();
        AmmoDrop();
        NukeDrop();
        InfinityPaintDrop();
        InstaKillDrop();
    }

    private void HealthDrop()
    {
        Instantiate(healthDrop, transform.position, Quaternion.identity);
    }

    private void NukeDrop()
    {
        var gen = Random.Range(0f, 1f);
        if (gen < nukeChance)
        {
            Instantiate(nukeDrop, transform.position, Quaternion.identity);
        }
    }
    
    private void InfinityPaintDrop()
    {
        var gen = Random.Range(0f, 1f);
        if (gen < infinitePaintChance)
        {
            Instantiate(infinitePaintDrop, transform.position, Quaternion.identity);
        }
    }
    
    private void InstaKillDrop()
    {
        var gen = Random.Range(0f, 1f);
        if (gen < instaKillChance)
        {
            Instantiate(instaKillDrop, transform.position, Quaternion.identity);
        }
    }

    private void AmmoDrop()
    {
        var numAmmo = Random.Range(ammoMin, ammoMax);
        for (int i = 0; i <= numAmmo; i++)
        {
            var pos = new Vector2(
                transform.position.x + Random.Range(-0.5f, 0.5f),
                transform.position.y + Random.Range(-0.5f, 0.5f));
            Instantiate(ammoDrop, pos, Quaternion.identity);
        }
    }

    private void GetPlayerVector()
    {
        _playerPos = _player.transform.position;
        _vectorToPlayer = (_playerPos - transform.position);
        if (_canAttack && _vectorToPlayer.magnitude <= meleeRange)
        {
            Debug.Log("attack");
            _player.GetComponent<PlayerStats>().TakeDamage(attackDamage);
            StartCoroutine(AttackCooldown());
        }
        
        _animator.SetFloat("Horizontal", _vectorToPlayer.x);
        _animator.SetFloat("Vertical", _vectorToPlayer.y);
    }

    private IEnumerator AttackCooldown()
    {
        _canAttack = false;
        yield return new WaitForSeconds(attackCooldownTime);
        _canAttack = true;
    }

    private IEnumerator ZombieSound()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(3, 10));
            FindObjectOfType<AudioManager>().Play("singleZombie clip");
        }
    }
}
