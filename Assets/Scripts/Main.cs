using Assets.Scripts.Examples.AdvGame;
using Assets.Scripts.Extensions;
using UnityEngine;
using UnityEngine.UI;
using static EGUI.Base.Utils;
using EGUI.Base;
using EGUI.GameObjects;

public class Main : MonoBehaviour
{
    void Start()
    {
        //new EgSlider(null).SetLocalPos(300,300);

        // var layout = new EGHorizontalLayoutScrollView()
        //     .SetLocalPos(200, 200)
        //     .SetRectSize(200, 200);
        // new EGButton(layout);
        // new EGButton(layout);
        // new EGButton(layout);
        // new EGButton(layout);
        // new EGButton(layout);
        // new EGButton(layout);
        // new EGButton(layout);
        // new EGButton(layout);

        // var titleText = new EGText(text: "たのしいアドベンチャーゲーム")
        //     // .SetCharacter(font: GUIData.GenjuGothicBold, fontSize: 48)
        //     .SetMiddleCenterAnchor()
        //     .SetRectSizeByRatio(0.8f, 0.2f)
        //     .SetLocalPosByRatio(0, 0.25f);

        // new EGButton().SetTopRightAnchor()
        //     .SetRectSizeByRatio(0.8f, 0.2f)
        //     .SetLocalPosByRatio(0, 0.25f);
        //
        new AdvGameOpening();


        // var parent = new GameObject( "parent");
        // parent.OnTransformChildrenChangedAsObservable()
        //     .Subscribe(g =>
        //     {
        //         Debug.Log("child object is added?????");
        //     });
        // parent.ObserveEveryValueChanged(_ => gameObject.transform.position)
        //     .Subscribe(e => Debug.Log(e));
        // var child = new GameObject("child");
        // child.transform.SetParent(parent.transform);
        // child.transform.position = new Vector3(10,101,0);

        // new RectTransformExtensionsTest2();


        // var canvas = new EGCanvas("test");

        // var button = new EGButton(canvas.gameObject, "");
        // button.gameObject.SetMiddleCenterAnchor().SetLocalPos(0, 0);
        // button.TextObject.SetText("aaaaaaa");

        // var view = new EGVerticalLayoutScrollView(canvas.gameObject);
        // view.gameObject.SetMiddleCenterAnchor().SetLocalPos(0, 0)
        //     .SetRectSize(200, 200);


        // var gridView = new EGVerticalLayoutScrollView(canvas.gameObject, true);
        // // var gridView = new EGHorizontalLayoutScrollView(canvas.gameObject, isAutoSizingHeight:true);
        // gridView.gameObject.SetMiddleCenterAnchor().SetLocalPos(0, 0)
        //     .SetRectSize(150, 300);
        // new EGButton(gridView.ContentAreaObject.gameObject, "aaaaa");
        // new EGButton(gridView.ContentAreaObject.gameObject, "aaaaa");
        // new EGButton(gridView.ContentAreaObject.gameObject, "aaaaa");
        // new EGButton(gridView.ContentAreaObject.gameObject, "aaaaa");
        // new EGButton(gridView.ContentAreaObject.gameObject, "aaaaa");
        // new EGButton(gridView.ContentAreaObject.gameObject, "aaaaa");

        //
        // new EgSlider(canvas.gameObject)
        //     .gameObject.SetRectSize(200, 30)
        //     .SetMiddleCenterAnchor()
        //     .SetLocalPos(0, 0);

        // new EGToggle(canvas.gameObject)
        //     .gameObject.SetRectSize(200, 40)
        //     .SetMiddleCenterAnchor()
        //     .SetLocalPos(0, 0);

        //
        // var dp = new EGDropDown();
        // dp.SetRectSize(300, 50)
        //     .SetMiddleCenterAnchor()
        //     .SetLocalPos(0, 0);
        //
        // dp.AddOption("option1", null);
        // dp.AddOption("option2", null);
        // dp.AddOption("option3", null);
        // dp.AddOption("option4", null);

        // new EGToggle().SetLocalPos(300,300);

        //


        // new EGButton(gridView.ContentAreaObject.gameObject, "aaaaa");
        // new EGButton(gridView.ContentAreaObject.gameObject, "aaaaa");
        // gridView.gameObject.SetRectSize(300, 300);


        // var image2 = new EGGameObject(view.ContentAreaObject.gameObject);

        // var scroll = new EGScrollBar(canvas.gameObject);
        // scroll.gameObject.SetMiddleCenterAnchor().SetRectSize(200, 40).SetLocalPos(0, 0);
    }
}