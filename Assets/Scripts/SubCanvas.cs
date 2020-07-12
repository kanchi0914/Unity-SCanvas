using System.Collections.Generic;
using Assets.Scripts.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.SCanvases
{
    public class SubCanvas : QCanvas
    {
        private QCanvas parent;
        
        public SubCanvas(
            QCanvas parent, float posXratio = 0,
            float posYratio = 0, float ratioX = 1f, float ratioY = 1f)
        {
            this.parent = parent;
            GameObject = UIFactory.CreateBaseRect();
            Rect = this.GameObject.GetComponent<RectTransform>();
            var pareRect = parent.GameObject.GetComponent<RectTransform>();
            GameObject.GetComponent<RectTransform>().sizeDelta
                = new Vector2(pareRect.sizeDelta.x * ratioX, pareRect.sizeDelta.y * ratioY);
            GameObject.transform.SetParent(parent.GameObject.transform, false);
            parent.SubCanvases.Add(this);
            GameObject.transform.AddLocalPosX(posXratio * pareRect.sizeDelta.x);
            GameObject.transform.AddLocalPosY(-posYratio * pareRect.sizeDelta.y);
        }

        public SubCanvas(
            QCanvas parent, float posXratio = 0,
            float posYratio = 0, int sizeX = 200, int sizeY = 200)
        {
            this.parent = parent;
            GameObject = UIFactory.CreateBaseRect();
            Rect = this.GameObject.GetComponent<RectTransform>();
            var pareRect = parent.GameObject.GetComponent<RectTransform>();
            GameObject.GetComponent<RectTransform>().sizeDelta
                = new Vector2(sizeX, sizeY);
            GameObject.transform.SetParent(parent.GameObject.transform, false);
            parent.SubCanvases.Add(this);
            GameObject.transform.AddLocalPosX(posXratio * pareRect.sizeDelta.x);
            GameObject.transform.AddLocalPosY(-posYratio * pareRect.sizeDelta.y);
        }

        private void SetListLayout(TextAnchor textAnchor)
        {
            var layout = GameObject.AddComponent<VerticalLayoutGroup>();
            layout.childForceExpandHeight = false;
            layout.childForceExpandWidth = false;
            layout.childControlHeight = false;
            layout.childControlWidth = false;
            layout.childAlignment = textAnchor;
        }

        public void SetGridLayout(int columnSize, int rowSize)
        {
            var layout = GameObject.AddComponent<GridLayoutGroup>();
            if (columnSize > 0)
            {
                layout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
                layout.constraintCount = columnSize;
                layout.cellSize = new Vector2(
                    Rect.sizeDelta.x / columnSize, Rect.sizeDelta.y / rowSize);
            }
        }

        public void SetText(string _text)
        {
            var textObj = UIFactory.CreateText(this.GameObject, "アイテム一覧");
        }

        public void SetVerticalListItems<T>(
            List<T> sGameObjects,
            int rowSize = 10,
            float widthXratio = 0.5f,
            TextAnchor textAnchor = TextAnchor.UpperLeft
        ) where T : SGameObject
        {
            SetListLayout(textAnchor);
            sGameObjects.ForEach(
                s => 
                {
                    s.GameObject.transform.SetParent(gameObject.transform, false);
                    var layoutElement = s.GameObject.GetComponent<LayoutElement>();
                    if (layoutElement != null && layoutElement.minHeight != 0)
                    {
                        s.SetSize(new Vector2(widthXratio * this.sizeDeltaX, layoutElement.minHeight));
                    }
                    else
                    {
                        s.SetSize(new Vector2(widthXratio * this.sizeDeltaX, sizeDeltaY / rowSize));
                    }
                });
        }

        public void SetGridButtons(
            List<SButton> sButtons,
            int columnSize, int rowSize
            )
        {
            SetGridLayout(columnSize, rowSize);
            sButtons.ForEach(sb => sb.Button.transform.SetParent(GameObject.transform, false));
        }


    }
}
