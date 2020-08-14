using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Extensions;
using Assets.Scripts.SGUI.SGameObjects.ComponentScripts;
using DG.Tweening;
using UnityEngine;
using UnityEngine;
using System.Threading.Tasks;
using EGUI.Base;
using EGUI.GameObjects.Interfaces;
using UnityEngine.UI;
using static EGUI.Base.Utils;

namespace EGUI.GameObjects
{
    public abstract class EGGameObject
    {
        protected GameObject gameObject;

        public EGMono Mono;

        private Utils.AnchorType anchorType;

        public GameObject GameObject
        {
            get { return gameObject; }
            set { gameObject = value; }
        }

        public EGGameObject ParentEgGameObject
        {
            get { return _parentEgGameObject; }
            set { _parentEgGameObject = value; }
        }

        /// <summary>
        /// rectTransform.sizeDeltaがStrechだとマイナスになったりするのでその回避用
        /// </summary>
        public Vector2 ApparentRectSize
        {
            get
            {
                var anchorMax = rectTransform.anchorMax;
                var anchorMin = rectTransform.anchorMin;
                var offsetMax = rectTransform.offsetMax;
                var offsetMin = rectTransform.offsetMin;
                float rectSizeX = 0;
                float rectSizeY = 0;
                if ((anchorMax - anchorMin).y != 0)
                {
                    if (_parentEgGameObject == null) rectSizeY = Screen.height;
                    else rectSizeY = _parentEgGameObject.ApparentRectSize.y - offsetMin.y + offsetMax.y;
                }
                else
                {
                    rectSizeY = rectTransform.sizeDelta.y;
                }

                if ((anchorMax - anchorMin).x != 0)
                {
                    if (_parentEgGameObject == null) rectSizeX = Screen.width;
                    else rectSizeX = _parentEgGameObject.ApparentRectSize.x - offsetMin.x + offsetMax.x;
                }
                else
                {
                    rectSizeX = rectTransform.sizeDelta.x;
                }

                return new Vector2(rectSizeX, rectSizeY);
            }
        }

        private EGGameObject _parentEgGameObject;

        private Sequence sequence = DOTween.Sequence();

        private Vector3 actualPos;

        public bool IsAcive
        {
            get { return gameObject.activeSelf; }
        }

        public string Name
        {
            get { return name; }
        }

        protected string name;

        public RectTransform RectTransform
        {
            get { return rectTransform; }
        }


        protected RectTransform rectTransform;

        protected EGGameObject
        (
            EGGameObject parent,
            float posRatioX,
            float posRatioY,
            float widthRatio,
            float heightRatio,
            string name,
            Func<GameObject> initializationMethod
        )
        {
            gameObject = initializationMethod.Invoke();
            rectTransform = gameObject.GetComponent<RectTransform>();
            SetParentSGameObject(parent);
            SetTopLeftAnchor();
            Mono = gameObject.TryAddComponent<EGMono>();
            SetRectSizeByRatio(widthRatio, heightRatio);
            SetLocalPosByRatio(posRatioX, posRatioY);
            this.name = name;
        }

        /// <summary>
        /// GameObjectをDestroyする
        /// </summary>
        public void Dispose()
        {
            GameObject.Destroy(gameObject);
        }

        public Vector2 RectSize
        {
            get => new Vector2(RectTransform.sizeDelta.x, RectTransform.sizeDelta.y);
            set => RectTransform.sizeDelta = new Vector2(value.x, value.y);
        }

        public EGGameObject SetGlobalPos(Vector3 pos)
        {
            var test = new EGUIObject(null);
            var parentMono = _parentEgGameObject.Mono;
            if (parentMono != null)
            {
                gameObject.SetActive(false);
                test.Mono.StartCoroutine(SetGlobalPosCoroutine(pos));
                //parentMono.StartCoroutine(SetGlobalPosCoroutine(pos));
            }
            else
            {
                Mono.StartCoroutine(SetGlobalPosCoroutine(pos));
            }
            test.Dispose();
            return this;
        }

        private IEnumerator SetGlobalPosCoroutine(Vector3 pos)
        {
            gameObject.transform.position = pos;
            gameObject.SetActive(true);
            yield return null;
            gameObject.transform.position = pos;
            gameObject.SetActive(true);
        }

        public void SetActive(bool isActive)
        {
            //gameObject.transform.position = initialPos;
            this.gameObject.SetActive(isActive);
        }


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
                .OnComplete(() => { });
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
                .Append(rectTransform.DOSizeDelta(new Vector2(rectsize.x + 20, RectSize.y + 20), 0.4f))
                .Append(rectTransform.DOSizeDelta(new Vector2(rectsize.x - 10, RectSize.y - 20), 0.4f))
                .Append(rectTransform.DOSizeDelta(new Vector2(rectsize.x, RectSize.y), 0.4f));
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
                .OnComplete(() => { SetPivot(0.0f, 1f); });
        }


        public void AnimateShake()
        {
            if (sequence.IsPlaying())
            {
                sequence.Complete();
                sequence.Kill();
            }

            sequence = DOTween.Sequence()
                .Append(gameObject.transform.DOShakePosition(0.5f, strength: 20, vibrato: 30));
        }

        public void AnimateBlink()
        {
            if (sequence.IsPlaying())
            {
                sequence.Complete();
                sequence.Kill();
            }

            CanvasGroup imageCanvas = gameObject.TryAddComponent<CanvasGroup>();
            sequence = DOTween.Sequence()
                .Append(imageCanvas.DOFade(0.0f, 0.1f).SetLoops(5))
                .Append(imageCanvas.DOFade(1.0f, 0.0f));
        }

        private IEnumerator SetAction(Action action)
        {
            yield return null;
            action.Invoke();
        }

        //private IEnumerator SetAnime()
        //{
        //    yield return null;
        //    if (sequence.IsPlaying())
        //    {
        //        sequence.Complete();
        //        sequence.Kill();
        //    }
        //    sequence = DOTween.Sequence().
        //        Append(gameObject.transform.DOShakePosition(0.5f, strength: 30, vibrato: 50));
        //}


        public void SetPivot(float x, float y)
        {
            var newPivot = new Vector2(x, y);
            // var pivotOffset = newPivot - rectTransform.pivot;
            rectTransform.pivot = newPivot;
            // rectTransform.AddLocalPosX(RectSize.x * pivotOffset.x);
            // rectTransform.AddLocalPosY(RectSize.y * pivotOffset.y);
        }

        public virtual EGGameObject SetColor(ColorType colorType, float alpha = 1)
        {
            var color = gameObject.GetComponent<Image>();
            if (!color) color = gameObject.AddComponent<Image>();
            if (color) color.color = Utils.GetColor(colorType, alpha);
            return this;
        }

        public virtual EGGameObject SetColor(Color _color)
        {
            var color = gameObject.TryAddComponent<Image>();
            color.color = _color;
            return this;
        }

        public virtual EGGameObject SetParentSGameObject(EGGameObject parent)
        {
            if (parent?.gameObject == null)
            {
            }

            if (parent != null && parent.gameObject.transform != null)
            {
                if (parent is ILayoutObject)
                {
                    ILayoutObject layoutObject = parent as ILayoutObject;
                    layoutObject.AddItem(this);
                }
                else
                {
                    gameObject.transform.SetParent(parent.GameObject.transform, false);
                    this._parentEgGameObject = parent;
                }
            }

            return this;
        }

        public virtual EGGameObject SetRectSizeByRatio(float ratioX, float ratioY)
        {
            if (_parentEgGameObject == null) return this;
            // SetRectSize(_parentEgGameObject.RectSize.x * ratioX,
            //     _parentEgGameObject.RectSize.y * ratioY);
            SetRectSize(_parentEgGameObject.ApparentRectSize.x * ratioX,
                _parentEgGameObject.ApparentRectSize.y * ratioY);
            return this;
        }

        public virtual EGGameObject SetRectSize(float width, float height)
        {
            // var anchor = anchorType;
            // var pos = RectTransform.position;
            // SetMiddleCenterAnchor();
            // RectSize = new Vector2(width, height);
            // SetAnchorType(anchor);
            // SetGlobalPos(pos);
            if (anchorType == AnchorType.FullStretch 
                || anchorType == AnchorType.HorizontalStretch
                || anchorType == AnchorType.HorizontalStretchWithBottomPivot
                || anchorType == AnchorType.HorizontalStretchWithTopPivot
                || anchorType == AnchorType.VerticalStretch
                || anchorType == AnchorType.VerticalStretchWithLeftPivot
                || anchorType == AnchorType.VerticalStretchWithRightPivot
                )
            {
                var anchor = anchorType;
                SetMiddleCenterAnchor();
                RectSize = new Vector2(width, height);
                SetAnchorType(anchor);
            }
            else
            {
                RectSize = new Vector2(width, height);
            }
            return this;
        }

        public virtual EGGameObject SetPresetRect(RectInfo rectInfo)
        {
            SetPosAndSize(rectInfo.PosX, rectInfo.PosY, rectInfo.Width, rectInfo.Height);
            return this;
        }

        public virtual EGGameObject SetLocalPosByRatio(float posXratio, float posYratio)
        {
            if (_parentEgGameObject == null) return this;
            var posX = posXratio * _parentEgGameObject.ApparentRectSize.x;
            var posY = -(posYratio * _parentEgGameObject.ApparentRectSize.y);
            SetLocalPos(posX, posY);
            return this;
        }

        public virtual EGGameObject SetLocalPos(float posX, float posY)
        {
            var anchor = anchorType;
            gameObject.transform.SetLocalPos(posX, -posY);
            return this;
        }

        public virtual EGGameObject SetPosAndSize(RectInfo rectInfo)
        {
            SetLocalPos(rectInfo.PosX, rectInfo.PosY);
            SetRectSize(rectInfo.Width, rectInfo.Height);
            return this;
        }

        public virtual EGGameObject SetPosAndSize(float posX, float posY, float width, float height)
        {
            SetLocalPos(posX, posY);
            SetRectSize(width, height);
            return this;
        }

        public virtual EGGameObject SetPosAndSizeByRatio(float posXratio, float posYratio, float widthRatio,
            float heightRatio)
        {
            SetLocalPosByRatio(posXratio, posYratio);
            SetRectSizeByRatio(widthRatio, heightRatio);
            return this;
        }

        public virtual EGGameObject SetBackGroundImage(string sourceName)
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

        #region RectSetter

        public EGGameObject SetOffset(float minX, float minY, float maxX, float maxY)
        {
            rectTransform.offsetMax = new Vector2(maxX, maxY);
            rectTransform.offsetMin = new Vector2(minX, minY);
            return this;
        }

        public EGGameObject SetAnchorType(AnchorType _anchorType)
        {
            this.anchorType = _anchorType;

            switch (_anchorType)
            {
                case Utils.AnchorType.TopLeft:
                {
                    rectTransform.SetTopLeftAnchor();
                    break;
                }
                case Utils.AnchorType.TopCenter:
                {
                    rectTransform.SetTopCenterAnchor();
                    break;
                }
                case Utils.AnchorType.TopRight:
                {
                    rectTransform.SetTopRightAnchor();
                    break;
                }
                case Utils.AnchorType.MiddleLeft:
                {
                    rectTransform.SetMiddleLeftAnchor();
                    break;
                }
                case Utils.AnchorType.MiddleCenter:
                {
                    rectTransform.SetMiddleCenterAnchor();
                    break;
                }
                case Utils.AnchorType.MiddleRight:
                {
                    rectTransform.SetMiddleRightAnchor();
                    break;
                }
                case Utils.AnchorType.BottomLeft:
                {
                    rectTransform.SetBottomLeftAnchor();
                    break;
                }
                case Utils.AnchorType.BottomCenter:
                {
                    rectTransform.SetMiddleCenterAnchor();
                    break;
                }
                case Utils.AnchorType.BottomRight:
                {
                    rectTransform.SetBottomRightAnchor();
                    break;
                }
                case Utils.AnchorType.HorizontalStretch:
                {
                    rectTransform.SetHorizontalStretchAnchor();
                    break;
                }
                case Utils.AnchorType.HorizontalStretchWithTopPivot:
                {
                    rectTransform.SetHorizontalStretchWithTopPivotAnchor();
                    break;
                }
                case Utils.AnchorType.HorizontalStretchWithBottomPivot:
                {
                    rectTransform.SetHorizontalStretchWithBottomPivotAnchor();
                    break;
                }
                case Utils.AnchorType.VerticalStretch:
                {
                    rectTransform.SetVerticalStretchAnchor();
                    break;
                }
                case Utils.AnchorType.VerticalStretchWithLeftPivot:
                {
                    rectTransform.SetVerticalStretchWithLeftPivotAnchor();
                    break;
                }
                case Utils.AnchorType.VerticalStretchWithRightPivot:
                {
                    rectTransform.SetVerticalStretchWithRightPivotAnchor();
                    break;
                }
                case Utils.AnchorType.FullStretch:
                {
                    rectTransform.SetFullStretchAnchor();
                    break;
                }
                default:
                {
                    throw new Exception("something is wrong");
                }
            }
            this.anchorType = _anchorType;
            return this;
        }

        public EGGameObject SetTopLeftAnchor()
        {
            rectTransform.SetTopLeftAnchor();
            return this;
        }
        
        public EGGameObject SetTopCenterAnchor()
        {
            rectTransform.SetTopCenterAnchor();
            return this;
        }
        
        public EGGameObject SetTopRightAnchor()
        {
            rectTransform.SetTopRightAnchor();
            return this;
        }
        
        public EGGameObject SetMiddleLeftAnchor()
        {
            rectTransform.SetMiddleLeftAnchor();
            return this;
        }

        public EGGameObject SetMiddleCenterAnchor()
        {
            rectTransform.SetMiddleCenterAnchor();
            return this;
        }
        
        public EGGameObject SetMiddleRightAnchor()
        {
            rectTransform.SetMiddleRightAnchor();
            return this;
        }
        
                
        public EGGameObject SetBottomLeftAnchor()
        {
            rectTransform.SetBottomLeftAnchor();
            return this;
        }

        public EGGameObject SetBottomCenterAnchor()
        {
            rectTransform.SetBottomCenterAnchor();
            return this;
        }
        
        public EGGameObject SetBottomRightAnchor()
        {
            rectTransform.SetBottomRightAnchor();
            return this;
        }

        public EGGameObject SetHorizontalStretchAnchor()
        {
            rectTransform.SetHorizontalStretchAnchor();
            return this;
        }

        public EGGameObject SetHorizontalStretchWithTopPivotAnchor()
        {
            rectTransform.SetHorizontalStretchWithTopPivotAnchor();
            return this;
        }
        
        public EGGameObject SetHorizontalStretchWithBottomPivotAnchor()
        {
            rectTransform.SetHorizontalStretchWithBottomPivotAnchor();
            return this;
        }

        public EGGameObject SetVerticalStretchAnchor()
        {
            rectTransform.SetVerticalStretchAnchor();
            return this;
        }
        
        public EGGameObject SetVerticalStretchWithLeftPivotAnchor()
        {
            rectTransform.SetVerticalStretchWithLeftPivotAnchor();
            return this;
        }
        
        public EGGameObject SetVerticalStretchWithRightPivotAnchor()
        {
            rectTransform.SetVerticalStretchWithRightPivotAnchor();
            return this;
        }
        
        public EGGameObject SetFullStretchAnchor()
        {
            rectTransform.SetFullStretchAnchor();
            anchorType = AnchorType.FullStretch;
            return this;
        }

        #endregion
    }
}