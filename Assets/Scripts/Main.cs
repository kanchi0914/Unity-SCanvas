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

public class Main : MonoBehaviour
{

    Transform mainCanvas;
    Camera mainCamera;

    // Start is called before the first frame update
    void Start ()
    {
        mainCamera = GameObject.Find ("Main Camera").GetComponent<Camera> ();
        mainCanvas = GameObject.Find ("Main Canvas").transform;
        // var ca = new MainMenu();
        var sc = new SCanvas ("aaaaaaaaa");

        // var sv = new SVerticalGridScrollView (sc, "sss", 3, 1);

        // sv.SetPadding (5, 5, 5, 5);
        // sv.SetSpacing (5, 5);

        // UIFactory.CreateTMProText(sc.GameObject, "", "sassssssss");

        UICreator.CreateToggle(sc.GameObject);

        // var cccc = 
        //new SButton (aaa, "tesst", "tetete");
        // new SButton (aaa, "tesst", "tetete");

        // new SButton (sv, "tesst", "tetete").AddOnClick (new Action (() => Debug.Log ("aaaaaaaaaaaaaaaa")));
        // new SButton (sv, "tesst", "tetete").AddOnClick (new Action (() => Debug.Log ("aaaaaaaaaaaaaaaa")));
        // new SButton (sv, "tesst", "tetete").AddOnClick (new Action (() => Debug.Log ("aaaaaaaaaaaaaaaa")));
        // new SButton (sv, "tesst", "tetete").AddOnClick (new Action (() => Debug.Log ("aaaaaaaaaaaaaaaa")));
        // new SButton (sv, "tesst", "tetete").AddOnClick (new Action (() => Debug.Log ("aaaaaaaaaaaaaaaa")));

        // UICreator.CreateInputField(sc.GameObject);

        // new SButton (aaa, "tesst", "tetete");
        // new SButton (aaa, "tesst", "tetete");
        // new SButton (aaa, "tesst", "tetete");
        // new SButton (aaa, "tesst", "tetete");
        // new SButton (aaa, "tesst", "tetete");
        // new SButton (aaa, "tesst", "tetete");

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