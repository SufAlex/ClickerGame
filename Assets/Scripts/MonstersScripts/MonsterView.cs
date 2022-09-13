using System;
using UnityEngine;

namespace MonstersScripts
{
    public class MonsterView : MonoBehaviour, IClickable
    {
        public Action OnClickAction;

        public void OnClick()
        {
            if(OnClickAction == null) Debug.Log($"[MonsterView][OnClick] OnClickAction == null");
            OnClickAction?.Invoke();
        }
    }
}