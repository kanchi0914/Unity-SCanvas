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
            gameObject = UIFactory.CreatePrefab(parent, name);
            //GameObject.Instantiate(gameObject);
            //gameObject = UnityEngine.Resources.Load(name) as GameObject;
            //UnityEngine.GameObject.Instantiate(gameObject);
        }
    }
}
