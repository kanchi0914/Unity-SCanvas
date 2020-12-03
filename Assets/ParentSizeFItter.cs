using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class ParentSizeFItter : MonoBehaviour
{
    // Start is called before the first frame update
    //
    // private RectTransform textRect;
    // private RectTransform buttonRect;
    //
    // void Start()
    // {
    //     
    //     var textObject = this.transform.Find("Text");
    //     var fitter = textObject.gameObject.AddComponent<ContentSizeFitter>();
    //     fitter.horizontalFit = fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
    //     textRect = textObject.GetComponent<RectTransform>();
    //     buttonRect = GetComponent<RectTransform>();
    //     // var rectTransform = textObject.GetComponent<RectTransform>();
    //     // this.GetComponent<RectTransform>().sizeDelta =
    //     //     new Vector2(rectTransform.sizeDelta.x + 10, rectTransform.sizeDelta.y + 10);
    // }
    //
    // // Update is called once per frame
    // void Update()
    // {
    //     buttonRect.sizeDelta =
    //         new Vector2(textRect.sizeDelta.x + 10, textRect.sizeDelta.y + 10);
    // }
}
