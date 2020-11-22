using System;
using System.Collections;
using System.Collections.Generic;
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
                
        public static Image GetImage(this GameObject gameObject)
        {
            return gameObject.GetOrAddComponent<Image>();
        }

        public static GameObject SetParent(this GameObject gameObject, Transform transform)
        {
            var tempMono = new GameObject();
            var mono = tempMono.gameObject.AddComponent<MonoBehaviour>();
            mono.StartCoroutine(setParent(gameObject, transform));
            return gameObject;
        }

        private static IEnumerator setParent(GameObject gameObject, Transform transform)
        {
            yield return new WaitForEndOfFrame();
            gameObject.transform.SetParent(transform);
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