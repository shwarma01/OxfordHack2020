using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int moveSpeed;
    private bool playingSound = false;
    private PlayerStats _playerStats;

    void Start()
    {
        _playerStats = GetComponent<PlayerStats>();
    }

    void Update()
    {
        if (_playerStats.isDead == false)
        {
            Move();
        }
    }
    
    private void Move()
    {
        // Frame rate independence
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPos = transform.position.x + deltaX;
        var newYPos = transform.position.y + deltaY;

        if ((deltaX == 0) && (deltaY == 0))
        {
            FindObjectOfType<AudioManager>().Stop("running clip");
            playingSound = false;
        } else if (playingSound == false)
        {
            FindObjectOfType<AudioManager>().Play("running clip");
            playingSound = true;
        }
        
        transform.position = new Vector2(newXPos, newYPos);
    }
}
