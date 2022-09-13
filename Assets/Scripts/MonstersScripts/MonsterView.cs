using System;
using UnityEngine;

namespace MonstersScripts
{
    public class MonsterView : MonoBehaviour
    {
        public Action OnClickAction;

        public void OnClick()
        {
            OnClickAction?.Invoke();
        }
    }
}