using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIScript : MonoBehaviour
{
    [SerializeField] private GameController gameController;
    
    [SerializeField] private Button btnMenu;
    [SerializeField] private Button btnBoost1;
    [SerializeField] private Button btnBoost2;
    void Awake()
    {
        btnMenu.onClick.AddListener(ToMainMenu);
       // btnBoost1.onClick.AddListener(gameController.KillAll);
    }

    private void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        
    }
}
