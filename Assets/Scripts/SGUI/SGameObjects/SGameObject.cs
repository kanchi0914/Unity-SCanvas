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
using static SGUI.Base.Utils;

namespace SGUI.SGameObjects
{
    public abstract class SGameObject
    {

        protected GameObject gameObject;

        private AnchorType anchorType;

        public GameObject GameObject
        {
            get { return gameObject; }
            set { gameObject = value; }
        }
        protected SGameObject parentSGameObject;

        //private Vector3 initialPos;

        private Sequence sequence = DOTween.Sequence();

        public bool IsAcive { get { return gameObject.activeSelf; } }

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

        public Vector2 RectSize
        {
            get
            {
                var rect = this.gameObject.GetComponent<RectTransform>();
                return new Vector2(rect.sizeDelta.x, rect.sizeDelta.y);
            }
            set
            {
                var rect = this.gameObject.GetComponent<RectTransform>();
                rect.sizeDelta = new Vector2(value.x, value.y);
            }
        }

        public void SetActive(bool isActive)
        {
            //gameObject.transform.position = initialPos;
            this.gameObject.SetActive(isActive);
            Animate();
        }

        //public void Animate()
        //{
        //    var anime = gameObject.TryAddComponent<AnimationScript>();
        //    anime.Animate();
        //}

        public void Animate()
        {
            var ts = gameObject.TryAddComponent<CanvasGroup>();

            if (sequence.IsPlaying())
            {
                sequence.Complete();
                sequence.Kill();
            }
            ts.DOFade(0, 0);
            this.rectTransform.AddLocalPosY(-20f);
            sequence = DOTween.Sequence()
                .Append(gameObject.transform.DOLocalMoveY(20f, 0.5f).SetRelative())
                .SetEase(Ease.InCubic)
                .Join(ts.DOFade(1f, 0.5f))
            .OnComplete(() =>
            {
            });
        }

        public void DisplayIn()
        {
            var ts = gameObject.TryAddComponent<CanvasGroup>();
            if (sequence.IsPlaying())
            {
                sequence.Complete();
                sequence.Kill();
            }
            var worldPos = gameObject.transform
                .InverseTransformPoint(gameObject.transform.position);
            var topLeftWorldPos = Utils.getScreenTopLeft();
            var merginToLeftTop = gameObject.transform.InverseTransformPoint(topLeftWorldPos);
            var bottomRightWorldPos = Utils.getScreenBottomRight();
            var merginToRightTop = gameObject.transform.InverseTransformPoint(bottomRightWorldPos);

            var pos = new Vector3(rectTransform.anchoredPosition.x,
                this.rectTransform.anchoredPosition.y);

            //rectTransform.AddLocalPosY(merginToLeftTop.y + RectSize.y);
            rectTransform.AddLocalPosX(merginToLeftTop.x - RectSize.x);

            sequence = DOTween.Sequence()
                .Append(rectTransform.DOAnchorPosX(pos.x, 0.8f))
                .SetEase(Ease.OutBounce);
        }

        public void Popup()
        {
            if (sequence.IsPlaying())
            {
                sequence.Complete();
                sequence.Kill();
            }
            var rectsize = new Vector2(RectSize.x, RectSize.y);
            sequence = DOTween.Sequence()
                .Append(rectTransform.DOSizeDelta
                (new Vector2(rectsize.x + 20, RectSize.y + 20), 0.4f))
                .Append(rectTransform.DOSizeDelta
                (new Vector2(rectsize.x - 10, RectSize.y - 20), 0.4f))
                .Append(rectTransform.DOSizeDelta
                (new Vector2(rectsize.x, RectSize.y), 0.4f));
            sequence.SetEase(Ease.OutBack);
        }

        public void Punch()
        {
            if (sequence.IsPlaying())
            {
                sequence.Complete();
                sequence.Kill();
            }

            var pivot = rectTransform.pivot;
            var pivotOffsets = new Vector2(0.5f, 0.5f) - pivot;
            SetPivot(0.5f, 0.5f);
            //var pospre = new Vector2(gameObject.transform.position.x,
            //    gameObject.transform.position.y);
            //var pospre2 = new Vector2(rectTransform.anchoredPosition.x,
            //    rectTransform.anchoredPosition.y);

            //var pivotOffsets = new Vector2(0.5f, 0.5f) - pivot; 

            //SetPivot(new Vector2(0.0f, 0.1f));

            //gameObject.transform.AddLocalPosX(-RectSize.x / 2);
            //gameObject.transform.AddLocalPosY(RectSize.y / 2);

            //        var pos2 = gameObject.transform.position;
            //        var posaf2 = new Vector2(rectTransform.anchoredPosition.x,
            //rectTransform.anchoredPosition.y);

            sequence = DOTween.Sequence()
                .Append(gameObject.transform.DOPunchScale(new Vector3(0.2f, 0.2f), 1f, 1))
                .SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                SetPivot(0.0f, 1f);
            });
        }

        public void SetPivot(float x, float y)
        {
            var newPivot = new Vector2(x, y);
            var pivotOffset = newPivot - rectTransform.pivot;
            rectTransform.pivot = newPivot;
            rectTransform.AddLocalPosX(RectSize.x * pivotOffset.x);
            rectTransform.AddLocalPosY(RectSize.y * pivotOffset.y);
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
            RectSize = new Vector2(parentSGameObject.RectSize.x * ratioX, parentSGameObject.RectSize.y * ratioY);
            return this;
        }

        public virtual SGameObject SetRectSize(float width, float height)
        {
            if (parentSGameObject == null) return this;
            RectSize = new Vector2(width, height);
            return this;
        }

        public virtual SGameObject SetLocalPosByRatio(float posXratio, float posYratio)
        {
            if (parentSGameObject == null) return this;
            gameObject.transform.SetLocalPosX(posXratio * parentSGameObject.RectSize.x);
            gameObject.transform.SetLocalPosY(-(posYratio * parentSGameObject.RectSize.y));
            //initialPos = rectTransform.position;
            return this;
        }

        public virtual SGameObject SetLocalPos(float posX, float posY)
        {
            if (parentSGameObject == null) return this;
            gameObject.transform.SetLocalPos(posX, -posY);
            //initialPos = rectTransform.position;

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
            var parentRectSize = GetParentRectSize();
            var pivot = rectTransform.pivot;

            if (RectSize.x < 0 && RectSize.y < 0)
            {
                var merginLeft = rectTransform.offsetMin.x;
                var merginTop = rectTransform.offsetMax.y;

                var totalMergin = (rectTransform.offsetMin - rectTransform.offsetMax);
                var rectsize = parentRectSize - totalMergin;

                rectTransform.anchorMin = new Vector2(0f, 1f);
                rectTransform.anchorMax = new Vector2(0f, 1f);
                SetPivot(0, 1);

                rectTransform.anchoredPosition = new Vector2(merginLeft, merginTop);
                rectTransform.sizeDelta = new Vector2(rectsize.x, rectsize.y);
            }
            else
            {
                if (pivot.x == 0 & pivot.y == 1) return this;

                var merginCenterX = rectTransform.anchoredPosition.x;
                var merginCenterY = rectTransform.anchoredPosition.y;

                var anchoredPosX = (parentRectSize.x / 2) + merginCenterX - RectSize.x / 2;
                var anchoredPosY = -(parentRectSize.y / 2) + merginCenterY + RectSize.y / 2;

                var rectSize = RectSize;
                rectTransform.anchorMin = new Vector2(0f, 1f);
                rectTransform.anchorMax = new Vector2(0f, 1f);
                SetPivot(0, 1);
                rectTransform.anchoredPosition = new Vector2(anchoredPosX, anchoredPosY);
                rectTransform.sizeDelta = new Vector2(rectSize.x, rectSize.y);
            }


            return this;
        }

        public SGameObject SetMiddleCenterAnchor()
        {
            var pivot = rectTransform.pivot;

            var parentRectSize = GetParentRectSize();

            var anchorMax = rectTransform.anchorMax;
            var anchorMin = rectTransform.anchorMin;

            // horizontal streth
            if (anchorMin.x == 0.0f && anchorMin.y == 0.25f && anchorMax.x == 1.0f && anchorMax.y == 0.75f)
            {
                var offsetMax = rectTransform.offsetMax;
                var offsetMin = rectTransform.offsetMin;

                var width = offsetMax.x + offsetMin.x;

                rectTransform.anchoredPosition = new Vector2(0, 0);
                rectTransform.sizeDelta = new Vector2(width, 200);
                return this;

                ////-right, -top
                //rectTransform.offsetMax = new Vector2(-right, 0);
                ////left, bottom
                //rectTransform.offsetMin = new Vector2(merginLeft, 0);
            }

            var merginLeft = rectTransform.anchoredPosition.x;
            var merginRight = parentRectSize.x - RectSize.x - merginLeft;
            var merginTop = -rectTransform.anchoredPosition.y;
            var merginBottom = parentRectSize.y - RectSize.y - merginTop;

            var anchoredPosX = merginLeft - (parentRectSize.x / 2) + RectSize.x / 2;
            var anchoredPosY = (parentRectSize.y / 2) - merginTop - RectSize.y / 2;

            var rectSize = new Vector2(RectSize.x, RectSize.y);
            rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            SetPivot(0.5f, 0.5f);
            rectTransform.anchoredPosition = new Vector2(anchoredPosX, anchoredPosY);
            rectTransform.sizeDelta = new Vector2(rectSize.x, rectSize.y);
            return this;
        }

        private Vector2 GetParentRectSize()
        {
            GameObject parent = parentSGameObject.GameObject;
            Vector2 parentRectSize;
            if (parent != null)
            {
                parentRectSize = parentSGameObject.RectSize;
            }
            else
            {
                parentRectSize = new Vector2(Screen.width, Screen.height);
            }
            return parentRectSize;
            //return new Vector2(Screen.width, Screen.height);
        }

        public SGameObject SetHorizontalStretchAnchor()
        {

            SetMiddleCenterAnchor();

            var parentRectSize = GetParentRectSize();

            var merginLeft = parentRectSize.x / 2 - RectSize.x / 2
                - rectTransform.anchoredPosition.x;
            var right = parentRectSize.x / 2 - RectSize.x / 2
                + rectTransform.anchoredPosition.x;

            rectTransform.anchorMin = new Vector2(0.0f, 0.25f);
            rectTransform.anchorMax = new Vector2(1f, 0.75f);
            rectTransform.pivot = new Vector2(0.5f, 0.5f);

            SetPivot(0.5f, 0.5f);

            //-right, -top
            rectTransform.offsetMax = new Vector2(-right, 0);
            //left, bottom
            rectTransform.offsetMin = new Vector2(merginLeft, 0);

            //rectTransform.offsetMax = new Vector2(0, 0);
            //rectTransform.offsetMin = new Vector2(0, 0);
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
            var pivot = rectTransform.pivot;
            var anchorMax = rectTransform.anchorMax;
            var anchorMin = rectTransform.anchorMin;
            if (pivot.x == 0.5f & pivot.y == 0.5f && anchorMin.x == 0
                && anchorMin.y == 0 && anchorMax.x == 1 && anchorMax.y == 1) return this;

            SetTopLeftAnchor();

            var parentRectSize = GetParentRectSize();
            //SetPivot(0, 1);
            var merginLeft = rectTransform.anchoredPosition.x;
            var merginRight = parentRectSize.x - RectSize.x - merginLeft;
            var merginTop = -rectTransform.anchoredPosition.y;
            var merginBottom = parentRectSize.y - RectSize.y - merginTop;
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(1, 1);
            rectTransform.pivot = new Vector3(0.5f, 0.5f);
            //-right, -top
            rectTransform.offsetMax = new Vector2(-merginRight, -merginTop);
            //left, bottom
            rectTransform.offsetMin = new Vector2(merginLeft, merginBottom);
            return this;
        }

        #endregion

    }
}