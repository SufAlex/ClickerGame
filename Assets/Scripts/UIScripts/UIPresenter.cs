using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UIScripts
{
    public class UIPresenter
    {
        private UIModel _uiModel;
        private UIView _uiView;
        public event Action Boost1;

        public UIPresenter (UIModel model, UIView view)
        {
            if(model == null)
                throw new ArgumentException("Constructor parameter is null.", nameof(model));
            if(view == null)
                throw new ArgumentException("Constructor parameter is null.", nameof(view));
            
            _uiModel = model;
            _uiView = view;
            
            //BUG при выходе в меню спавнится еще несколько монстров WTF
            _uiView.btnMenu.onClick.AddListener(ToMainMenu);
            _uiView.btnBoost1.onClick.AddListener(Boost1Method);
        }

        private void ToMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        private void Boost1Method()
        {
            //NOTE можно ли обойтись без создания этого метода напрямую - в _uiView.btnBoost1.onClick.AddListener передавая что-то другое
            Boost1?.Invoke();
        }
        
    }
}