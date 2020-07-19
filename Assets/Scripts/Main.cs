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

public class Main : MonoBehaviour
{

    public Button button;

    public GameObject obj;

    Transform mainCanvas;
    Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        mainCanvas = GameObject.Find("Main Canvas").transform;

        //var sc = new SCanvas("aaaaaaaaa");

        //SInputField ipf = new SInputField(sc, "input").SetLocalPos(300, 300)
        //    .SetRectSize(200, 50) as SInputField;

        //SImage panel = new SImage(sc, "img").SetLocalPos(330, 230) as SImage;
        ////panel.SetActive(false);
        //panel.SetActive(false);
        ////panel.SetActive(true);
        //panel.SetRectSize(100, 40);

        //ipf.SetOnSelect(() => panel.SetActive(true));
        //ipf.SetOnDeselect(() => panel.SetActive(false));

        //var myImage = new SImage(sc, "aaaaa").SetLocalPos(300, 300);
        //var bt = new SButton(sc, "aaaa", "butttttto")
        //    .SetLocalPos(400, 400).SetRectSize(200, 100) as SButton;

        //var obj = new GameObject();
        //var objAnime = obj.TryAddComponent<DGAnimationScript>();
        ////bt.AddOnClick(() =>
        //{
        //    objAnime.Animate();
        //});

        obj = new GameObject();
        var objAnime = obj.TryAddComponent<DGAnimationScript>();
        button.onClick.AddListener(() =>
        {
            objAnime.Animate();
        });

        //Sequence seq = DOTween.Sequence();
        //seq.Append(obj.transform.DOMoveX(0, 1f))
        //    .OnComplete(() => Debug.Log("done!!!!!"));





        //new SSlider(sc, "slider").SetRectSize(400, 40)
        //    .SetLocalPosByRatio(0.1f, 0.1f);


        //var ccc = new GameObject("dadas");
        //var rect = ccc.AddComponent<RectTransform>();
        //UIFactory.SetTopLeftAnchor(rect);
        //ccc.transform.SetParent(sc.GameObject.transform, false);
        //rect.localPosition = new Vector3(100, 100, 0);

        //aaa.GameObject.transform.localPosition = new Vector3(0,0,0);

        //var aaadas = new GameObject();
        //aaadas.transform.SetParent(sc.GameObject.transform);

        // var ca = new MainMenu();
        //
        // var sv = new SVerticalGridScrollView (sc, "sss", 3, 1);

        // sv.SetPadding (5, 5, 5, 5);
        // sv.SetSpacing (5, 5);

        // UIFactory.CreateTMProText(sc.GameObject, "", "sassssssss");

        //UICreator.CreateToggle(sc.GameObject);

        //var aaa = new SToggle(sc, "aa").SetLocalPosByRatio(0.2f, 0.2f)
        //    .SetLocalPos(100, 100)
        //    .SetRectSize(300, 80);

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