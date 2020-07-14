using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HC.UI;
using Assets.Scripts.Extensions;
using UnityEngine.UI;
using Assets.Scripts;
using System;
using Assets.Scripts.SCanvases;

public class Main : MonoBehaviour
{

    Transform mainCanvas;
    Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        mainCanvas = GameObject.Find("Main Canvas").transform;
        //var aaa = new SButton(new Action(() => Debug.Log("NO1!")));
        //List<SButton> buttons = new List<SButton>()
        //{
        //    aaa,
        //    new SButton(new Action(() => Debug.Log("NO222222!"))),
        //    new SButton(new Action(() => QCanvasGenerator.addNewCanvas()))
        //};

        //QCanvas mult = new MultiCanvas();
        var ca = new MainMenu();
        //QCanvas qCanvas = new MainMenu();

        

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
