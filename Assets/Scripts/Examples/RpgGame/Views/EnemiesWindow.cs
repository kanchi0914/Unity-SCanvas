using System;
using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.Extensions;
using EGUI.Base;
using EGUI.GameObjects;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Examples.RpgGame.Views
{
    public class EnemiesWindow : EGGameObject
    {
        private List<EnemyView> enemyViews = new List<EnemyView>();
        public EGCanvas enemySelectingCanvas;

        public EnemiesWindow(RpgCanvas canvas, CommandBattle commandBattle, List<Enemy> enemies) : base(canvas)
        {
            SetRelativeSize(1, .5f);
            var layoutView = new EgHorizontalLayoutView(this).SetRelativeSize(1, 1).As<EgHorizontalLayoutView>();
            layoutView.SetChildAliments(TextAnchor.MiddleCenter).SetPaddingAndSpacing(spacing: 10);
            enemies.ForEach(enemy =>
            {
                var view = new EnemyView(layoutView, enemy);
                enemyViews.Add(view);
            });
        }

        /// <summary>
        /// 敵画像を表示する画面
        /// </summary>
        public class EnemyView : EGGameObject
        {
            public Enemy Enemy;
            private RpgText nameTextObject;
            private RpgText hpTextObject;
            private DoTweenAnimator animator;
            private string HpText => $"HP:{Enemy?.CurrentHp,3}/{Enemy?.MaxHp,3}";

            public EnemyView(EGGameObject parent, Enemy enemy) : base(parent, enemy.Id)
            {
                Enemy = enemy;
                SetImage(enemy.Image);
                SetImageAspectRatio(this, enemy.Image);

                SetSize(128, 0);
                var collider2D = gameObject.AddComponent<BoxCollider2D>();
                collider2D.size = new Vector2(128, 128);
                gameObject.tag = "Enemy";
                gameObject.ObserveEveryValueChanged(_ => gameObject.GetRectTransform().sizeDelta)
                    .Subscribe(_ => collider2D.size = gameObject.GetRectTransform().sizeDelta);
                Enemy = enemy;
                hpTextObject = new RpgText(this)
                    .SetText(HpText)
                    .SetParagraph(TextAnchor.LowerCenter)
                    .SetSize(128, 25)
                    .SetAnchorType(AnchorType.BottomCenter)
                    .As<RpgText>();
                hpTextObject.TextComponent.raycastTarget = false;

                nameTextObject = new RpgText(this)
                    .SetText(Enemy.Name)
                    .SetParagraph(TextAnchor.LowerCenter)
                    .SetSize(128, 25)
                    .SetAnchorType(AnchorType.BottomCenter)
                    .SetPosition(0, 25)
                    .As<RpgText>();

                animator = gameObject.GetOrAddComponent<DoTweenAnimator>();
                enemy.AddOnHpDecreased(hp =>
                {
                    hpTextObject.SetText(HpText);
                    ClearEnemyIfDead();
                    animator.AnimateBlink();
                });
                ClearEnemyIfDead();
            }

            public void ClearEnemyIfDead()
            {
                if (!Enemy.IsAlive)
                {
                    gameObject.GetOrAddComponent<CanvasGroup>().alpha = 0;
                }
            }

            public void SetSelectable(bool isSelectable)
            {
                if (isSelectable)
                {
                    AddEvent(EventTriggerType.PointerEnter, () => SetSize(160, 160));
                    AddEvent(EventTriggerType.PointerExit, () => SetSize(128, 128));
                }
                else
                {
                    SetSize(128, 128);
                    RemoveAllEvent();
                }
            }

            private void SetImageAspectRatio(EGGameObject imageObject, Sprite source)
            {
                var ratio = source.bounds.size.x / source.bounds.size.y;
                var fitter = imageObject.gameObject.GetOrAddComponent<AspectRatioFitter>();
                fitter.aspectMode = AspectRatioFitter.AspectMode.WidthControlsHeight;
                fitter.aspectRatio = ratio;
            }
        }

        public void SetSelectMode(Action<string> callbackSelected, Action callbackCanceled)
        {
            enemySelectingCanvas = new EGCanvas();
            var label = new AutoResizedTextLabel(enemySelectingCanvas, "誰に？")
                .SetAnchorType(AnchorType.BottomLeft)
                .SetPosition(60, 148);

            var hasClicked = false;
            enemyViews.ForEach(e => e.SetSelectable(true));
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
                        var clickedGameObject = hit2d.transform.gameObject.name;
                        if (hit2d.transform.gameObject.tag == "Enemy")
                        {
                            hasClicked = true;
                            // enemySelectingCanvas?.DestroySelf();
                            DisableEnemySelecting();
                            callbackSelected.Invoke(hit2d.transform.gameObject.name);
                        }
                    }
                }).AddTo(gameObject);
        }

        public void DisableEnemySelecting()
        {
            foreach (var enemyView in enemyViews)
            {
                enemyView.SetSelectable(false);
            }

            enemySelectingCanvas?.DestroySelf();
            enemyViews.ForEach(e => e.SetSelectable(false));
        }
    }
}