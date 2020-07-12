using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HC.UI;
using UnityEngine.UI;

public class MyTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    void fun()
    {
        // EventSystemが存在しない場合一緒に生成します
        Canvas canvas = UICreator.CreateCanvas();
        
        // キャンバスの子としてテキストを生成する
        Text text = UICreator.CreateText(canvas.gameObject);
        text.rectTransform.localPosition = Vector3.left * 140f;
        text.text = "Hello World!!";
        text.alignment = TextAnchor.MiddleCenter;
        text.color = Color.green;

        // オブジェクト名やコンポーネントの初期値を設定する
        Dropdown dropdown = UICreator.CreateDropdown(canvas.gameObject, "Gender", "性別");
        dropdown.GetComponent<RectTransform>().localPosition = Vector3.right * 140f;
        dropdown.options.Clear();
        dropdown.options.Add(new Dropdown.OptionData { text = "男性" });
        dropdown.options.Add(new Dropdown.OptionData { text = "女性" });
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
