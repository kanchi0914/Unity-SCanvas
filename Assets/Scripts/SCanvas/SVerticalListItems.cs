using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.SCanvases
{
    public class SVerticalListItems : SGameObject 
    {
        private SGameObject parent;

        public SVerticalListItems(
            SGameObject parent,
            float posXratio = 0.0f,
            float posYratio = 0.0f, 
            float ratioX = 1.0f, 
            float ratioY = 1.0f)
        {
            InitGameObject ();
            this.parent = parent;
            RectSize = (parent.RectSize.x * ratioX, parent.RectSize.y * ratioY);
            SetParent (parent);
            SetLocalPos (posXratio * parent.RectSize.x, -(posYratio * parent.RectSize.y));
        }

        public override void InitGameObject (params object[] args)
        {
            GameObject = UIFactory.CreateBaseRect();
        }

        public string name { get; private set; } = "SubCanvas";

        public float posRatioX { get; private set; } = 0.0f;
        public float posRatioY { get; private set; } = 0.0f;
        public float widthRatio { get; private set; } = 0.2f;
        public float heightRatio { get; private set; } = 0.2f;

        // public float LocalPosX

        public int width { get; private set; } = 100;

        public int height { get; private set; } = 100;

        public void ClearComponents ()
        {
            foreach (Transform child in gameObject.transform)
            {
                GameObject.Destroy (child.gameObject);
            }
            var layout0 = gameObject.GetComponent<GridLayoutGroup>();
            //GameObject.DestroyImmediate(layout0, true); 
            Destroy(layout0);
        }

        IEnumerator Destroy(Component component)
        {
            yield return new WaitForEndOfFrame();
            Destroy(component);
        }

        private void SetListLayout (TextAnchor textAnchor)
        {
            var layout = GameObject.AddComponent<VerticalLayoutGroup> ();
            layout.childForceExpandHeight = false;
            layout.childForceExpandWidth = false;
            layout.childControlHeight = false;
            layout.childControlWidth = false;
            layout.childAlignment = textAnchor;
        }

        public SVerticalListItems SetVerticalListItems<T> (
            List<T> sGameObjects,
            int rowSize = 10,
            float widthXratio = 1.0f,
            TextAnchor textAnchor = TextAnchor.UpperLeft
        ) where T : SGameObject
        {
            SetListLayout (textAnchor);
            sGameObjects.ForEach (
                s =>
                {
                    s.SetParent (this);
                    var layoutElement = s.GameObject.GetComponent<LayoutElement> ();
                    if (layoutElement != null && layoutElement.minHeight != 0)
                    {
                        s.RectSize = (widthXratio * RectSize.x, layoutElement.minHeight);
                    }
                    else
                    {
                        s.RectSize = (widthXratio * RectSize.x, this.RectSize.y / rowSize);
                    }
                });
            return this;
        }

    }
}