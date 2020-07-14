using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public void SetBackGroundColor (ColorType colorType)
        {
            if (gameObject.GetComponent<Image> () != null)
            {
                var color = gameObject.GetComponent<Image> ();
                color.color = GetColor (Utils.ColorType.Black);
            }
            else
            {
                var color = gameObject.AddComponent<Image> ();
                color.color = GetColor (Utils.ColorType.Black);
            }
        }

        public void SetParent (SGameObject parent)
        {
            gameObject.transform.SetParent (parent.GameObject.transform, false);
        }

        // public float RectSizeX
        // {
        //     get
        //     {
        //         return this.gameObject.GetComponent<RectTransform> ().sizeDelta.x;
        //     }
        //     set
        //     {
        //         this.gameObject.GetComponent<RectTransform> ().sizeDelta = new Vector2 (value, RectSizeY);
        //     }
        // }

        // public float RectSizeY
        // {
        //     get
        //     {
        //         return this.gameObject.GetComponent<RectTransform> ().sizeDelta.y;
        //     }
        //     set
        //     {
        //         this.gameObject.GetComponent<RectTransform> ().sizeDelta = new Vector2 (RectSizeX, value);
        //     }
        // }

        // public void SetSize (Vector2 vector2)
        // {
        //     this.gameObject.GetComponent<RectTransform> ().sizeDelta = vector2;
        // }

        public abstract void InitGameObject (params object[] args);

        // public abstract void InitGameObject<T>(T args);

        protected GameObject gameObject;

        // protected RectTransform rectTransform;
        protected string name;

    }
}