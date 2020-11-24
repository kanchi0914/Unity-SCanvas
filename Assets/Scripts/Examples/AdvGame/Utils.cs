using System;
using System.Collections.Generic;
using Assets.Scripts.Extensions;
using EGUI.GameObjects;
using UnityEngine.UI;

namespace Assets.Scripts.Examples.AdvGame
{
    public class Utils
    {
        public static void SetBackgroundImage(EGGameObject image, string imageFilePath)
        {
            image.SetImage(imageFilePath);
            image.gameObject.GetImageComponent().SetNativeSize();
            image.SetMiddleCenterAnchor().SetLocalPos(0, 0);
            var asfitter = image.gameObject.AddComponent<AspectRatioFitter>();
            asfitter.aspectRatio = image.RectSize.x / image.RectSize.y;
            asfitter.aspectMode = AspectRatioFitter.AspectMode.EnvelopeParent;
        }
    }
    
    public static partial class TupleEnumerable
    {
        public static IEnumerable<(T item, int index)> Indexed<T>(this IEnumerable<T> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            IEnumerable<(T item, int index)> impl()
            {
                var i = 0;
                foreach (var item in source)
                {
                    yield return (item, i);
                    ++i;
                }
            }

            return impl();
        }
    }
}