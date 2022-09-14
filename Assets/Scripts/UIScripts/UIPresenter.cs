using System;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UIScripts
{
    public class UIPresenter
    {
        private UIModel _uiModel;
        private UIView _uiView;
        
        //public event Action ScoreScreen;
        public event Action Boost1;

        public UIPresenter (UIModel model, UIView view)
        {
            if(model == null)
                throw new ArgumentException("Constructor parameter is null.", nameof(model));
            if(view == null)
                throw new ArgumentException("Constructor parameter is null.", nameof(view));
            
            _uiModel = model;
            _uiView = view;
            
            //BUG при выходе в меню спавнится еще несколько монстров - вероятно не останавливается таск SpawnTask
            _uiView.btnMenu.onClick.AddListener(ToMainMenu);
            _uiView.btnBoost1.onClick.AddListener(Boost1Method);
        }

        public void ToMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void PrintScorePanel(int score)
        {
            _uiView.endPanel.GetComponentInChildren<Text>().text = "Score: " + score;
            _uiView.endPanel.SetActive(true);
        }

        private void Boost1Method()
        {
            //NOTE можно ли обойтись без создания этого метода напрямую - в _uiView.btnBoost1.onClick.AddListener передавая что-то другое
            Boost1?.Invoke();
        }
        
    }
}