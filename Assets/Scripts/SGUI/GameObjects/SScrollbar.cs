using System;
using SGUI.Base;
using UnityEngine;

namespace SGUI.GameObjects
{
    class SScrollbar : SGameObject
    {
        public SScrollbar (
            SGameObject parent,
            string name
        ) : base (parent, name,
            new Func<GameObject> (() =>
            {
                return UIFactory.CreateScrollBar (parent.GameObject, name);
            })
        ){}
    }
}