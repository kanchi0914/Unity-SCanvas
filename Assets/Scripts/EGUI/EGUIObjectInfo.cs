using System;
using System.Linq;
using Assets.Scripts.Extensions;
using EGUI.GameObjects;
using UniRx;
using UnityEngine;

namespace Assets.Scripts.EGUI
{
    public class EGUIObjectInfo : MonoBehaviour
    {
        public EGGameObject EgGameObject { get; private set; }

        public void Init(EGGameObject egGameObject)
        {
            EgGameObject = egGameObject;
        }
    }
}