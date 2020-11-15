using System.Collections;
using UnityEngine;

public class RPTree : MonoBehaviour
{
    [SerializeField] private TreeProjectile treeProjectile;
    [SerializeField] private float firingPeriod;
    [SerializeField] private float treeSpeed;
    [SerializeField] private int ammoCost;
    private Vector3 _aimDirection;
    private Vector3 _cursorInWorldPos;

    private Camera _gameCam;

    private bool _isShooting;
    private GameObject _player;
    private Vector3 _playerPos;
    private PlayerStats _playerStats;

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
        if (!_isShooting) StartCoroutine(ShootContinuously());
        _isShooting = true;
    }

    public void StopShoot()
    {
        StopAllCoroutines();
        _isShooting = false;
    }

    private IEnumerator ShootContinuously()
    {
        while (_playerStats.currentAmmo >= ammoCost)
        {
            _playerStats.UseAmmo(ammoCost);
            _aimDirection = new Vector2(
                _aimDirection.x,
                _aimDirection.y);
            _aimDirection = _aimDirection.normalized;
            var projectileAngle = Vector2.Angle(Vector2.up, _aimDirection);
            if (_aimDirection.x > 0) projectileAngle = -projectileAngle;

            var newProjectile = Instantiate(treeProjectile, new Vector3(transform.position.x + _aimDirection.x, transform.position.y + _aimDirection.x, -5),
                Quaternion.Euler(0, 0, projectileAngle));
            newProjectile.GetComponent<Rigidbody2D>().velocity = new Vector2(_aimDirection.x * treeSpeed,
                _aimDirection.y * treeSpeed);
            yield return new WaitForSeconds(firingPeriod);
        }
    }
}