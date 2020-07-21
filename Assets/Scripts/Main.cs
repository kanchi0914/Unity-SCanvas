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
using DG.Tweening;
using Assets.Scripts.SGUI.SGameObjects.ComponentScripts;
using UnityEditor;
using SGUI.SGameObjects.ComponentScripts;

public class Main : MonoBehaviour
{

    public Button button;

    public GameObject obj;


    bool isB = false;

    Transform mainCanvas;
    Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        mainCanvas = GameObject.Find("Main Canvas").transform;

        var sc = new SCanvas("aaaaaaaaa");

        //SInputField ipf = new SInputField(sc, "input").SetLocalPos(300, 300)
        //    .SetRectSize(200, 50) as SInputField;

        //var baseImage = new SImage(sc, "aaaa").SetBackGroundColor(ColorType.White, 1)
        //    .SetLocalPosByRatio(0.2f, 0.2f).SetRectSizeByRatio(0.4f, 0.4f);

        //var myImage = new SImage(baseImage, "").SetBackGroundColor(ColorType.Green)
        //    .SetLocalPosByRatio(0.25f, 0.25f).SetRectSizeByRatio(0.5f, 0.5f);




        //var myImage = new SImage(sc, "aaaaa")
        //    .SetLocalPosByRatio(0.2f, 0.2f).SetRectSize(200,200);
        //SButton bt = new SButton(sc, "aaaa", "butttttto")
        //    .SetLocalPos(400, 400).SetRectSize(200, 100) as SButton;

        //    var button = new SButton(baseImage, "aaaaa", "dasdsadas")
        //.SetLocalPosByRatio(0.25f, 0.50f).SetRectSizeByRatio(0.5f, 0.5f);

        //var button = new SButton();
        //var myGrid = new SVerticalGridScrollView(sc, "gridview", 3, 3);

        //myGrid.SetRectSize(300, 300);

        //myGrid.AddChild(button);

        var ggg = new SImage(sc, "");
        ggg.GameObject.AddComponent<TestComponent>();
        PrefabUtility.SaveAsPrefabAsset(ggg.GameObject, "Assets/TESDDSA.prefab");


        //myGrid.AddChild(new SButton());
        //myGrid.AddChild(new SButton());
        //myGrid.AddChild(new SButton());

        //button.AddOnClick(() => Debug.Log("dsadsadsadad!!"));

        //myGrid.SetScrollbarVisibility(ScrollbarVisibility.Permanent);

        //PrefabUtility.SaveAsPrefabAsset(myGrid.GameObject, "Assets/GRID222.prefab");


        //Debug.Log("deataaaa");


        // var middleCenterButton = new SButton(sc, "mc", "MIDDLECENTER")
        //     .SetLocalPosByRatio(0.5f, 0.7f);

        // middleCenterButton.AddOnClick(() => myImage.SetMiddleCenterAnchor());

        // var leftTopButton = new SButton(sc, "lt", "LEFTTOP")
        //     .SetLocalPosByRatio(0.2f, 0.7f);

        // leftTopButton.AddOnClick(() => myImage.SetTopLeftAnchor());

        // var fullLectButton = new SButton(sc, "fl", "FULLLECT")
        //     .SetLocalPosByRatio(0.8f, 0.7f);

        // fullLectButton.AddOnClick(() => myImage.SetFullStretchAnchor());

        // var hoButton = new SButton(sc, "fl", "horizooo")
        //.SetLocalPosByRatio(0.9f, 0.7f);

        // hoButton.AddOnClick(() => myImage.SetHorizontalStretchAnchor());


    }

    private Vector3 getScreenTopLeft()
    {
        // 画面の左上を取得
        Vector3 topLeft = mainCamera.ScreenToWorldPoint(Vector3.zero);
        // 上下反転させる
        topLeft.Scale(new Vector3(1f, -1f, 1f));
        return topLeft;
    }

    private Vector3 getScreenBottomRight()
    {
        // 画面の右下を取得
        Vector3 bottomRight = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0.0f));
        // 上下反転させる
        bottomRight.Scale(new Vector3(1f, -1f, 1f));
        return bottomRight;
    }

    void addNewCanvas()
    {
        var root = new GameObject();
        root.name = "NEWCANVAS!!";
        Canvas canvas = root.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = mainCamera;
        root.AddComponent<CanvasScaler>();
        root.AddComponent<GraphicRaycaster>();
    }
}