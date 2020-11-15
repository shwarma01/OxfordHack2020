using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickUp : MonoBehaviour
{
    // Change this value after gun pick up gameObject has been instantiated
    public int gunIndex;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerShooting>().SetGunIndex(gunIndex);
            FindObjectOfType<AudioManager>().Play("powerUp clip");
            Destroy(gameObject);
        }
    }
}
