using Assets.Scripts.Extensions;
using EGUI.Base;
using UnityEngine;
using UnityEngine.UI;

namespace EGUI.GameObjects
{
    public class EgImage : EGGameObject
    {
        public Image Image { get; private set; }


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="imageFilePath"></param>
        /// <param name="posRatioX"></param>
        /// <param name="posRatioY"></param>
        /// <param name="widthRatio"></param>
        /// <param name="heightRatio"></param>
        /// <param name="name"></param>
        public EgImage
        (
            EGGameObject parent,
            string imageFilePath = null,
            float posRatioX = 0,
            float posRatioY = 0,
            float widthRatio = 1,
            float heightRatio = 1,
            bool keepsAspectRatio = false,
            string name = "SImage"
        ) : base(
            parent, 
            posRatioX, 
            posRatioY, 
            widthRatio, 
            heightRatio,
            "SImage", 
            () => UIFactory.CreateImage(parent.GameObject, name, imageFilePath)
        )
        {
            Image = gameObject.GetComponent<Image>();
            if (keepsAspectRatio)
            {
                var aspectFitter = gameObject.TryAddComponent<AspectRatioFitter>();
                aspectFitter.aspectMode = AspectRatioFitter.AspectMode.FitInParent;
            }
        }

        /// <summary>
        /// 画像ソースを設定
        /// </summary>
        /// <param name="source">設定するSprite</param>
        public EgImage SetImageSource(Sprite source)
        {
            Image.sprite = source;
            return this;
        }

    }
}