using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Extensions;
using Assets.Scripts.Models;
using Assets.Scripts.Presentor;
using SGUI.Base;
using SGUI.SGameObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class MainMenu : SCanvas
    {
        private SSubCanvas parent;

        private SVerticalListItems leftMenu;
        private SGridLayoutView detailView;

        private PlayerPresenter playerPresenter;

        // set constructor
        public MainMenu ()
        {
            parent = new SSubCanvas (this, 0.1f, 0.1f, 0.8f, 0.8f);
            leftMenu = new SVerticalListItems (parent, 0.0f, 0.0f, 0.2f, 1.0f)
                .SetBackGroundColor (ColorType.Black, 0.4f)
                .SetVerticalListItems (GetMenuList ());
            detailView = new SGridLayoutView (parent, 0.25f, 0.0f, 0.75f, 1.0f)
                .SetBackGroundColor (ColorType.Black, 0.4f);
        }

        public List<SGameObject> GetMenuList ()
        {
            var menuList = new List<SGameObject> ()
            {
                new SButton (text: "アイテム").AddOnClick (GetFunction (OnClickItem)),
                new SButton (text: "スキル").AddOnClick (GetFunction (OnClickSkill)),
                new SButton (text: "ステータス").AddOnClick (GetFunction (OnClickStatus)),
                new SButton (text: "オプション").AddOnClick (GetFunction (OnClickOption)),
                new SPrefab (null, "MyButton")
            };
            return menuList;
        }

        public List<String> Skills = new List<string> ()
        {
            "naguke",
            "keur"
        };

        public List<SGameObject> GetItems ()
        {
            var itemButtons = new List<SGameObject> ();
            GameObject.Find ("Player").GetComponent<Player> ().Items.ToList ().ForEach (i =>
            {
                itemButtons.Add (
                    new SButton (detailView, i.Name)
                    .AddOnClick (new Action (() => OnClickItemName (i.Id)))
                );
            });
            return itemButtons;
        }

        public List<SGameObject> GetSkills ()
        {
            var itemButtons = new List<SGameObject> ();
            Skills.ForEach (i =>
            {
                itemButtons.Add (
                    new SButton (detailView, i).AddOnClick (GetFunction (OnClickItem))
                );
            });
            return itemButtons;
        }

        public void SetItems ()
        {
            detailView.ClearComponents ();
            detailView.SetGridListItems (GetItems (), 4, 4);
        }

        public void OnClickItem ()
        {
            SetItems ();
        }

        public void OnClickSkillView ()
        {
            detailView.ClearComponents ();
            detailView.SetGridListItems (GetSkills (), 4, 4);
        }

        public void OnClickItemName (string id)
        {
            SCanvas sCanvas = new SCanvas ().SetBackGroundColor (ColorType.White, 0.8f) as SCanvas;
            var popUp = new SSubCanvas (sCanvas, 0.4f, 0.4f, 0.4f, 0.3f)
                .SetBackGroundColor (Utils.GetColor (ColorType.Cyan, 0.8f));
            new SButton (popUp, "使う")
                .SetLocalPosByRatio (0.2f, 0.5f)
                .SetRectSizeByRatio (0.3f, 0.3f)
                .AddOnClick (GetFunction (() => PlayerPresenter.OnClickUseItem (id)));
            new SButton (popUp, "キャンセル")
                .SetLocalPosByRatio (0.6f, 0.5f)
                .SetRectSizeByRatio (0.3f, 0.3f)
                .AddOnClick (GetFunction (OnClikcDiscardItem));
            // CanvasStack.GotoNextState (sCanvas, TransitionType.Overlay);
        }

        public void OnClickUseItem ()
        {
            var sCanvas2 = new SCanvas ().SetBackGroundColor (ColorType.White, 0.8f);
            var popUp2 = new SSubCanvas (sCanvas2, 0.4f, 0.4f, 0.6f, 0.4f);
            var ok2 = new SSubCanvas (popUp2, 0.2f, 0.5f, 0.6f, 0.4f);
            new SButton (ok2, "OK")
                .AddOnClick (GetFunction (() =>
                {
                    CanvasStack.Pop ();
                    CanvasStack.Pop ();
                }));
            // CanvasStack.GotoNextState (sCanvas2 as SCanvas, TransitionType.Overlay);
        }

        public void OnClikcDiscardItem ()
        {
            CanvasStack.Pop ();
        }

        public void OnClickSkill ()
        {
            Debug.Log ("Skill is clicked");
        }

        public void OnClickStatus ()
        {

        }

        public void OnClickEquip ()
        {

        }

        public void OnClickOption ()
        {

        }
    }
}