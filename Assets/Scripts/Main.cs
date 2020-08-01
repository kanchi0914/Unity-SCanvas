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
        new CommandBattleRpg();

        //var si = new SImage(sc).SetMiddleCenterAnchor().SetRectSize(300, 300);

        //var aa = new SImage(si).SetFullStretchAnchor();
        //aa.SetLocalPos(20, 20);
        //aa.SetRectSize(200, 200);
        //aa.SetBackGroundColor(ColorType.Black, 1f);



        //new RPG1();
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