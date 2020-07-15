using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Extensions;
using Assets.Scripts.Models;
using Assets.Scripts.Presentor;
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

        private PlayerPresenter playerPresenter;

        // set constructor
        public MainMenu()
        {
            parent = new SubCanvas(this, 0.1f, 0.1f, 0.8f, 0.8f);
            leftMenu = new SubCanvas("menu", parent, 0.0f, 0.0f, 0.2f, 1.0f)
                .SetBackGroundColor(ColorType.Black)
                as SubCanvas;
            var items = new SVerticalListItems(leftMenu).SetVerticalListItems(GetMenuList());
            new SPrefab(items, "MyButton");
            detailView = new SubCanvas("detailView", parent, 0.25f, 0.0f, 0.75f, 1.0f)
                .SetBackGroundColor(ColorType.Black)
                as SubCanvas;
        }

        public List<SButton> GetMenuList()
        {
            var menuList = new List<SButton>()
            {
                new SButton (this, "アイテム").AddOnClick(GetFunction(OnClickItem)),
                new SButton (this, "スキル").AddOnClick(GetFunction(OnClickSkill)),
                new SButton (this, "ステータス").AddOnClick(GetFunction(OnClickStatus)),
                new SButton (this, "オプション").AddOnClick(GetFunction(OnClickOption))
            };
            return menuList;
        }

        public List<String> Skills = new List<string>()
        {
            "naguke",
            "keur"
        };

        public List<SButton> GetItems()
        {
            var itemButtons = new List<SButton>();
            GameObject.Find("Player").GetComponent<Player>().Items.ToList().ForEach(i =>
           {
               itemButtons.Add(
                    new SButton(detailView, i.Name)
                    .AddOnClick(GetFunction(() => OnClickItemName(i.Id)))
                    );
           });
            return itemButtons;
        }

        public List<SButton> GetSkills()
        {
            var itemButtons = new List<SButton>();
            Skills.ForEach(i =>
           {
           itemButtons.Add(
               new SButton(detailView, i).AddOnClick(GetFunction(OnClickItem))
               );
           });
            return itemButtons;
        }

        public void SetItems()
        {
            detailView.ClearComponents();
            detailView.SetGridListItems(GetItems(), 4, 4);
        }

        public void OnClickItem()
        {
            SetItems();
        }

        public void OnClickSkillView()
        {
            detailView.ClearComponents();
            detailView.SetGridListItems(GetSkills(), 4, 4);
        }

        public void OnClickItemName(string id)
        {
            SCanvas sCanvas = new SCanvas().SetBackGroundColor(ColorType.White) as SCanvas;
            var popUp = new SubCanvas(sCanvas, 0.4f, 0.4f, 0.4f, 0.3f)
                .SetBackGroundColor(new Color(1, 1, 0, 1));
            new SButton(popUp, "使う")
                .SetLocalPos(0.2f, 0.5f)
                .SetRectSize(0.3f, 0.3f)
                .AddOnClick(GetFunction(() => PlayerPresenter.OnClickUseItem(id)));
            new SButton(popUp, "キャンセル")
                .SetLocalPos(0.6f, 0.5f)
                .SetRectSize(0.3f, 0.3f)
                .AddOnClick(GetFunction(OnClikcDiscardItem));
            CanvasStack.GotoNextState(sCanvas, TransitionType.Overlay);
        }

        public void OnClickUseItem()
        {
            var sCanvas2 = new SCanvas().SetBackGroundColor(ColorType.White);
            var popUp2 = new SubCanvas(sCanvas2, 0.4f, 0.4f, 0.6f, 0.4f);
            var ok2 = new SubCanvas(popUp2, 0.2f, 0.5f, 0.6f, 0.4f);
            new SButton(ok2, "OK")
                .AddOnClick(GetFunction(() => {
                    CanvasStack.Pop();
                    CanvasStack.Pop();
                }));
            CanvasStack.GotoNextState(sCanvas2 as SCanvas, TransitionType.Overlay);
        }

        public void OnClikcDiscardItem()
        {
            CanvasStack.Pop();
        }

        public void OnClickSkill()
        {
            Debug.Log("Skill is clicked");
        }

        public void OnClickStatus()
        {

        }

        public void OnClickEquip()
        {

        }

        public void OnClickOption()
        {

        }
    }
}