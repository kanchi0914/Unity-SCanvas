using System;
using Assets.Scripts.Examples.AdvGame;
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
    private EGImage image;

    void Start()
    {
        //new RectTransformExtensionsTest2().TestSetAnchor();

        var canvas = new EGCanvas("test");

        var layout = new EGVerticalLayoutScrollView(canvas);

        // //new EgSlider(canvas).SetRectSize(200, 40).SetLocalPos(300, 300);
        // layout.AddItem(new EGImage(layout));
        // layout.AddItem(new EGImage(layout));
        // layout.AddItem(new EGImage(layout));
        // new EgToggle(canvas).SetLocalPos(300, 300)
        //     .SetRectSize(400, 80);
        // new EGDropDown(canvas).SetLocalPos(200, 200).SetRectSize(300,100);


        // var layout = new EGGridLayoutView(canvas, columnCount: 3, rowCount: 3)
        //     .SetRectSize(200, 200);
        // new EGImage(layout);
        // new EGImage(layout);

        new AdvGameOpening();
    }
}