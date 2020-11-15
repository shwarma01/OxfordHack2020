using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{

    public string currentPowerup = "none";
    public int maxHealth = 100;
    public int currentHealth;
    public int maxAmmo = 300;
    public int currentAmmo;
    public int kills;
    public bool isDead = false;
    public int bobRossCompliment = 1;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private AmmoBar ammoBar;
    [SerializeField] private StoredPowerup storedPowerup;
    public KillCount killCount;
    public int damageModifier = 1;
    [SerializeField] private float nukeRadius;
    [SerializeField] private int nukeDamage;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private EnemySpawner enemySpawner;

    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        currentHealth = maxHealth;
        currentAmmo = maxAmmo;
        healthBar.SetMaxHealth(maxHealth);
        ammoBar.SetMaxAmmo(maxAmmo);
        currentPowerup = "none";
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isDead == false)
        {
            checkCombo();
            if (Input.GetButtonDown("Fire2"))
            {
                UsePowerup();
            }
            // Health Bar
            // Change condition to something different
            if (Input.GetKeyDown(KeyCode.Space))
            {
                TakeDamage(damageModifier);
            }
            // Ammo Bar
            // Change condition to something different
            if (Input.GetKeyDown(KeyCode.Return))
            {
                UseAmmo(30);
            }

            // Temporary kill self
            if (Input.GetKeyDown(KeyCode.K))
            {
                TakeDamage(100);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        StartCoroutine(DamageAnim());
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        // Death
        if (currentHealth <= 0)
        {
            GetComponent<PlayerMovement>().enabled = false;
            audioManager.StopAll();
            GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneTransition>().NextScene();
            audioManager.GameOver();
            isDead = true;
        }
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

    public void UseAmmo(int ammo)
    {   
        if (currentAmmo - ammo < 0)
        {
            currentAmmo = 0;
        }
        else
        {
            currentAmmo -= ammo;
        }
        ammoBar.SetAmmo(currentAmmo);
    }

    public void AddAmmo(int amount)
    {
        currentAmmo = Mathf.Clamp(currentAmmo + amount, 0, maxAmmo);
        ammoBar.SetAmmo(currentAmmo);
    }

    public void AddHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        healthBar.SetHealth(currentHealth);
    }

    public void AddKill()
    {
        kills += 1;
        enemySpawner.waveKill();
        killCount.SetKills(kills);
    }

    public void AddPowerup(string powerupName)
    {
        if (powerupName != "")
        {
            currentPowerup = powerupName;
            storedPowerup.ShowPowerup(powerupName);
        }
    }

    public void UsePowerup()
    {
        switch (currentPowerup)
        {
            case "InstaKill":
                storedPowerup.UseLasting();
                DamageBuff();
                break;
            case "PaintNuke":
                UsePaintNuke();
                AddPowerup("none");
                break;
            case "InfinitePaint":
                storedPowerup.UseLasting();
                UseInfinitePaint();
                break;
            case "none":
                break;
            default:
                Debug.LogError("Invalid powerup name");
                break;
        }
    }

    private void UseInfinitePaint()
    {
        StartCoroutine(InfiniteAmmo(5));
    }

    private IEnumerator InfiniteAmmo(int seconds)
    {
        var counter = seconds;
        while (counter > 0)
        {
            currentAmmo = maxAmmo;
            yield return new WaitForSeconds(1);
            counter--;
        }
        AddPowerup("none");
    }

    private void UsePaintNuke()
    {
        GameObject.FindGameObjectWithTag("NukeEffects").GetComponent<ParticleSystem>().Play();
        var results = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in results)
        {
            enemy.GetComponent<Enemy>().TakeDamage(nukeDamage);
        }
    }


    public void DamageBuff()
    {
        damageModifier *= 1000000;
        StartCoroutine(DamageBuffCountdown(5));
    }

    private void DeBuff()
    {
        damageModifier = 1;
        AddPowerup("none");
    }
    
    private IEnumerator DamageBuffCountdown(int seconds)
    {
        int counter = seconds;
        while (counter > 0)
        {
            yield return new WaitForSeconds(1);
            counter--;
        }
        DeBuff();
    }

    private void checkCombo()
    {
        if ((kills / 5) % 15 >= bobRossCompliment)
        {
            if (bobRossCompliment >= 2) {
                audioManager.Stop("Clip " + (bobRossCompliment - 1));
            }

            audioManager.Play("Clip " + bobRossCompliment);
            bobRossCompliment++;

            if (bobRossCompliment > 14)
            {
                bobRossCompliment = 1;
            }
        }
    }

    public void PlayExplosion()
    {
        audioManager.Play("rpgExplosion clip");
    }
}
