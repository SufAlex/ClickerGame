using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIScript : MonoBehaviour
{
    [SerializeField] private MonsterControllerScript _monsterControllerScript;
    
    [SerializeField] private Button btnMenu;
    [SerializeField] private Button btnBoost1;
    [SerializeField] private Button btnBoost2;
    void Awake()
    {
        btnMenu.onClick.AddListener(ToMainMenu);
        btnBoost1.onClick.AddListener(_monsterControllerScript.KillAll);
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
