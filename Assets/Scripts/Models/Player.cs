using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Presentor;
using EGUI.Base;
using UniRx;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public class Player : MonoBehaviour
    {
        public ReactiveProperty<int> Hp = new ReactiveProperty<int> (0);

        public ReactiveCollection<Item> Items = new ReactiveCollection<Item> ()
        {
            new Item ("薬草", "使うと体力が回復する"),
            new Item ("凄い薬草", "使うとメチャクチャハイになる"),
            new Item ("白い粉", "つかうとｆｄさおsdf")
        };

        ////public int Hp = 100;
        //private PlayerPresenter playerPresenter;

        //void Init ()
        //{
        //    //playerPresenter = new PlayerPresenter();
        //    //var change = gameObject.ObserveEveryValueChanged(_ => Hp);
        //    //change.Subscribe(_ => Debug.Log("aaa"));//hogeの値が変わると、呼ばれる
        //    //var change = Hp.ObserveEveryValueChanged(_ => _.Value);

        //}

        //void Start ()
        //{
        //    Hp.ObserveEveryValueChanged (_ => _.Value)
        //        .Subscribe (value => PlayerPresenter.OnHpChanged (value));
        //    Items.ObserveCountChanged ().Subscribe (_ => PlayerPresenter.OnCountChanged ());
        //}

        //public void UseItem (string id)
        //{
        //    var item = Items.ToList ().Find (i => i.Id == id);
        //    var itemName = item.Name;
        //    if (itemName == "薬草")
        //    {
        //        CanvasStack.PopAndPush (
        //            new PopupOk ("体力が20回復した！")
        //        );
        //        // CanvasStack.GotoNextState (
        //        //     new PopupOk ("体力が20回復した！"),
        //        //     TransitionType.Recurrent);
        //    }
        //    else if (itemName == "凄い薬草")
        //    {
        //        CanvasStack.PopAndPush (
        //            new PopupOk ("体力が20回復した！")
        //        );
        //        Hp.Value += 60;
        //    }
        //    else if (itemName == "白い粉")
        //    {
        //        CanvasStack.PopAndPush (
        //            new PopupOk ("体力が20回復した！")
        //        );
        //        Hp.Value += 100;
        //    }
        //    Items.Remove (item);
        //}

    }
}