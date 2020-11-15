using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] private Sprite[] weaponSprites;

    private GameObject _player;
    private PlayerShooting _playerShooting;
    private Image _image;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerShooting = _player.GetComponent<PlayerShooting>(); 
        _image = gameObject.GetComponent<Image>();
    }

    void Update()
    {
        _image.sprite = weaponSprites[_playerShooting.gunIndex];
    }
}
