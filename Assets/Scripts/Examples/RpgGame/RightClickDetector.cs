using System;
using UniRx.Triggers;
using UnityEngine;

namespace Examples.RpgGame
{
    public class RightClickDetector : MonoBehaviour
    {
        private Action action;
        
        public void Init(Action action)
        {
            this.action = action;
        }
        
        void Update()
        {
            // if (Input.GetMouseButtonDown(1) && action != null)
            // {
            //     action.Invoke();
            // }
        }
    }
}