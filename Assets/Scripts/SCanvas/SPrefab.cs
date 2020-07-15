using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Assets.Scripts.UIFactory;

namespace Assets.Scripts
{
    class SPrefab : SGameObject
    {
        public SPrefab(SGameObject parent, string name)
        {
            InitGameObject(parent, name);
            SetParent(parent);
        }

        /// <summary>
        /// arg[0] needs to be SGameObject
        /// arg[1] needs to be sring
        /// </summary>
        /// <param name="args"></param>
        public override void InitGameObject(object[] args){
            gameObject = UIFactory.CreatePrefab((
                args[0] as SGameObject).GameObject, args[1] as string);
        }
    }
}
