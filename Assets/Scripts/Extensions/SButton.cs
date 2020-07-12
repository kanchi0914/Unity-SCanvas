using HC.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Extensions
{
    public class SButton : SGameObject
    {
        public Button Button { get; set; }
        public Delegate Func { get; private set; }

        public SButton(GameObject parent, string text, Delegate func)
        {
            gameObject = UIFactory.CreateButton(parent, text);
            Func = func;
            Button = gameObject.GetComponent<Button>();
            Button.onClick.AddListener(() => func.DynamicInvoke());
        }

        public SButton(string text)
        {
            Button = UICreator.CreateButton(defaultLabel: text);
        }

    }
}
