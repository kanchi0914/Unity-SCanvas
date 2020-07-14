using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Extensions;
using UnityEngine;
using UnityEngine.UI;
using static Assets.Scripts.Utils;

namespace Assets.Scripts.SCanvases
{
    public class MainMenu : SCanvas
    {
        private SubCanvas parent;

        private SubCanvas leftMenu;
        private SubCanvas detailView;

        // set constructor
        public MainMenu ()
        {
            parent = new SubCanvas (this, 0.1f, 0.1f, 0.8f, 0.8f);
            leftMenu = new SubCanvas ("menu", parent, 0.0f, 0.0f, 0.2f, 1.0f);
            leftMenu.SetBackGroundColor (ColorType.Black);
            leftMenu.SetVerticalListItems (GetMenuList (), widthXratio : 1.0f);
            detailView = new SubCanvas ("detailView", parent, 0.25f, 0.0f, 0.75f, 1.0f);
            detailView.SetBackGroundColor (ColorType.Black);

            //detailView.SetGridListItems(GetItems (), 4, 4);
        }

        public List<SButton> GetMenuList ()
        {
            var menuList = new List<SButton> ()
            {
                new SButton (gameObject, "アイテム",
                Func (SetItemView), Func (OnSelectItem)),
                new SButton (gameObject, "スキル", onClick:Func(SetSkillView)),
                new SButton (gameObject, "ステータス", Func (OnClickStatus)),
                new SButton (gameObject, "オプション", Func (OnClickOption))
            };
            return menuList;
        }

        public void SetItemView()
        {
            detailView.ClearComponents();
            detailView.SetGridListItems(GetItems (), 4, 4);
        }

        public void SetSkillView()
        {
            detailView.ClearComponents();
            detailView.SetGridListItems(GetSkills(), 4, 4);
        }

        public List<String> Items = new List<string> ()
        {
            "薬草",
            "薬草",
            "すごい薬草",
            "マッチ",
            "寝袋"
        };

        public List<String> Skills = new List<string> ()
        {
            "naguke",
            "keur"
        };

        public List<SButton> GetItems ()
        {
            var itemButtons = new List<SButton> ();
            Items.ForEach (i =>
            {
                itemButtons.Add (
                    new SButton (detailView.GameObject, i, onClick : Func (OnClickItem))
                );
            });
            return itemButtons;
        }

        public List<SButton> GetSkills ()
        {
            var itemButtons = new List<SButton> ();
            Skills.ForEach (i =>
            {
                itemButtons.Add (
                    new SButton (detailView.GameObject, i, onClick : Func (OnClickItem))
                );
            });
            return itemButtons;
        }

        public Action Func (Action function)
        {
            return new Action (() => function ());
        }

        public Action<string> Func2 (Action<string> function)
        {
            return new Action<string> (function);
        }

        public void OnClickItem ()
        {
            Debug.Log ("Item is clicked");
            detailView.ClearComponents();
        }

        public void OnSelectItem ()
        {
            Debug.Log ("selected!!!");
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