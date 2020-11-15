using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinitePaint : MonoBehaviour
{
    private PlayerStats _playerStats;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _playerStats = other.gameObject.GetComponent<PlayerStats>();
            if (_playerStats.currentPowerup == "none")
            {
                _playerStats.AddPowerup("InfinitePaint");
                FindObjectOfType<AudioManager>().Play("powerUp clip");
                Destroy(gameObject);
            }

        }
    }
    // private PlayerStats playerStats;
    //
    // void OnCollisionEnter2D(Collision2D other)
    // {
    //     if (other.gameObject.CompareTag("Player"))
    //     {
    //         Debug.Log(1);
    //         playerStats = other.gameObject.GetComponent<PlayerStats>();
    //         FindObjectOfType<AudioManager>().Play("powerUp clip");
    //         GetComponent<SpriteRenderer>().forceRenderingOff = true;
    //         GetComponent<BoxCollider2D>().enabled = false;
    //         StartCoroutine(Countdown(5));
    //     }
    // }
    //
    // private IEnumerator Countdown(int seconds)
    // {
    //     for (int i = 0; i < seconds; i++) 
    //     {
    //         ResetAmmo();
    //         yield return new WaitForSeconds(1);
    //     }
    //     ResetAmmo();
    //     Destroy(gameObject);
    // }
    //
    // void ResetAmmo()
    // {
    //     playerStats.currentAmmo = playerStats.maxAmmo;
    // }
}
