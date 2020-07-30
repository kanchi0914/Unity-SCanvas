using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGUI.Base;
using UnityEngine;
namespace SGUI.GameObjects
{
    class SPrefab : SGameObject
    {

        public SPrefab (
            SGameObject parent = null,
            string name = "SPrefab"
        ) : base (parent, name,
            new Func<GameObject> (() =>
            {
                return UIFactory.CreatePrefab (parent.GameObject, name);
            })
        ) { }

        #region  RequiredMethods

        public new SPrefab SetBackGroundColor (ColorType colorType, float alpha)
        {
            return base.SetBackGroundColor (colorType, alpha) as SPrefab;
        }

        public new SPrefab SetBackGroundColor (Color color)
        {
            return base.SetBackGroundColor (color) as SPrefab;
        }

        public new SPrefab SetParentSGameObject (SGameObject parent)
        {
            return base.SetParentSGameObject (parent) as SPrefab;
        }

        public new SPrefab SetRectSizeByRatio (float ratioX, float ratioY)
        {
            return base.SetRectSizeByRatio (ratioX, ratioY) as SPrefab;
        }

        public new SPrefab SetLocalPosByRatio (float posXratio, float posYratio)
        {
            return base.SetLocalPosByRatio (posXratio, posYratio) as SPrefab;
        }

        //public virtual SGameObject SetRectSize(int width, int height)
        //{
        //    return this;
        //}

        //public virtual SGameObject SetLocalPos(int posX, int posY)
        //{
        //    return this;
        //}

        #endregion

    }
}