using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGUI.Base;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Assets.Scripts.Extensions;
using DG.Tweening;

namespace SGUI.GameObjects
{
    public class SImage : SGameObject
    {
        public Image Image { get; private set; }

        public SImage (
            SGameObject parent,
            string imageFilePath = null
        ) : this(parent, 0, 0, 1, 1, "SImage", imageFilePath){}

        public SImage(
            SGameObject parent,
            float posRatioX,
            float posRatioY,
            float ratioX,
            float ratioY,
            string name,
            string imageFilePath
        ) : base(parent, "SImage",
            new Func<GameObject>(() =>
            {
                return UIFactory.CreateImage(parent.GameObject, "SImage", imageFilePath);
            })
        )
        {
            SetRectSizeByRatio(ratioX, ratioY);
            SetLocalPosByRatio(posRatioX, posRatioY);
            Image = gameObject.GetComponent<Image>();
        }

        public void SetImageSource(Sprite source)
        {
            this.Image.sprite = source;
        }

    

        #region  RequiredMethods

        public new SImage SetBackGroundColor(ColorType colorType, float alpha = 1)
        {
            return base.SetBackGroundColor(colorType, alpha) as SImage;
        }

        public new SImage SetBackGroundColor(Color color)
        {
            return base.SetBackGroundColor(color) as SImage;
        }

        public new SImage SetParentSGameObject(SGameObject parent)
        {
            return base.SetParentSGameObject(parent) as SImage;
        }

        public new SImage SetRectSizeByRatio(float ratioX, float ratioY)
        {
            return base.SetRectSizeByRatio(ratioX, ratioY) as SImage;
        }

        public new SImage SetLocalPosByRatio(float posXratio, float posYratio)
        {
            return base.SetLocalPosByRatio(posXratio, posYratio) as SImage;
        }

        public new SImage SetRectSize(float width, float height)
        {
            return base.SetRectSize(width, height) as SImage;
        }

        #endregion
    }
}