using System;

using HC.UI;
using UnityEngine;
using UnityEngine.UI;
using static SGUI.Base.Utils;
using SGUI.Base;
using SGUI.GameObjects;
using static UnityEngine.UI.ScrollRect;
using static HC.UI.UICreator;

public class Main : MonoBehaviour
{

    private SImage image;
    void Start ()
    {
        var sc = new SCanvas("aaaaaaaaa");

        //var aaa = new SSubCanvas(sc).SetLocalPosByRatio(0.1f, 0.2f).SetRectSizeByRatio(0.8f, 0.4f);

        //var one = new SSelectableImage(aaa, "Images/animal_gorilla").SetRectSize(100, 100).SetMiddleCenterAnchor().SetLocalPos(0,0);
        //var two = new SSelectableImage(aaa, "Images/animal_gorilla")
        //    .SetRectSize(100, 100).SetMiddleCenterAnchor().SetLocalPosByRatio(-0.25f, 0);
        //var three = new SSelectableImage(aaa, "Images/animal_gorilla")
        //    .SetRectSize(100, 100).SetMiddleCenterAnchor().SetLocalPosByRatio(0.25f, 0);

        //var commandCanvas = new SSubCanvas(sc).SetLocalPosByRatio(0, 0.7f).SetRectSizeByRatio(1f, 0.3f);

        //var commandButtonsView = new SGridLayoutView(commandCanvas, 3, 2).SetRectSizeByRatio(0.4f, 1f);

        //var attackButton = new SButton(commandButtonsView, "攻撃");
        //var guardButton = new SButton(commandButtonsView, "防御");
        //var skillButton = new SButton(commandButtonsView, "スキル");
        //var itemButton = new SButton(commandButtonsView, "アイテム");
        //var excapeButton = new SButton(commandButtonsView, "逃げる");

        new RPG1();


    }
}

public class MyToggle : SToggle
{
    public MyToggle(SGameObject parent, string name) : base(parent, isWithBoxImage: true, isGrouped: true, name: name)
    {
        SetAnchorType(AnchorType.MiddleCenter).SetLocalPos(0, 0);
        SetRectSize(400, 50);
        SetBackGroundImage("Images/Tab");
        EnabledBackGroundImageColor = Color.white;
        DisabledBackGroundImageColor = Color.gray;
        EnabledTextColor = Color.black;
        DisabledTextColor = Color.black;
        Text.SetTextAnchor(TextAnchor.MiddleCenter);
    }
}

public class MyImage : SImage
{
    public MyImage(SGameObject parent) : base(parent)
    {

    }
}