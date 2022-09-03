using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject recordsPanel;
    [SerializeField] private Text recordsText;
    
    [SerializeField] private Button btnStart;
    [SerializeField] private Button btnSettings;
    [SerializeField] private Button btnRecords;
    [SerializeField] private Button btnExit;
    [SerializeField] private Button btnMainMenu_S;
    [SerializeField] private Button btnMainMenu_R;
    private void Awake()
    {
        btnStart.onClick.AddListener(StartGame);
        btnSettings.onClick.AddListener(ShowSettings);
        btnRecords.onClick.AddListener(ShowRecords);
        btnExit.onClick.AddListener(QuiteGame);
        btnMainMenu_S.onClick.AddListener(ShowMain);
        btnMainMenu_R.onClick.AddListener(ShowMain);
    }

    private void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
    private void ShowMain()
    {
        menuPanel.SetActive(true);
        settingsPanel.SetActive(false);
        recordsPanel.SetActive(false);
    }
    
    private void ShowSettings()
    {
        menuPanel.SetActive(false);
        settingsPanel.SetActive(true);
        recordsPanel.SetActive(false);
    }
    
    private void ShowRecords()
    {
        menuPanel.SetActive(false);
        settingsPanel.SetActive(false);
        recordsPanel.SetActive(true);
        
        SaveDataScript.LoadData();
        SaveDataScript.PrintToText(recordsText);
    }
    
    private void QuiteGame()
    {
        Application.Quit();
    }


}
