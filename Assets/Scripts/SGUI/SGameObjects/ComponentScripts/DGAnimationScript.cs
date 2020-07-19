using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.SGUI.SGameObjects.ComponentScripts
{
    public class DGAnimationScript : MonoBehaviour
    {
        private Sequence sequence;

        public void Start()
        {
            sequence = DOTween.Sequence();
            Animate();
        }

        public void Animate()
        {
            Debug.Log("START");
            sequence.Append(gameObject.transform.DOLocalMoveY(0f, 0.5f))
                .OnComplete(() =>
                {
                    Debug.Log("DONE!");
                });
        }
    }
}
