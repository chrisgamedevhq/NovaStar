using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndPanelManager : MonoBehaviour
{
    private GameObject _endPanel;
    private Text _finalScore;
    private Text _finalWave;
    private Text _diedText;
    private Text _winText;
    private Button _checkPointButton;
    private SpawnManager _spawnManager;
    private Score_Display_UI _playerScore;
    private CanvasManager _canvas;
    void Start()
   {
        _endPanel = GameObject.Find("End_Panel");
        _playerScore = GameObject.Find("Canvas").GetComponent<Score_Display_UI>();
        _canvas = GameObject.Find("Canvas").GetComponent<CanvasManager>();
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        _finalScore = GameObject.Find("Endpanel_Score_Text").GetComponent<Text>();
        _finalWave = GameObject.Find("Wave_Number").GetComponent<Text>();
        _diedText = GameObject.Find("Died_Text").GetComponent<Text>();
        _checkPointButton = GameObject.Find("Checkpoint_Button").GetComponent<Button>();
        _winText = GameObject.Find("Win_Text").GetComponent<Text>();

        if (_playerScore == null)
        {
            Debug.LogError("Cant find the HUD in EndPanel");
        }

        if(_endPanel == null)
        {
            Debug.Log("Missing end panel");
        }
        
        if (_spawnManager == null)
        {
            Debug.Log("Cant find spawn manager");
        }

        _winText.enabled = false;
        _diedText.enabled = false;
    }

    private void Update()
    {
        if (_canvas.GetPlayerDead())
        {
            _diedText.enabled = true;
            if (_spawnManager.GetCheckPointStatus())
            {
                _checkPointButton.interactable = true;
            }
        }
        if (_spawnManager.DidWin() && !_canvas.GetPlayerDead())
        {
            _diedText.enabled = false;
            _winText.enabled = true;
            _checkPointButton.interactable = false;
        }

        _finalScore.text = "Final Score: " + _playerScore.GetUIScore();
        _finalWave.text = "You survived " + _spawnManager.GetWave().ToString() + " total waves";
    }

    // On click Events start
    public void LoadLevel()
    {
        // Load the Game Scene
        SceneManager.LoadScene(2);
    }

    public void LoadCheckpoint()
    {
        _spawnManager.RestartFromCheckpoint();
        _endPanel.SetActive(false);
    }

    public void QuitLevel()
    {
        // Load the Main Menu Scene
        SceneManager.LoadScene(0);
    }
    // On click Event end
}