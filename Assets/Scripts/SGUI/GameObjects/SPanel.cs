using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGUI.Base;
using UnityEngine;
using UnityEngine.UI;

namespace SGUI.GameObjects
{
    class SPanel : SGameObject
    {
        public SPanel(
            SGameObject parent,
            bool isHorizontal = true,
            bool isRaycastTarget = true,
            string name = "SPanel"
        ) : base (parent, name,
            new Func<GameObject> (() =>
            {
                return UIFactory.CreatePanel (parent.GameObject, name);
            })
        ){
            new SHorizontalLayoutView(this, true);
            var image = gameObject.GetComponent<Image>();
            image.raycastTarget = isRaycastTarget;
        }
    }
}