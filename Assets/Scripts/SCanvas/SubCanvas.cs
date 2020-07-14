using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.SCanvases
{
    public class SubCanvas : SGameObject
    {
        private SGameObject parent;

        public SubCanvas (
            SGameObject parent, float posXratio = 0.0f,
            float posYratio = 0.0f, float ratioX = 1.0f, float ratioY = 1.0f)
        {
            InitGameObject ();
            this.parent = parent;
            RectSize = (parent.RectSize.x * ratioX, parent.RectSize.y * ratioY);
            SetParent (parent);
            SetLocalPos (posXratio * parent.RectSize.x, -(posYratio * parent.RectSize.y));
        }

        public override void InitGameObject (params object[] args)
        {
            GameObject = UIFactory.CreateBaseRect ();
        }

        public SubCanvas (
            string name, SGameObject parent, float posRatioX = 0, float posRatioY = 0, float widthRatio = 0.2f, float heightRatio = 0.2f
        ) : this (parent, posRatioX, posRatioY, widthRatio, heightRatio)
        {
            this.gameObject.name = name;
        }

        public void SetLocalPos (float x, float y)
        {
            gameObject.transform.AddLocalPosX (x);
            gameObject.transform.AddLocalPosY (y);
        }

        // public SubCanvas(
        //     QCanvas parent, float posXratio = 0,
        //     float posYratio = 0, int sizeX = 200, int sizeY = 200)
        // {
        //     this.parent = parent;
        //     GameObject = UIFactory.CreateBaseRect();
        //     Rect = this.GameObject.GetComponent<RectTransform>();
        //     var pareRect = parent.GameObject.GetComponent<RectTransform>();
        //     GameObject.GetComponent<RectTransform>().sizeDelta
        //         = new Vector2(sizeX, sizeY);
        //     GameObject.transform.SetParent(parent.GameObject.transform, false);
        //     parent.SubCanvases.Add(this);
        //     GameObject.transform.AddLocalPosX(posXratio * pareRect.sizeDelta.x);
        //     GameObject.transform.AddLocalPosY(-posYratio * pareRect.sizeDelta.y);
        // }

        public string name { get; private set; } = "SubCanvas";

        public float posRatioX { get; private set; } = 0.0f;
        public float posRatioY { get; private set; } = 0.0f;
        public float widthRatio { get; private set; } = 0.2f;
        public float heightRatio { get; private set; } = 0.2f;

        // public float LocalPosX

        public int width { get; private set; } = 100;

        public int height { get; private set; } = 100;

        // public SubCanvas(
        //     string name = "SubCanvas", float posRatioX = 0, float posRatioY = 0, float widthRatio = 0.2f, float heightRatio = 0.2f) 
        //     : this(posRatioX, posRatioY, widthRatio, heightRatio)
        // {
        //     this.name = name;
        // }

        // public SubCanvas(
        //     float posRatioX = 0, float posRatioY = 0, float widthRatio = 0.2f, float heightRatio = 0.2f)
        // {
        //     this.name = name;
        //     this.posRatioX = posRatioX;
        //     this.posRatioY = posRatioY;
        //     this.widthRatio = widthRatio;
        //     this.heightRatio = heightRatio;
        // }

        // internal SubCanvas(string name)
        // {

        // }

        // internal SubCanvas()
        // {

        // }

        public void ClearComponents ()
        {
            var layout0 = gameObject.GetComponent<GridLayoutGroup>();
            GameObject.Destroy(layout0);
            foreach (Transform child in gameObject.transform)
            {
                GameObject.Destroy (child.gameObject);
            }
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

        private void SetGridLayout (int columnSize, int rowSize)
        {
            GridLayoutGroup layout = gameObject.GetComponent<GridLayoutGroup>();
            if (layout == null)
            {
                Debug.Log("null!!");
                layout = gameObject.AddComponent<GridLayoutGroup>();
            }
            
            // if (columnSize > 0)
            // {
            //     Debug.Log(layout);
            //     layout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            //     layout.constraintCount = columnSize;
            //     layout.cellSize = new Vector2 (
            //         RectSize.x / columnSize, RectSize.y / rowSize);
            // }
            Debug.Log("------------------------------------");
        }

        public void SetText (string _text)
        {
            var textObj = UIFactory.CreateText (this.GameObject, _text);
        }

        public void SetVerticalListItems<T> (
            List<T> sGameObjects,
            int rowSize = 10,
            float widthXratio = 0.5f,
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
                        s.RectSize = (widthXratio * this.RectSize.x, layoutElement.minHeight);
                    }
                    else
                    {
                        s.RectSize = (widthXratio * this.RectSize.x, this.RectSize.y / rowSize);
                    }
                });
        }

        public void SetGridListItems<T> (
            List<T> sGameObjects,
            int columnSize, int rowSize
        ) where T : SGameObject
        {
            SetGridLayout(columnSize, rowSize);
            sGameObjects.ForEach (
                s =>
                {
                    s.SetParent (this);
                });
        }

        // public void SetGridButtons(
        //     List<SButton> sButtons,
        //     int columnSize, int rowSize
        //     )
        // {
        //     SetGridLayout(columnSize, rowSize);
        //     sButtons.ForEach(sb => sb.Button.transform.SetParent(GameObject.transform, false));
        // }

    }
}