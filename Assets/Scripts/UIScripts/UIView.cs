using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UIScripts
{
    
    public class UIView : MonoBehaviour
    {
        //NOTE Нормально ли то что поля view публичные
        
        [SerializeField] public Button btnMenu;
        [SerializeField] public Button btnBoost1;
        [SerializeField] public Button btnBoost2;
        [SerializeField] public GameObject endPanel;
        
        

    }
}