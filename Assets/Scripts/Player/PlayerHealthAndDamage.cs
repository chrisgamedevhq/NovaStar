using UnityEngine;

public class PlayerHealthAndDamage : MonoBehaviour
{
    [SerializeField]
    public float health;

    [SerializeField]
    public float maximumHealth;

    [SerializeField]
    private GameObject _explosionAnim;

    private PlayerWeaponsFire _playerWeapon;
    private SpawnManager _spawnManager;

    private CameraShake _cameraShake;

    [SerializeField]
    private AudioClip _bgMusic;

    private void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("Missing Spawn Manager");
        }

        _playerWeapon = GameObject.Find("Player").GetComponent<PlayerWeaponsFire>();
        if (_playerWeapon == null)
        {
            Debug.LogError("Player Weapon is NULL");
        }

        _cameraShake = GameObject.FindWithTag("MainCamera").GetComponent<CameraShake>();
        if (_cameraShake == null)
        {
            Debug.LogError("Camera Shake is NULL.");
        }

        health = 3;
        maximumHealth = 3;
    }

    public void PlayerDamage()
    {
        health -= 1;

        StartCoroutine(_cameraShake.Shake(.3f, 1f));

        if (_playerWeapon._weaponPowerLevel > 0)
        {
            _playerWeapon._weaponPowerLevel = 0;
            _playerWeapon.UpdateWeaponLevel();
        }

        if (health <= 0)
        {
            this.gameObject.SetActive(false); //inactive to prevent damage or input
            Instantiate(_explosionAnim, transform.position, Quaternion.identity);
        }
    }
    public void PlayerRespawn()
    {
        health = 3f; 
        AudioManager.Instance.PlayMusic(_bgMusic, 1.0f);
    }
}

