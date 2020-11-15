using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HordeSound : MonoBehaviour
{
    public EnemySpawner enemySpawner;
    public int threshold = 4;

    private bool playing = false;

    // Update is called once per frame
    void Update()
    {
        if (enemySpawner.GetZombiesLeft() >= threshold && playing == false)
        {
            FindObjectOfType<AudioManager>().Play("horde clip");
            playing = true;
        } 
        else if (enemySpawner.GetZombiesLeft() < threshold)
        {
            FindObjectOfType<AudioManager>().Stop("horde clip");
            playing = false;
        }
    }
}
