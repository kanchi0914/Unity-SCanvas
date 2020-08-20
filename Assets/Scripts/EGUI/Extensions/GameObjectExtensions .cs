using System;
using System.Collections.Generic;
using EGUI.Base;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Extensions
{
    public static class GameObjectExtensions
    {
        public static List<GameObject> GetChildrenObjects(this GameObject gameObject)
        {
            var childrenObjects = new List<GameObject>();
            foreach (Transform child in gameObject.transform)
            {
                childrenObjects.Add(child.gameObject);
            }
            return childrenObjects;
        }

        /// <summary>
        /// GameObjectをDestroyする
        /// </summary>
        public static void DestroySelf(this GameObject gameObject)
        {
            GameObject.DestroyImmediate(gameObject);
        }

        public static RectTransform GetRectTransform(this GameObject gameObject)
        {
            return gameObject.GetOrAddComponent<RectTransform>();
        }

        public static Vector2 GetRectSize(this GameObject gameObject)
        {
            return gameObject.GetRectTransform().sizeDelta;
        }

        // public EGGameObject SetGlobalPos(Vector3 pos)
        // {
        //     var test = new EGUIObject(null);
        //     var obj = new GameObject().AddComponent<MonoBehaviour>();
        //     gameObject.SetActive(false);
        //     obj.StartCoroutine(SetGlobalPosCoroutine(pos));
        //     test.DestroySelf();
        //     return this;
        // }

        // private IEnumerator SetGlobalPosCoroutine(Vector3 pos)
        // {
        //     gameObject.transform.position = pos;
        //     gameObject.SetActive(true);
        //     yield return null;
        //     gameObject.transform.position = pos;
        //     gameObject.SetActive(true);
        // }




        // public static GameObject SetImageColor(this GameObject gameObject, Color color, float alpha = 1)
        // {
        //     var image = gameObject.TryAddComponent<Image>();
        //     image.color = color;
        //     return gameObject;
        // }

        public static GameObject SetRectSizeByRatio(this GameObject gameObject, float ratioX, float ratioY)
        {
            gameObject.SetRectSize(gameObject.GetRectTransform().GetParentRectSize().x * ratioX,
                gameObject.GetRectTransform().GetParentRectSize().y * ratioY);
            return gameObject;
        }

        public static GameObject SetRectSize(this GameObject gameObject, float width, float height)
        {
            gameObject.GetRectTransform().SetRectSize(width, height);
            return gameObject;
        }

        public static GameObject SetLocalPosByRatio(this GameObject gameObject, float posXratio, float posYratio)
        {
            gameObject.GetRectTransform().SetRectSizeByRatio(posXratio, posYratio);
            return gameObject;
        }

        public static GameObject SetLocalPos(this GameObject gameObject, float posX, float posY)
        {
            gameObject.GetRectTransform().SetAnchoredPos(posX, -posY);
            return gameObject;
        }
        
        public static GameObject SetPivot(this GameObject gameObject, float x, float y)
        {
            gameObject.GetRectTransform().SetPivot(x, y);
            return gameObject;
        }

        public static GameObject SetPresetRect(this GameObject gameObject, RectInfo rectInfo)
        {
            gameObject.GetRectTransform().SetPresetRect(rectInfo);
            return gameObject;
        }

        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            try
            {
                var attached = gameObject.GetComponent<T>();
                if (!attached) attached = gameObject.AddComponent<T>();
                return attached;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
        
        public static GameObject SetTopLeftAnchor(this GameObject gameObject)
        {
            gameObject.GetRectTransform().SetTopLeftAnchor();
            return gameObject;
        }
        
        public static GameObject SetTopCenterAnchor(this GameObject gameObject)
        {
            gameObject.GetRectTransform().SetTopCenterAnchor();
            return gameObject;
        }
        
        public static GameObject SetTopRightAnchor(this GameObject gameObject)
        {
            gameObject.GetRectTransform().SetTopRightAnchor();
            return gameObject;
        }
        
        public static GameObject SetMiddleLeftAnchor(this GameObject gameObject)
        {
            gameObject.GetRectTransform().SetMiddleLeftAnchor();
            return gameObject;
        }
        
        public static GameObject SetMiddleCenterAnchor(this GameObject gameObject)
        {
            gameObject.GetRectTransform().SetMiddleCenterAnchor();
            return gameObject;
        }
        
        public static GameObject SetMiddleRightAnchor(this GameObject gameObject)
        {
            gameObject.GetRectTransform().SetMiddleRightAnchor();
            return gameObject;
        }
        
        public static GameObject SetBottomLeftAnchor(this GameObject gameObject)
        {
            gameObject.GetRectTransform().SetBottomLeftAnchor();
            return gameObject;
        }
        
        public static GameObject SetBottomCenterAnchor(this GameObject gameObject)
        {
            gameObject.GetRectTransform().SetBottomCenterAnchor();
            return gameObject;
        }
        
        public static GameObject SetBottomRightAnchor(this GameObject gameObject)
        {
            gameObject.GetRectTransform().SetBottomRightAnchor();
            return gameObject;
        }

        
        public static GameObject SetHorizontalStretchAnchor(this GameObject gameObject)
        {
            gameObject.GetRectTransform().SetHorizontalStretchAnchor();
            return gameObject;
        }
        
        public static GameObject SetHorizontalStretchWithTopPivotAnchor(this GameObject gameObject)
        {
            gameObject.GetRectTransform().SetHorizontalStretchWithTopPivotAnchor();
            return gameObject;
        }
        
        public static GameObject SetHorizontalStretchWithBottomPivotAnchor(this GameObject gameObject)
        {
            gameObject.GetRectTransform().SetHorizontalStretchWithBottomPivotAnchor();
            return gameObject;
        }
        
        public static GameObject SetVerticalStretchAnchor(this GameObject gameObject)
        {
            gameObject.GetRectTransform().SetVerticalStretchAnchor();
            return gameObject;
        }
        
        public static GameObject SetVerticalStretchWithLeftPivotAnchor(this GameObject gameObject)
        {
            gameObject.GetRectTransform().SetVerticalStretchWithLeftPivotAnchor();
            return gameObject;
        }
        
        public static GameObject SetVerticalStretchWithRightPivotAnchor(this GameObject gameObject)
        {
            gameObject.GetRectTransform().SetVerticalStretchWithRightPivotAnchor();
            return gameObject;
        }

        public static GameObject SetFullStretchAnchor(this GameObject gameObject)
        {
            gameObject.GetRectTransform().SetFullStretchAnchor();
            return gameObject;
        }
        
        public static GameObject SetOnClick(this GameObject gameObject, Action action)
        {
            var Trigger = gameObject.GetOrAddComponent<EventTrigger>();
            Trigger.triggers = new List<EventTrigger.Entry>();
            return gameObject.AddOnClick(action);
        }

        public static GameObject AddOnClick(this GameObject gameObject, Action action)
        {
            var trigger = gameObject.GetOrAddComponent<EventTrigger>();
            var entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener(e => action.Invoke());
            trigger.triggers.Add(entry);
            return gameObject;
        }
        
        public static GameObject SetImage(this GameObject gameObject, string imageFilePath)
        {
            var image = gameObject.GetOrAddComponent<Image>();
            if (image != null)
            {
                var source = Resources.Load<Sprite> (imageFilePath);
                if (!source) Debug.Log ($"Sprite resource {imageFilePath} is not found");
                else image.sprite = source;
                return gameObject;
            }
            return gameObject;
        }
        
        public static GameObject SetImageSprite(this GameObject gameObject, Sprite sprite)
        {
            var image = gameObject.GetOrAddComponent<Image>();
            image.type = Image.Type.Sliced;
            if (image != null)
            {
                image.sprite = sprite;
                return gameObject;
            }
            return gameObject;
        }
        
        public static GameObject SetImageColor(this GameObject gameObject, Color color, float? alpha = null)
        {
            var image = gameObject.GetOrAddComponent<Image>();
            if (image != null)
            {
                image.color = new Color(color.r, color.g, color.b, alpha ?? color.a);
                return gameObject;
            }
            return gameObject;
        }

    }
}