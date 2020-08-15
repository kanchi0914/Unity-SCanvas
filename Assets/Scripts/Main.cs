using System;
using Assets.Scripts.Extensions;
using HC.UI;
using UnityEngine;
using UnityEngine.UI;
using static EGUI.Base.Utils;
using EGUI.Base;
using EGUI.GameObjects;
using static UnityEngine.UI.ScrollRect;
using static HC.UI.UICreator;

public class Main : MonoBehaviour
{
    private EgImage image;

    void Start()
    {
        //new RectTransformExtensionsTest2().TestSetAnchor();
        
        var canvas = new EGCanvas("test");
        //new EgSlider(canvas).SetRectSize(200, 40).SetLocalPos(300, 300);
        new EgToggle(canvas).SetLocalPos(300, 300)
            .SetRectSize(400, 80);
        new EGDropDown(canvas).SetLocalPos(200, 200).SetRectSize(300,100);
        //
        // var image = new EgImage(canvas).SetRectSize(300, 300);
        // var chi = new EgImage(image).SetMiddleLeftAnchor()
        //     .SetRectSize(100, 100)
        //     .SetLocalPos(0, 0)
        //     .SetColor(ColorType.Black);
        // chi.RectTransform.SetVerticalStretchWithLeftPivotAnchor();
    }


    public void Init()
    {
        var canvas = new EGCanvas("test");
        var root = new EgImage(canvas)
            .SetRectSize(300, 20)
            .SetMiddleCenterAnchor()
            .SetLocalPos(0, 0) as EgImage;

        var dropdownComponent = root.GameObject.TryAddComponent<Dropdown>();

        var label = new EGText(root, name: "label");
        var arrow = new EgImage(root, name: "arrow");
        var template = new PreEgVerticalLayoutScrollView(root, name: "template");


        //dropdownの設定
        dropdownComponent.targetGraphic = root.Image;
        dropdownComponent.template = template.RectTransform;
        dropdownComponent.captionText = label.TextComponent;

        new EGScrollBar(canvas);
    }
}