using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGUI.Base;
using UnityEngine;

namespace SGUI.SGameObjects
{
    class SBaseRect : SGameObject
    {
        public SBaseRect(
            SGameObject parent,
            string name
        ) : base (parent, name,
            new Func<GameObject> (() =>
            {
                return UIFactory.CreateBaseRect (parent.GameObject, name);
            })
        ){}
    }
}