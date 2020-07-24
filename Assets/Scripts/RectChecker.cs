using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectChecker : MonoBehaviour
{

    private RectTransform rectTransform;

    public float rectSizeX = 0;
    public float rectSizeY = 0;

    public float anchoredPosX = 0;

    public float anchoredPosY = 0;

    public float offsetMaxX = 0;

    public float offsetMaxY = 0;

    public float offsetMinX = 0;

    public float offsetMinY = 0;



    [SerializeField]
    public float RectSizeX { get { return rectTransform.sizeDelta.x;}  }
    
    [SerializeField]
    public float RectSizeY { get { return rectTransform.sizeDelta.y;}  }


    // Start is called before the first frame update
    void Start()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        rectSizeX = rectTransform.sizeDelta.x;
        rectSizeY = rectTransform.sizeDelta.y;
        offsetMaxX = rectTransform.offsetMax.x;
        offsetMaxY = rectTransform.offsetMax.y;
        offsetMinX = rectTransform.offsetMin.x;
        offsetMinY = rectTransform.offsetMin.y;
        anchoredPosX = rectTransform.anchoredPosition.x;
        anchoredPosY = rectTransform.anchoredPosition.y;
    }
}
