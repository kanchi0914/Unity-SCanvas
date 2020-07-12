using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class SGameObject
    {
        public GameObject GameObject
        {
            get { return gameObject; } set { gameObject = value; }
        }

        public float sizeDeltaX {
            get
            {
                return this.gameObject.GetComponent<RectTransform>().sizeDelta.x;
            }
            set
            {
                this.gameObject.GetComponent<RectTransform>().sizeDelta
                    = new Vector2(value, sizeDeltaY);
            }
        }

        public float sizeDeltaY
        {
            get
            {
                return this.gameObject.GetComponent<RectTransform>().sizeDelta.y;
            }
            set
            {
                this.gameObject.GetComponent<RectTransform>().sizeDelta
                    = new Vector2(sizeDeltaX, value);
            }
        }

        public void SetSize(Vector2 vector2)
        {
            this.gameObject.GetComponent<RectTransform>().sizeDelta = vector2;
        }

        protected GameObject gameObject;

    }
}
