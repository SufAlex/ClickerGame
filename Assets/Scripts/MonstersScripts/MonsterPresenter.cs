using System;
using UnityEngine;

namespace MonstersScripts
{
    public class MonsterPresenter 
    {
        private MonsterModel _monsterModel;
        public MonsterView MonsterView { get; }
        
        public event Action Death;

        public MonsterPresenter(MonsterModel monsterModel, MonsterView monsterView)
        {
            if(monsterView == null)
                throw new ArgumentException("Constructor parameter is null.", nameof(monsterView));
            if(monsterModel == null)
                throw new ArgumentException("Constructor parameter is null.", nameof(monsterModel));
                
            _monsterModel = monsterModel;
            MonsterView = monsterView;
            MonsterView.OnClickAction += () => ReceiveDamage(1);
        }
        
        private void ReceiveDamage(int dmg)
        {
            Debug.Log($"[ReceiveDamage] Отнял HP");
            _monsterModel.Hp -= dmg;

            if(Death == null) Debug.Log($"[ReceiveDamage] Death == null");
            
            if (_monsterModel.Hp <= 0)
                Death?.Invoke();

                
        }
        
    }
}