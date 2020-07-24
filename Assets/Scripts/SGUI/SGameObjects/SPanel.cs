using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGUI.Base;
using UnityEngine;

namespace SGUI.SGameObjects
{
    class SToggleGroup : SGameObject
    {
        public SToggleGroup(
            SGameObject parent,
            bool isHorizontal = true,
            string name = "SToggleGroup"
        ) : base (parent, name,
            new Func<GameObject> (() =>
            {
                return UIFactory.CreatePanel (parent.GameObject, name);
            })
        ){
            new SHorizontalLayoutView(this, 3);
        }
    }
}