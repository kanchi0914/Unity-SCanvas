using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.SGUI.SGameObjects.ComponentScripts
{
    public class OnDeselectWrapper : MonoBehaviour, IDeselectHandler
    {
        public delegate void DeselectAction(GameObject target);
        public event DeselectAction OnDeselectEvent;
        
        public void OnDeselect(BaseEventData eventData)
        {
            if (OnDeselectEvent != null)
            {
                OnDeselectEvent(this.gameObject);
            }
        }
    }
}
