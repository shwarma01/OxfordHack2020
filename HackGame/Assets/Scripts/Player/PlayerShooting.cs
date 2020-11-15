using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR;


public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [Header("Guns")]
    [SerializeField] private AssaultBrush assaultBrush;
    [SerializeField] private PaintShotgun paintShotgun;
    [SerializeField] private RPTree rpTree;
    [SerializeField] public int gunIndex;
    [SerializeField] private AudioManager audioManager;

    private Vector2 _cursorInWorldPos;
    private Vector2 _playerPos;
    private Vector2 _aimDirection;
    private Camera _gameCam;
    private PlayerStats _playerStats;
    



    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        _gameCam = Camera.main;
        _playerStats = GetComponent<PlayerStats>();
        gunIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerStats.isDead == false)
        {
            GetDirection();
            if (Input.GetButtonDown("Fire1"))
            {
                HandleFire();
            }

            if (Input.GetButtonUp("Fire1"))
            {
                HandleFireStop();
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SetGunIndex(0);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SetGunIndex(1);
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SetGunIndex(2);
            }
        }
    }

    public void SetGunIndex(int index)
    {
        // Stop shooting for any gun when theres a new one
        assaultBrush.StopShoot();
        paintShotgun.StopShoot();
        rpTree.StopShoot();
        // Set new gun on player
        gunIndex = index;
    }

    private void HandleFire()
    {
        if (_playerStats.currentAmmo > 0)
        {
            audioManager.Play("pop clip");
            // Assault brush
            if (gunIndex == 0)
            {
                assaultBrush.Shoot();
            }
            else if (gunIndex == 1)
            {
                paintShotgun.Shoot();
            }
            else if (gunIndex == 2)
            {
                audioManager.Play("rpgFire clip");
                rpTree.Shoot();
            }
        }
    }

    private void HandleFireStop()
    {
        audioManager.Stop("pop clip");
        // Assault Brush
        if (gunIndex == 0)
        {
            assaultBrush.StopShoot();
        }
        else if (gunIndex == 1)
        {
            paintShotgun.StopShoot();
        }
        else if (gunIndex == 2)
        {
            rpTree.StopShoot();
        } 
    }

    private void GetDirection()
    {
        _playerPos = transform.position;
        _cursorInWorldPos = _gameCam.ScreenToWorldPoint(Input.mousePosition);
        _aimDirection = _cursorInWorldPos - _playerPos;
        // Restrict magnitude to 1
        _aimDirection.Normalize();
        animator.SetFloat("Horizontal", _aimDirection.x);
        animator.SetFloat("Vertical", _aimDirection.y);
    }
}
