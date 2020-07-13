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
    public class MainMenu : QCanvas
    {
        // set constructor
        public MainMenu()
        {
            var parent = new SubCanvas(this, 0.1f, 0.1f, 0.8f, 0.8f);
            var menu = new SubCanvas(parent, 0f, 0f, 0.2f, 1f);
            var color = menu.GameObject.AddComponent<Image>();
            color.color = GetColor(Utils.ColorType.Black);
            menu.SetVerticalListItems(GetMenuList(), widthXratio: 0.9f);
            var menu2 = new SubCanvas(parent, 0.25f, 0f, 0.75f, 1f);
            var color2 = menu2.GameObject.AddComponent<Image>();
            color2.color = GetColor(Utils.ColorType.Black);
        }

        public List<SButton> GetMenuList()
        {
            var menuList = new List<SButton>()
            {
                new SButton(gameObject, "アイテム", 
                Func(OnClickItem), Func(OnSelectItem)),
                new SButton(gameObject, "スキル", Func(OnClickSkill)),
                //new SButton(gameObject, "装備", Func(OnClickEquip),
                //Func2(e => OnSelect("装備"))),
                //new SButton(gameObject, "ステータス", Func(OnClickStatus),
                //Func2(e => OnSelect("ステータス"))),
                //new SButton(gameObject, "オプション", Func(OnClickOption),
                //Func2(e => OnSelect("オプション")))
            };
            return menuList;
        }

        public Action Func(Action function)
        {
            return new Action(() => function());
        }

        public Action<string> Func2(Action<string> function)
        {
            return new Action<string>(function);
        }

        public void OnClickItem()
        {
            Debug.Log("Item is clicked");
        }

        public void OnSelectItem()
        {
            Debug.Log("selected!!!");
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
