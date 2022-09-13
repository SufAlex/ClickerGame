using System;

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
            _monsterModel.Hp -= dmg;

            if (_monsterModel.Hp <= 0) 
                Death?.Invoke();
        }
        
    }
}