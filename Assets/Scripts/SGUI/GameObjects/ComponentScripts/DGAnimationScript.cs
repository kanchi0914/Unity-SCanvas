using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.SGUI.SGameObjects.ComponentScripts
{
    public class AnimationScript : MonoBehaviour
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
            gameObject.transform.DOLocalMoveY(0, 0.2f)
                .OnComplete(() => Debug.Log("asdsa"));
            sequence = DOTween.Sequence().Append(gameObject.transform.DOLocalMoveY(10f, 0.2f))
                .OnComplete(() =>
                {
                    Debug.Log("DONE!");
                });
        }
    }
}
