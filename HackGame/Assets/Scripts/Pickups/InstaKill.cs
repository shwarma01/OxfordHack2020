using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstaKill : MonoBehaviour
{
    private PlayerStats _playerStats;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _playerStats = other.gameObject.GetComponent<PlayerStats>();
            if (_playerStats.currentPowerup == "none")
            {
                _playerStats.AddPowerup("InstaKill");
                FindObjectOfType<AudioManager>().Play("powerUp clip");
                Destroy(gameObject);
            }

        }
    }
}
