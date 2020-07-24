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

    public Button button;

    public GameObject obj;

    bool isB = false;

    Transform mainCanvas;
    Camera mainCamera;

    // Start is called before the first frame update
    void Start ()
    {
        mainCamera = GameObject.Find ("Main Camera").GetComponent<Camera> ();
        mainCanvas = GameObject.Find ("Main Canvas").transform;

        // var ggg = new SImage(sc, "");
        // ggg.GameObject.AddComponent<TestComponent>();
        // PrefabUtility.SaveAsPrefabAsset(ggg.GameObject, "Assets/TESDDSA.prefab");

        var sc = new SCanvas ("aaaaaaaaa");

        var baseImage = new SImage (sc, "aaaa").SetBackGroundColor (ColorType.White, 1)
            .SetLocalPosByRatio (0.2f, 0.2f).SetRectSize (400, 400);

        var myImage = new SImage (baseImage, "").SetBackGroundColor (ColorType.Green)
            .SetLocalPos (60, 90).SetRectSize (150, 200);

        var bottomLayout = new SVerticalGridScrollView (sc, "", 2, 3)
            .SetTopLeftAnchor ()
            .SetLocalPosByRatio (0f, 0.7f).SetRectSizeByRatio (1f, 0.3f);

        myImage.GameObject.AddComponent<RectChecker> ();

        //var sv = new SVerticalLayoutView(sc, 10).SetRectSizeByRatio(0.2f, 1f);
        var shl = new SHorizontalLayoutView(sc, 10).SetRectSize(300, 100)
        .SetMiddleCenterAnchor().SetLocalPos(0, 0).SetBackGroundColor(ColorType.Black)
        as SHorizontalLayoutView;
        
        // shl.SetSpacing(10);
        shl.SetPadding(10,10,10,10);

        var aaa = new SImage(shl, "");
        var aaa2 = new SImage(shl, "");
        var aaa3 = new SImage(shl, "");
    
    }

    private Vector3 getScreenTopLeft ()
    {
        // 画面の左上を取得
        Vector3 topLeft = mainCamera.ScreenToWorldPoint (Vector3.zero);
        // 上下反転させる
        topLeft.Scale (new Vector3 (1f, -1f, 1f));
        return topLeft;
    }

    private Vector3 getScreenBottomRight ()
    {
        // 画面の右下を取得
        Vector3 bottomRight = mainCamera.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, 0.0f));
        // 上下反転させる
        bottomRight.Scale (new Vector3 (1f, -1f, 1f));
        return bottomRight;
    }

    void addNewCanvas ()
    {
        var root = new GameObject ();
        root.name = "NEWCANVAS!!";
        Canvas canvas = root.AddComponent<Canvas> ();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = mainCamera;
        root.AddComponent<CanvasScaler> ();
        root.AddComponent<GraphicRaycaster> ();
    }
}