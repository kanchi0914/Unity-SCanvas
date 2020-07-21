using Assets.Scripts.SGUI.SGameObjects.ComponentScripts;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class App : MonoBehaviour
{
    [SerializeField]
    private GameObject obj;

    [SerializeField]
    private Button button;

    public Sequence sequence;
    public Sequence sequence2;

    void Start()
    {
        sequence = DOTween.Sequence();
        sequence = DOTween.Sequence().Append(obj.transform.DOShakePosition(10f, 100, 100));
        button.onClick.AddListener(() =>
        {
            Animate();
            //Animate2();
            //Animate3();
        });
    }

    public void Animate1()
    {
        Debug.Log("STARTED_1");
        obj.transform.DOShakePosition(0.2f, 100, 100)
            .OnComplete(() => Debug.Log("COMPLETED_1"));
    }

    public void Animate()
    {
        Debug.Log("STARTED"); 
                //if (sequence.IsPlaying()) { sequence.Complete(); }
        //sequence.Play();
    }



    public void Animate3()
    {
        Debug.Log("STARTED_3");
        DOTween.Sequence().Append(obj.transform.DOShakePosition(0.2f, 100, 100))
            .OnComplete(() =>
            {
                Debug.Log("COMPLETED_3");
            });
    }
}

