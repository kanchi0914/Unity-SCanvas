using System;
using Assets.Scripts.Extensions;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts
{
    public class DoTweenAnimator : MonoBehaviour
    {
        private Sequence sequence;

        private void Start()
        {
            sequence = DOTween.Sequence();
        }

        public void AnimateShake()
        {
            if (sequence == null) return;
            if (sequence.IsPlaying())
            {
                sequence.Complete();
                sequence.Kill();
            }

            sequence = DOTween.Sequence()
                .Append(gameObject.transform.DOShakePosition(0.5f, strength: 20, vibrato: 30));
        }

        public void AnimateBlink()
        {
            if (sequence == null) return;
            if (sequence.IsPlaying())
            {
                sequence.Complete();
                sequence.Kill();
            }

            CanvasGroup imageCanvas = gameObject.GetOrAddComponent<CanvasGroup>();
            var preAlpha = imageCanvas.alpha;
            sequence = DOTween.Sequence()
                .Append(imageCanvas.DOFade(0.0f, 0.1f).SetLoops(5))
                .Append(imageCanvas.DOFade(preAlpha, 0.0f));
        }
    }
}