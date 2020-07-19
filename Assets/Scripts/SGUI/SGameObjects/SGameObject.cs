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
using Assets.Scripts.SGUI.SGameObjects.ComponentScripts;
using DG.Tweening;
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
            get { return gameObject; }
            set { gameObject = value; }
        }
        protected SGameObject parentSGameObject;

        private Vector3 initialPos;

        private Sequence sequence = DOTween.Sequence();

        public string Name
        {
            get
            {
                return name;
            }
        }
        protected string name;

        public RectTransform RectTransform
        {
            get
            {
                return rectTransform;
            }
        }

        protected RectTransform rectTransform;

        protected SGameObject(SGameObject parent, string name, Func<GameObject> initializationMethod)
        {
            this.gameObject = initializationMethod.Invoke() as GameObject;
            rectTransform = gameObject.GetComponent<RectTransform>();
            SetParentSGameObject(parent);
            this.name = name;
        }

        public (float x, float y) RectSize
        {
            get
            {
                var rect = this.gameObject.GetComponent<RectTransform>();
                return (rect.sizeDelta.x, rect.sizeDelta.y);
            }
            set
            {
                var rect = this.gameObject.GetComponent<RectTransform>();
                rect.sizeDelta = new Vector2(value.x, value.y);
            }
        }

        public void SetActive(bool isActive)
        {
            gameObject.transform.position = initialPos;
            this.gameObject.SetActive(isActive);
            if (isActive) Animate();
        }

        public void Animate()
        {
            Debug.Log("dadasdasdasds");
            var anime = gameObject.TryAddComponent<DGAnimationScript>();
            anime.Animate();
        }


        ///// <summary>
        ///// Set local positon of Gamebject. 
        ///// </summary>
        ///// <param name="x">pixel from left</param>
        ///// <param name="y">pixel from left</param>
        //public void SetLocalPos (float x, float y)
        //{
        //    gameObject.transform.AddLocalPosX (x);
        //    gameObject.transform.AddLocalPosY (-y);
        //}

        public virtual SGameObject SetBackGroundColor(ColorType colorType, float alpha)
        {
            var color = gameObject.GetComponent<Image>();
            if (!color) color = gameObject.AddComponent<Image>();
            if (color) color.color = Utils.GetColor(colorType, alpha);
            return this;
        }

        public virtual SGameObject SetBackGroundColor(Color _color)
        {
            var color = gameObject.GetComponent<Image>();
            if (!color) color = gameObject.AddComponent<Image>();
            if (color) color.color = _color;
            return this;
        }

        public virtual SGameObject SetParentSGameObject(SGameObject parent)
        {
            if (parent != null && parent.gameObject.transform != null)
            {
                // if (parent is SVerticalListScrollView scrollView)
                // {
                //     scrollView.AddChild (this);
                // }
                if (parent is SVerticalGridScrollView gridScrollView)
                {
                    gridScrollView.AddChild(this);
                }
                else if (parent is SHorizontallGridLayoutScrollView horizontalGrid)
                {
                    horizontalGrid.AddChild(this);
                }
                else
                {
                    gameObject.transform.SetParent(parent.GameObject.transform, false);
                }
            }

            this.parentSGameObject = parent;
            return this;
        }

        public virtual SGameObject SetRectSizeByRatio(float ratioX, float ratioY)
        {
            if (parentSGameObject == null) return this;
            RectSize = (parentSGameObject.RectSize.x * ratioX, parentSGameObject.RectSize.y * ratioY);
            return this;
        }

        public virtual SGameObject SetRectSize(float width, float height)
        {
            if (parentSGameObject == null) return this;
            RectSize = (width, height);
            return this;
        }

        public virtual SGameObject SetLocalPosByRatio(float posXratio, float posYratio)
        {
            if (parentSGameObject == null) return this;
            gameObject.transform.SetLocalPosX(posXratio * parentSGameObject.RectSize.x);
            gameObject.transform.SetLocalPosY(-(posYratio * parentSGameObject.RectSize.y));
            initialPos = rectTransform.position;
            return this;
        }

        public virtual SGameObject SetLocalPos(float posX, float posY)
        {
            if (parentSGameObject == null) return this;
            gameObject.transform.SetLocalPos(posX, -posY);
            initialPos = rectTransform.position;
            //gameObject.transform.SetLocalPosX(posX);
            //gameObject.transform.SetLocalPosY(-posY);
            return this;
        }

        public virtual SGameObject SetBackGroundImage(string sourceName)
        {
            var imageSource = Resources.Load<Sprite>(sourceName);
            var image = gameObject.GetComponent<Image>();
            if (!image) image = gameObject.AddComponent<Image>();
            image.sprite = imageSource;
            return this;
        }

        // public void ClearComponents () { 
        //     GameObject.Destroy(gameObject);
        //     gameObject = UIFactory.CreateBaseRect(parentSGameObject.GameObject, name);
        //     var rect = gameObject.GetComponent<RectTransform>().sizeDelta;
        //     gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(RectSize.x, RectSize.y);
        // }

        public void ClearComponents()
        {
            foreach (Transform child in gameObject.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            var gridLayout = gameObject.GetComponent<GridLayoutGroup>();
            var verticalLayout = gameObject.GetComponent<VerticalLayoutGroup>();
            //GameObject.DestroyImmediate(layout0, true); 
            Destroy(gridLayout, verticalLayout);
        }
        IEnumerator Destroy(params Component[] components)
        {
            yield return new WaitForEndOfFrame();
            components.ToList().ForEach(c => Destroy(c));
        }

        // IEnumerator Destroy (Component component)
        // {
        //     yield return new WaitForEndOfFrame ();
        //     Destroy (component);
        // }

        public Action GetFunction(Action function)
        {
            return new Action(() => function());
        }

        public Action<string> GetFunction(Action<string> function)
        {
            return new Action<string>(function);
        }


        #region RectSetter

        public SGameObject SetOffset(float minX, float minY, float maxX, float maxY)
        {
            rectTransform.offsetMax = new Vector2(maxX, maxY);
            rectTransform.offsetMin = new Vector2(minX, minY);
            return this;
        }

        public SGameObject SetTopLeftAnchor()
        {
            rectTransform.anchorMin = new Vector2(0f, 1f);
            rectTransform.anchorMax = new Vector2(0f, 1f);
            rectTransform.pivot = new Vector2(0f, 1f);
            rectTransform.localPosition = new Vector2(0f, 0f);
            rectTransform.offsetMax = new Vector2(0, 0);
            rectTransform.offsetMin = new Vector2(0, 0);
            return this;
        }

        public SGameObject SetMiddleCenterAnchor()
        {
            rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
            rectTransform.offsetMax = new Vector2(0, 0);
            rectTransform.offsetMin = new Vector2(0, 0);
            return this;
        }

        public SGameObject SetHorizontalStretchAnchor()
        {
            rectTransform.anchorMin = new Vector2(0.0f, 0.25f);
            rectTransform.anchorMax = new Vector2(1f, 0.75f);
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
            rectTransform.offsetMax = new Vector2(0, 0);
            rectTransform.offsetMin = new Vector2(0, 0);
            return this;
        }

        public SGameObject SetStretchLeftAnchor()
        {
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(0, 1);
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
            rectTransform.offsetMax = new Vector2(0, 0);
            rectTransform.offsetMin = new Vector2(0, 0);
            return this;
        }

        public SGameObject SetMiddleLeftAnchor()
        {
            rectTransform.anchorMin = new Vector2(0f, 0.5f);
            rectTransform.anchorMax = new Vector2(0f, 0.5f);
            rectTransform.pivot = new Vector2(0f, 0.5f);
            rectTransform.offsetMax = new Vector2(0, 0);
            rectTransform.offsetMin = new Vector2(0, 0);
            return this;
        }

        public SGameObject SetFullStretchAnchor()
        {
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(1, 1);
            rectTransform.pivot = new Vector3(0.5f, 0.5f);
            rectTransform.offsetMax = new Vector2(0, 0);
            rectTransform.offsetMin = new Vector2(0, 0);
            return this;
        }

        #endregion

    }
}