using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIScript : MonoBehaviour
{
    //[SerializeField] private GameObject interfacePanel;
    
    [SerializeField] private Button btnMenu;
    [SerializeField] private Button btnBoost1;
    [SerializeField] private Button btnBoost2;
    void Awake()
    {
        btnMenu.onClick.AddListener(ToMainMenu);
    }

    private void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
