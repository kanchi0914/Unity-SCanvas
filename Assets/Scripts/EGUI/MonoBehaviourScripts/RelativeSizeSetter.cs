using System;
using Assets.Scripts.Extensions;
using UniRx;
using UnityEngine;
using EGUI.Base;
using UnityEngine.Serialization;

namespace Assets.Scripts.EGUI.MonoBehaviourScripts
{
    public class RelativeSizeSetter : MonoBehaviour
    {
        [SerializeField]
        public float RatioX;
        [SerializeField]
        public float RatioY;
        private void Start()
        {
            var transform = gameObject.GetRectTransform();
            gameObject.ObserveEveryValueChanged(_ => transform.GetParentRectSize())
                .Subscribe(_ =>
                {
                    gameObject.GetRectTransform().SetSize(transform.GetParentRectSize().x * RatioX,
                        transform.GetParentRectSize().y * RatioY);
                });
        }
    }
}