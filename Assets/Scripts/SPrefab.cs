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
        public SPrefab(GameObject parent, string name)
        {
            InitGameObject(parent, name);
        }

        public override void InitGameObject(object[] args){
            gameObject = UIFactory.CreatePrefab(args[0] as GameObject, args[1] as string);
        }
    }
}
