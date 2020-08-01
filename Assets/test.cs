using SGUI.GameObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    Vector3 originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        var obj = gameObject.FindDeep("Image");
        var rect = GameObject.Find("RECT");
        Debug.Log("rect0" + rect.transform.position);
        originalPosition = obj.transform.position;
        Debug.Log("pre" + obj.transform.position);
        //StartCoroutine(AdjustTransInTheEndOfFrame(rect));
        rect.transform.position = obj.transform.position;
        Debug.Log("after" + rect.transform.position);
        
    }

    private IEnumerator AdjustTransInTheEndOfFrame(GameObject obj)
    {
        yield return new WaitForEndOfFrame();
        obj.transform.position = originalPosition;
        obj.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
