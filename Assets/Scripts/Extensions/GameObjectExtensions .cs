using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace Assets.Scripts.Extensions
{

    public static class GameObjectExtensions
    {
        public static T TryAddComponent<T>(this GameObject gameObject) where T : Component
        {
            var attached = gameObject.GetComponent<T>();
            if (!attached) attached = gameObject.AddComponent<T>();
            return attached;
        }
    }
}