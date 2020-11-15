using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintShotgun : MonoBehaviour
{
    [SerializeField] private Projectile projectile;
    [SerializeField] private float firingPeriod;
    [SerializeField] private float shootSpeed;
    [SerializeField] private int numProjectiles;
    [SerializeField] private float projectileRandomness;

    private Camera _gameCam;
    private GameObject _player;
    private PlayerStats _playerStats;
    private Vector3 _playerPos;
    private Vector3 _cursorInWorldPos;
    private Vector3 _aimDirection;
    
    private bool _isShooting;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerStats = _player.GetComponent<PlayerStats>();
        _gameCam = Camera.main;
    }

    private void Update()
    {
        _playerPos = _player.transform.position;
        _playerPos = transform.position;
        _cursorInWorldPos = _gameCam.ScreenToWorldPoint(Input.mousePosition);
        _aimDirection = _cursorInWorldPos - _playerPos;
        _aimDirection = _aimDirection.normalized;
    }

    public void Shoot()
    {
        if (!_isShooting)
        {
            StartCoroutine(ShootContinuously());
        }
        _isShooting = true;
    }

    public void StopShoot()
    {
        StopAllCoroutines();
        _isShooting = false;
    }

    private IEnumerator ShootContinuously()
    {
        while (_playerStats.currentAmmo >= numProjectiles)
        {
            _playerStats.UseAmmo(numProjectiles);
            
            for (int i = 0; i < numProjectiles; i++)
            {
                _aimDirection = new Vector2(
                    _aimDirection.x + (Random.Range(-1f, 1f)*projectileRandomness), 
                    _aimDirection.y + (Random.Range(-1f, 1f)*projectileRandomness));
                _aimDirection = _aimDirection.normalized;
                var projectileAngle = Vector2.Angle(Vector2.up, _aimDirection);
                if (_aimDirection.x > 0)
                {
                    projectileAngle = -projectileAngle;
                }

                var newProjectile = Instantiate(projectile, transform.position, Quaternion.Euler(0, 0, projectileAngle));
                newProjectile.GetComponent<Rigidbody2D>().velocity = new Vector2(_aimDirection.x * shootSpeed,
                    _aimDirection.y * shootSpeed);
            }
            yield return new WaitForSeconds(firingPeriod);
        }
    }
}
