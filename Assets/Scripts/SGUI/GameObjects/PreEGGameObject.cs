// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.Linq;
// using System.Linq.Expressions;
// using System.Reflection;
// using System.Text;
// using System.Threading.Tasks;
// using Assets.Scripts.Extensions;
// using Assets.Scripts.SGUI.SGameObjects.ComponentScripts;
// using DG.Tweening;
// using UnityEngine;
// using UnityEngine;
// using System.Threading.Tasks;
// using EGUI.Base;
// using EGUI.GameObjects.Interfaces;
// using UnityEngine.UI;
// using static EGUI.Base.Utils;
//
// namespace EGUI.GameObjects
// {
//     public abstract class EGGameObject
//     {
//         protected GameObject gameObject;
//
//         public List<EGGameObject> ChildrenObjects { get; private set; } = new List<EGGameObject>();
//
//         public GameObject GameObject
//         {
//             get { return gameObject; }
//             set { gameObject = value; }
//         }
//
//         public EGGameObject ParentEgGameObject
//         {
//             get { return _parentEgGameObject; }
//             set { _parentEgGameObject = value; }
//         }
//
//         /// <summary>
//         /// rectTransform.sizeDeltaがStrechだとマイナスになったりするのでその回避用
//         /// </summary>
//         public Vector2 ApparentRectSize
//         {
//             get => RectTransform.GetApparentRectSize(); 
//         }
//
//         private EGGameObject _parentEgGameObject;
//
//         public RectTransform RectTransform { get; }
//
//         protected EGGameObject
//         (
//             EGGameObject parent,
//             string name
//         )
//         {
//             InitGameObject(parent, name);
//             RectTransform = gameObject.GetComponent<RectTransform>();
//             SetParentSGameObject(parent);
//             RectTransform.SetTopLeftAnchor();
//             gameObject.name = name;
//         }
//
//         private void InitGameObject(EGGameObject parent, string name)
//         {
//             GameObject = new GameObject(name);
//             if (parent != null) GameObject.transform.SetParent(parent.GameObject.transform, false);
//             var rect = GameObject.AddComponent<RectTransform>();
//             rect.SetTopLeftAnchor();
//             GameObject.transform.SetLocalPos(0, 0);
//             rect.sizeDelta = new Vector2(100, 100);
//         }
//
//         /// <summary>
//         /// GameObjectをDestroyする
//         /// </summary>
//         public void DestroySelf()
//         {
//             GameObject.Destroy(gameObject);
//         }
//
//         public Vector2 RectSize
//         {
//             get => new Vector2(RectTransform.sizeDelta.x, RectTransform.sizeDelta.y);
//             set => RectTransform.sizeDelta = new Vector2(value.x, value.y);
//         }
//
//         public EGGameObject SetGlobalPos(Vector3 pos)
//         {
//             var test = new EGUIObject(null);
//             var obj = new GameObject().AddComponent<MonoBehaviour>();
//             gameObject.SetActive(false);
//             obj.StartCoroutine(SetGlobalPosCoroutine(pos));
//             test.DestroySelf();
//             return this;
//         }
//
//         private IEnumerator SetGlobalPosCoroutine(Vector3 pos)
//         {
//             gameObject.transform.position = pos;
//             gameObject.SetActive(true);
//             yield return null;
//             gameObject.transform.position = pos;
//             gameObject.SetActive(true);
//         }
//
//         public void SetActive(bool isActive)
//         {
//             this.gameObject.SetActive(isActive);
//         }
//
//
//         public void SetPivot(float x, float y)
//         {
//             var newPivot = new Vector2(x, y);
//             RectTransform.pivot = newPivot;
//         }
//
//         public virtual EGGameObject SetImageColor(Color color, float alpha = 1)
//         {
//             var image = gameObject.TryAddComponent<Image>();
//             image.color = color;
//             return this;
//         }
//
//         public virtual EGGameObject SetParentSGameObject(EGGameObject parent)
//         {
//             if (parent != null)
//             {
//                 if (parent is ILayoutObject)
//                 {
//                     ILayoutObject layoutObject = parent as ILayoutObject;
//                     layoutObject.AddItem(this);
//                 }
//                 else
//                 {
//                     gameObject.transform.SetParent(parent.GameObject.transform, false);
//                     this._parentEgGameObject = parent;
//                 }
//                 parent.ChildrenObjects.Add(this);
//             }
//             return this;
//         }
//
//         public virtual EGGameObject SetRectSizeByRatio(float ratioX, float ratioY)
//         {
//             SetRectSize(RectTransform.GetParentRectSize().x * ratioX,
//                 RectTransform.GetParentRectSize().y * ratioY);
//             return this;
//         }
//
//         public virtual EGGameObject SetRectSize(float width, float height)
//         {
//             if (RectTransform.GetAnchorType() == RectTransformExtensions.AnchorType.FullStretch
//                 || RectTransform.GetAnchorType() == RectTransformExtensions.AnchorType.HorizontalStretch
//                 || RectTransform.GetAnchorType() == RectTransformExtensions.AnchorType.HorizontalStretchWithBottomPivot
//                 || RectTransform.GetAnchorType() == RectTransformExtensions.AnchorType.HorizontalStretchWithTopPivot
//                 || RectTransform.GetAnchorType() == RectTransformExtensions.AnchorType.VerticalStretch
//                 || RectTransform.GetAnchorType() == RectTransformExtensions.AnchorType.VerticalStretchWithLeftPivot
//                 || RectTransform.GetAnchorType() == RectTransformExtensions.AnchorType.VerticalStretchWithRightPivot
//             )
//             {
//                 var anchor = RectTransform.GetAnchorType();
//                 RectTransform.SetMiddleCenterAnchor();
//                 RectSize = new Vector2(width, height);
//                 RectTransform.SetAnchorType(anchor);
//             }
//             else
//             {
//                 RectSize = new Vector2(width, height);
//             }
//             return this;
//         }
//
//         public EGGameObject SetLocalPosByRatio(float posXratio, float posYratio)
//         {
//             var posX = posXratio * RectTransform.GetParentRectSize().x;
//             var posY = -(posYratio * RectTransform.GetParentRectSize().x);
//             SetLocalPos(posX, posY);
//             return this;
//         }
//
//         public EGGameObject SetLocalPos(float posX, float posY)
//         {
//             gameObject.transform.SetLocalPos(posX, -posY);
//             return this;
//         }
//
//         public EGGameObject SetPosAndSize(float posX, float posY, float width, float height)
//         {
//             SetLocalPos(posX, posY);
//             SetRectSize(width, height);
//             return this;
//         }
//
//         public EGGameObject SetPosAndSizeByRatio(float posXratio, float posYratio, float widthRatio,
//             float heightRatio)
//         {
//             SetLocalPosByRatio(posXratio, posYratio);
//             SetRectSizeByRatio(widthRatio, heightRatio);
//             return this;
//         }
//
//         public void ClearComponents()
//         {
//             foreach (Transform child in gameObject.transform)
//             {
//                 GameObject.Destroy(child.gameObject);
//             }
//             var gridLayout = gameObject.GetComponent<GridLayoutGroup>();
//             var verticalLayout = gameObject.GetComponent<VerticalLayoutGroup>();
//             DestroySelf(gridLayout, verticalLayout);
//         }
//
//         IEnumerator DestroySelf(params Component[] components)
//         {
//             yield return new WaitForEndOfFrame();
//             components.ToList().ForEach(c => DestroySelf(c));
//         }
//
//     }
// }