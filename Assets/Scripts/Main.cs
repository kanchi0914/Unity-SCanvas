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

        // var dp = new SDropDown (sc, "", 60, 200);

        // dp.AddOption (("korewore", new Action (() => Debug.Log ("1111111111111111"))));
        // dp.AddOption (("dsadadasdasdas", new Action (() => Debug.Log ("22222222222222222"))));
        // dp.AddOption (("dsadadasdasdas", new Action (() => Debug.Log ("33333333333333333"))));

        // dp.AddOption (("korewore", new Action (() => Debug.Log ("44444444444444"))));
        // dp.AddOption (("dsadadasdasdas", new Action (() => Debug.Log ("555555555555555555"))));
        // dp.AddOption (("dsadadasdasdas", new Action (() => Debug.Log ("6666666666666666666"))));

        // dp.SetContentAreaImage("Black1");
        // dp.SetTemplateItemImage("Black1");
        // dp.SetTemplateTextConfig(24, ColorType.White, "Font/GenJyuuGothic-Medium");
        // dp.SetTopItemTextConfig(24, ColorType.Black, "Font/GenJyuuGothic-Bold");

        // var sv = new SVerticalListScrollView (sc, "aaaaa", visibleItemCount : 3)
        // .SetMovementType(MovementType.Clamped);
        // sv.SetRectSizeByRatio (0.4f, 0.6f);
        // sv.SetPadding (10, 30, 30, 10);
        // sv.SetSpacing (10);

        var svGrid = new SVerticalGridScrollView(sc, "dadsa", 2,2);

        var aaa = new SButton (svGrid, "tesst", "tetete");
        var bbb = new SButton (svGrid, "tesst", "tetete");
        var cccc = new SButton (svGrid, "tesst", "tetete");
        // var aaa222 = new SButton(sv, "tesst", "tetete");
        // sv.AddChild(aaa);
        // sv.AddChild(bbb);
        // sv.AddChild(aa2a);
        // sv.AddChild(aa233a);
        // sv.AddChild(aa32a);
        // sv.AddChild(aaa222);
        // new SButton(sv, "tesst", "tetete");
        // new SButton(sv, "tesst", "tetete");

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