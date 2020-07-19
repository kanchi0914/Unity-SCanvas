using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.SGUI.SGameObjects.ComponentScripts
{
    public class OnSelectEventWrapper : MonoBehaviour, ISelectHandler
    {
        public delegate void SelectAction(GameObject target);
        public event SelectAction OnSelectEvent;
        
        public void OnSelect(BaseEventData eventData)
        {
            if (OnSelectEvent != null)
            {
                OnSelectEvent(this.gameObject);
            }
        }
    }
}
