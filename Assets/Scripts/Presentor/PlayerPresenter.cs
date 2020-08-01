using Assets.Scripts.Models;
using SGUI.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Presentor
{
    public class PlayerPresenter
    {
        //private Player player;
        //void Init()
        //{
        //    player = GameObject.Find("Player").GetComponent<Player>();
        //}
        
        public static void OnClickUseItem(string id)
        {
            //var player = GameObject.Find("Player").GetComponent<GameInfos>();
            //player.UseItem(id);
        }
        
        public static void OnHpChanged(int value)
        {
            Debug.Log(value);
        }

        public static void OnCountChanged()
        {
            //Debug.Log("dsdads");
            //MainMenu a = CanvasStack.Stack.ToList().Find(i => i.GetType() == typeof(MainMenu)) 
            //    as MainMenu;
            //a.SetItems();
        }
    }
}
