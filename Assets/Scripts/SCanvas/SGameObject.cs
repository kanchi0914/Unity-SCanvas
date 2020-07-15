using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Extensions;
using Assets.Scripts.SCanvases;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;
using static Assets.Scripts.Utils;

namespace Assets.Scripts
{
    public abstract class SGameObject
    {
        public GameObject GameObject
        {
            get { return gameObject; } set { gameObject = value; }
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public (float x, float y) RectSize
        {
            get
            {
                var rect = this.gameObject.GetComponent<RectTransform> ();
                return (rect.sizeDelta.x, rect.sizeDelta.y);
            }
            set
            {
                var rect = this.gameObject.GetComponent<RectTransform> ();
                rect.sizeDelta = new Vector2 (value.x, value.y);
            }
        }

        public void SetLocalPos(float x, float y)
        {
            gameObject.transform.AddLocalPosX(x);
            gameObject.transform.AddLocalPosY(y);
        }

        public SGameObject SetBackGroundColor (ColorType colorType)
        {
            var color = gameObject.GetComponent<Image>();
            if (!color) color = gameObject.AddComponent<Image>();
            color.color = GetColor(colorType); 
            return this;
        }

        public SGameObject SetBackGroundColor(Color _color)
        {
            var color = gameObject.GetComponent<Image>();
            if (!color) color = gameObject.AddComponent<Image>();
            color.color = _color;
            return this;
        }

        public SGameObject SetParent (SGameObject parent)
        {
            gameObject.transform.SetParent(parent.GameObject.transform, false);
            return this;
        }

        public abstract void InitGameObject (params object[] args);

        protected GameObject gameObject;

        protected string name;

    }
}