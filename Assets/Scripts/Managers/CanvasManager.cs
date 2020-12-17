using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject _endPanel;
    [SerializeField] private GameObject _wavePanel;

    private PlayerHealthAndDamage _player;
    private SpawnManager _spawnManager;
    [SerializeField] private bool _playerDead = false;
    void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _player = GameObject.Find("Player").GetComponent<PlayerHealthAndDamage>();
        _wavePanel.SetActive(true);
        
        if (_player == null)
        {
            Debug.Log("Missing Player Health");
        }
        if (_spawnManager == null)
        {
            Debug.Log("Cant find spawn manager");
        }
    }
    void Update()
    {
        CheckEndPanelStatus();
    }

    void CheckEndPanelStatus()
    {
        if (_playerDead)
        {
            _endPanel.SetActive(true);
            _wavePanel.SetActive(false);
        } else if (_spawnManager.DidWin() && !_playerDead)
        {
            _endPanel.SetActive(true);
            _wavePanel.SetActive(false);
        } else
        {
            _endPanel.SetActive(false);
            _wavePanel.SetActive(true);
        }
    }

    public void SetPlayerDead()
    {
        _playerDead = true;
    }
    public void SetPlayerAlive()
    {
        _playerDead = false;       
    }

    public bool GetPlayerDead()
    {
        return _playerDead;
    }
}
