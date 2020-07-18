using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Extensions;
using Assets.Scripts.SCanvases;
using SGUI.Base;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;

namespace SGUI.SGameObjects
{
    public abstract class SGameObject
    {

        protected GameObject gameObject;
        public GameObject GameObject
        {
            get { return gameObject; } set { gameObject = value; }
        }
        protected SGameObject parentSGameObject;

        public string Name
        {
            get
            {
                return name;
            }
        }
        protected string name;

        protected SGameObject (SGameObject parent, string name, Func<GameObject> initializationMethod)
        {
            this.gameObject = initializationMethod.Invoke () as GameObject;
            SetParentSGameObject (parent);
            //if (parent != null) this.SetRectSize((int)parent.RectSize.x, (int)parent.RectSize.y);
            //if (parent != null) this.SetRectSize(300, 300);
            this.name = name;
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

        public void SetLocalPos (float x, float y)
        {
            gameObject.transform.AddLocalPosX (x);
            gameObject.transform.AddLocalPosY (y);
        }

        public virtual SGameObject SetBackGroundColor (ColorType colorType, float alpha)
        {
            var color = gameObject.GetComponent<Image> ();
            if (!color) color = gameObject.AddComponent<Image> ();
            color.color = Utils.GetColor (colorType, alpha);
            return this;
        }

        public virtual SGameObject SetBackGroundColor (Color _color)
        {
            var color = gameObject.GetComponent<Image> ();
            if (!color) color = gameObject.AddComponent<Image> ();
            color.color = _color;
            return this;
        }

        public virtual SGameObject SetParentSGameObject (SGameObject parent)
        {
            if (parent != null && parent.gameObject.transform != null)
            {
                if (parent is SVerticalListScrollView scrollView)
                {
                    scrollView.AddChild (this);
                }
                else if (parent is SVerticalGridScrollView gridScrollView)
                {
                    gridScrollView.AddChild (this);
                }
                else
                {
                    gameObject.transform.SetParent (parent.GameObject.transform, false);
                }
            }

            this.parentSGameObject = parent;
            return this;
        }

        public virtual SGameObject SetRectSizeByRatio (float ratioX, float ratioY)
        {
            if (parentSGameObject == null) return this;
            RectSize = (parentSGameObject.RectSize.x * ratioX, parentSGameObject.RectSize.y * ratioY);
            return this;
        }

        public virtual SGameObject SetRectSize (int width, int height)
        {
            if (parentSGameObject == null) return this;
            RectSize = (width, height);
            return this;
        }

        public virtual SGameObject SetLocalPosByRatio (float posXratio, float posYratio)
        {
            if (parentSGameObject == null) return this;
            gameObject.transform.AddLocalPosX (posXratio * parentSGameObject.RectSize.x);
            gameObject.transform.AddLocalPosY (-(posYratio * parentSGameObject.RectSize.y));
            return this;
        }

        public virtual SGameObject SetLocalPos (int posX, int posY)
        {
            if (parentSGameObject == null) return this;
            gameObject.transform.AddLocalPosX (posX);
            gameObject.transform.AddLocalPosY (-posY);
            return this;
        }

        public virtual SGameObject SetBackGroundImage (string sourceName)
        {
            var imageSource = Resources.Load<Sprite> (sourceName);
            var image = gameObject.GetComponent<Image> ();
            if (!image) image = gameObject.AddComponent<Image> ();
            image.sprite = imageSource;
            return this;
        }

        // public void ClearComponents () { 
        //     GameObject.Destroy(gameObject);
        //     gameObject = UIFactory.CreateBaseRect(parentSGameObject.GameObject, name);
        //     var rect = gameObject.GetComponent<RectTransform>().sizeDelta;
        //     gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(RectSize.x, RectSize.y);
        // }

        public void ClearComponents ()
        {
            foreach (Transform child in gameObject.transform)
            {
                GameObject.Destroy (child.gameObject);
            }
            var gridLayout = gameObject.GetComponent<GridLayoutGroup> ();
            var verticalLayout = gameObject.GetComponent<VerticalLayoutGroup> ();
            //GameObject.DestroyImmediate(layout0, true); 
            Destroy (gridLayout, verticalLayout);
        }
        IEnumerator Destroy (params Component[] components)
        {
            yield return new WaitForEndOfFrame ();
            components.ToList ().ForEach (c => Destroy (c));
        }

        // IEnumerator Destroy (Component component)
        // {
        //     yield return new WaitForEndOfFrame ();
        //     Destroy (component);
        // }

        public Action GetFunction (Action function)
        {
            return new Action (() => function ());
        }

        public Action<string> GetFunction (Action<string> function)
        {
            return new Action<string> (function);
        }

    }
}