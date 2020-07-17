using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMono : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    void OnGUI () { 
        if (GUI.Button(new Rect (60,60,300,200), "ボタン")) {
            print ("OK!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
