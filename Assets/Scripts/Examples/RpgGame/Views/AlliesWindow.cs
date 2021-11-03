using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using Assets.Scripts.Extensions;
using EGUI.Base;
using EGUI.GameObjects;
using UniRx;
using UnityEngine;

namespace Examples.RpgGame.Views
{
    public class AlliesWindow : EGGameObject
    {
        private List<AllyView> allyViews = new List<AllyView>();

        // public AutoResizedTextLabel whomLabel;
        public EGCanvas allySelectingCanvas;
        
        public AlliesWindow(RpgCanvas canvas, CommandBattle commandBattle, List<Ally> allies) : base(canvas,
            "AlliesWindow")
        {
            SetAnchorType(AnchorType.TopCenter)
                .SetPosition(0, -10)
                .SetSize(480, 128);
            var layoutView = new EgHorizontalLayoutView(this, isAutoAlignment: true)
                .SetRelativeSize(1, 1) as EgHorizontalLayoutView;
            layoutView.LayoutComponent.childAlignment = TextAnchor.MiddleCenter;
            layoutView.SetPaddingAndSpacing(2);

            allies.ForEach(ally =>
            {
                var view = new AllyView(layoutView, commandBattle, ally);
                allyViews.Add(view);
            });
        }


        public void SetSelectMode(Action<string> callbackSelected, Action callbackCanceled)
        {
            allySelectingCanvas = new EGCanvas("AllySelecting");
            // new EGGameObject(allySelectingCanvas).SetRelativeSize(1, 1).SetImageColor(Color.clear);
            // foreach (AllyView allyView in allyViews)
            // {
            //     allyView.gameObject.SetParent(allySelectingCanvas.gameObject);
            // }
            var whomLabel = new AutoResizedTextLabel(allySelectingCanvas, "誰に？")
                .SetAnchorType(AnchorType.TopLeft)
                .SetPosition(60, -180)
                .As<AutoResizedTextLabel>();
            
            var hasClicked = false;
            var stream1 = Observable.EveryUpdate().Where(_ => hasClicked);
            Observable
                .EveryUpdate()
                .TakeUntil(stream1)
                .Where(_ => Input.GetMouseButtonDown(0))
                .Subscribe(_ =>
                {
                    var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    var hit2d = Physics2D.Raycast(ray.origin, ray.direction);
                    if (hit2d != null && hit2d.transform?.gameObject != null)
                    {
                        if (hit2d.transform.gameObject.tag == "Ally")
                        {
                            hasClicked = true;
                            allySelectingCanvas?.DestroySelf();
                            callbackSelected.Invoke(hit2d.transform.gameObject.name);
                            return;
                        }
                    }
                    // hasClicked = true;
                    // allySelectingCanvas.DestroySelf();
                    // callbackCanceled.Invoke();
                }).AddTo(gameObject);
        }

        public void SetCommand(Ally ally, string commandText)
        {
            allyViews.Find(a => a.Ally == ally).SetCommandView(commandText);
        }

        public void ClearCommandView(Ally ally)
        {
            allyViews.Find(a => a.Ally == ally).ClearCommandView();
        }

        public void ClearCommandViews()
        {
            allyViews.ForEach(a => a.ClearCommandView());
        }

        public void DisableAllySelecting()
        {
            allySelectingCanvas.DestroySelf();
            // whomLabel?.DestroySelf();
        }


        /// <summary>
        /// 味方の情報を表示する画面
        /// </summary>
        public class AllyView : EGGameObject
        {
            private CommandBattle commandBattle;

            public EGGameObject CommandView;
            public RpgText CommandText;

            public Ally Ally;
            EGText nameTextObject;
            EGText hpTextObject;
            EGText mpTextObject;
            private EGText statusTextObject;
            private DoTweenAnimator animator;

            public string StatusText => $"{Ally.Job[0]}:Lv{Ally.Lv,2}";

            public string HpText => $"HP:{Ally?.CurrentHp,3}/{Ally?.MaxHp,3}";
            public string MpText => $"MP:{Ally.CurrentMp,3}/{Ally.MaxMp,3}";

            public AllyView(EGGameObject parent, CommandBattle commandBattle, Ally ally) : base(parent, ally.Id)
            {
                this.commandBattle = commandBattle;
                this.Ally = ally;
                animator = gameObject.AddComponent<DoTweenAnimator>();
                var collider = gameObject.GetOrAddComponent<BoxCollider2D>();
                gameObject.ObserveEveryValueChanged(_ => RectSize)
                    .Subscribe(_ => collider.size = RectSize);
                gameObject.tag = "Ally";
                SetSize(115, 128).SetImage("Images/clear_box");

                var layoutView = new EGVerticalLayoutView(this, true, isAutoAlignment: true)
                    .SetRelativeSize(1, 1) as EGVerticalLayoutView;
                layoutView.LayoutComponent.childAlignment = TextAnchor.UpperCenter;

                nameTextObject = new RpgText(layoutView)
                    .SetText(ally.Name)
                    .SetRelativeSize(1, 0.25f).As<RpgText>();
                hpTextObject = new RpgText(layoutView)
                    .SetText(HpText)
                    .SetRelativeSize(1, 0.25f).As<RpgText>();
                mpTextObject = new RpgText(layoutView)
                    .SetText(MpText)
                    .SetRelativeSize(1, 0.25f).As<RpgText>();
                statusTextObject = new RpgText(layoutView)
                    .SetText(StatusText)
                    .SetRelativeSize(1, .25f).As<RpgText>();

                gameObject.ObserveEveryValueChanged(_ => ally.CurrentHp).Subscribe(_ => hpTextObject.SetText(HpText));
                gameObject.ObserveEveryValueChanged(_ => ally.CurrentMp).Subscribe(_ => mpTextObject.SetText(MpText));
                ally.AddOnHpDecreased(hp =>
                {
                    hpTextObject.SetText(HpText);
                    if (Ally.CurrentHp < Ally.MaxHp * 0.2) SetColor(Color.red, 0.6f);
                    animator.AnimateBlink();
                });
            }

            public void SetColor(Color c, float alpha)
            {
                SetImageColor(c, alpha);
                nameTextObject.SetTextColor(c, alpha);
                hpTextObject.SetTextColor(c, alpha);
                mpTextObject.SetTextColor(c, alpha);
                statusTextObject.SetTextColor(c, alpha);
            }

            public void SetCommandView(string commandText)
            {
                CommandView?.DestroySelf();
                CommandView = new EGGameObject(this)
                    .SetImage("Images/clear_box")
                    .SetSize(115, 40).SetAnchorType(AnchorType.TopCenter)
                    .SetPosition(0, -138);
                CommandText = new RpgText(CommandView).SetCharacter(fontSize: 15).SetSize(95, 40).As<RpgText>();
                CommandText.SetText(commandText);
            }

            public void ClearCommandView()
            {
                CommandView.DestroySelf();
            }
        }
    }
}