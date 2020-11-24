using System;
using System.Collections.Generic;
using Assets.Scripts.Examples.AdvGame;
using Assets.Scripts.Extensions;
using Assets.Scripts.SGUI.Base;
using DG.Tweening;
using EGUI.Base;
using UnityEngine;

namespace EGUI.GameObjects
{
    public class EgMessageWindow : EGGameObject
    {

        // public List<Section> SentSections = new List<Section>();
        //
        // public Queue<Section> MessageQueue = new Queue<Section>();
        // private bool closesCanvas = true;

        // public Action OnSentEveryMessage { get; protected set; }

        public EGText MessageText { get; private set; }

        private static Sequence textSeq = DOTween.Sequence();

        public EGGameObject TalkerPanelImage;
        public EGText TalkerText;

        public EgMessageWindow(
            GameObject parent,
            string name = "SMessageWindow"
        ) : base(parent, name)
        {
            SetImageColor(Color.gray, 0.5f)
                .SetBottomCenterAnchor()
                .SetLocalPosByRatio(0, -0.1f)
                .SetRectSizeByRatio(0.8f, 0.3f);
            
            MessageText = new EGText(gameObject, "");
            MessageText
                .SetCharacter(fontSize: 36, font: UGUIResources.Font)
                .SetColor(Color.white)
                .SetParagraph(alignment: TextAnchor.UpperLeft);
                
            MessageText
                .SetLocalPosByRatio(0.05f, 0.1f)
                .SetRectSizeByRatio(0.9f, 0.8f)
                .SetFullStretchAnchor()
                .SetImageColor(Color.gray, 0.5f)
                .SetOnClick(OnClicked);

            TalkerPanelImage = new EGGameObject(gameObject)
                .SetImageColor(Color.gray, 0.5f)
                .SetTopLeftAnchor()
                .SetLocalPos(0, -60)
                .SetRectSize(200, 60);

            TalkerText = new EGText(TalkerPanelImage.gameObject, "")
                    .SetRectSizeByRatio(1, 1)
                    .SetFullStretchAnchor()
                as EGText;
            
            gameObject.GetImageComponent().raycastTarget = true;
        }

        protected virtual void OnClicked()
        {
        }

        public void SetTalkerText(string talker)
        {
            TalkerText.SetText(talker);
        }

        public void SetMessageText(string message)
        {
            MessageText.SetText(message);
        }

    }
}