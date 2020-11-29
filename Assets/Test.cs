using System.Collections;
using System.Collections.Generic;
using EGUI.GameObjects;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.Find("Button").GetComponent<Button>().onClick.AddListener(() =>
        {
            Debug.Log("aaaaa");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
