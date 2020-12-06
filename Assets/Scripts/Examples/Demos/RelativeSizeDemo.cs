using System.Linq;
using Assets.Scripts.Extensions;
using EGUI.Base;
using UnityEngine;

namespace EGUI.GameObjects.Demos
{
    public class RelativeSizeDemo
    {
        public RelativeSizeDemo()
        {
            var canvas = new EGCanvas("");
            var parentImage = new EGGameObject(canvas.gameObject, "Parent")
                .SetAnchorType(AnchorType.MiddleCenter)
                .SetPosition(-200, 0)
                .SetSize(300, 200)
                .SetImageColor(Color.white);

            var cloneParent = parentImage.Duplicate().SetPosition(200, 0);
            var cloneChild = new EGGameObject(cloneParent.gameObject.GetChildrenObjects().First());
            cloneChild.SetRelativeSize(.8f, .8f, false).SetAnchorType(AnchorType.FullStretch);
            
            var slider = new EgSlider(canvas)
                .SetAnchorType(AnchorType.TopCenter)
                .SetPosition(0, 50)
                .SetSize(200, 40) as EgSlider;
            
            slider.SliderComponent.onValueChanged.AddListener(e =>
            {
                var value = slider.SliderComponent.value;
                parentImage.SetSize(300 * value, 200 * value);
                cloneParent.SetSize(300 * value, 200 * value);
            });
        }
    }
}