using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.Extensions;
using Assets.Scripts.SCanvases;
using HC.UI;
using UnityEngine;
using UnityEngine.UI;
using static SGUI.Base.Utils;
using SGUI.Base;
using SGUI.SGameObjects;
using static UnityEngine.UI.ScrollRect;
using static HC.UI.UICreator;
using Assets.Scripts.SGUI.SGameObjects.ComponentScripts;
using DG.Tweening;
using SGUI.SGameObjects.ComponentScripts;
using UnityEditor;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    void Start ()
    {
        var sc = new SCanvas ("aaaaaaaaa");

        var ssss = new SHorizontalLayoutView(sc, 3).SetRectSize(600, 50)
            .SetMiddleCenterAnchor().SetLocalPos(0,0);

        new SToggle(ssss);
        new SToggle(ssss);
        new SToggle(ssss);
        //new SToggle(ssss);

        //new SImage(ssss);
        //new SImage(ssss);
        //new SImage(ssss);
        //new SImage(ssss);


        //new MyToggle(ssss, "1111");
        //new MyToggle(ssss, "2222");
        //new MyToggle(ssss, "3333");
        //new MyToggle(ssss, "last");
        //new MyToggle(ssss);
        //new MyToggle(ssss);
        //new MyToggle(ssss);
        //new SImage(ssss);
        //new SImage(ssss);
        //new SImage(ssss);
    }
}

public class MyToggle : SToggle
{
    public MyToggle(SGameObject parent, string name) : base(parent, isWithBoxImage: false, isGrouped: true, name: name)
    {
        SetMiddleCenterAnchor().SetLocalPos(0, 0);
        SetRectSize(60, 100);
        SetBackGroundImage("Images/Tab");
        EnabledBackGroundImageColor = Color.white;
        DisabledBackGroundImageColor = Color.gray;
        EnabledTextColor = Color.black;
        DisabledTextColor = Color.black;
        Text.SetAlignMent(TextAnchor.MiddleCenter);
    }
}

public class MyImage : SImage
{
    public MyImage(SGameObject parent) : base(parent)
    {

    }
}