using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace EGUI
{
    public class ClickEventGetter : MonoBehaviour
    {
        void Update()
        {
            // if (Input.GetMouseButtonDown(0))
            // {
            //     var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //     Debug.Log(ray);
            //
            //     var hit2d = Physics2D.Raycast((Vector2) ray.origin, (Vector2) ray.direction);
            //     Debug.Log(hit2d.transform);
            //     if (hit2d)
            //     {
            //         var clickedGameObject = hit2d.transform.gameObject.name;
            //         Debug.Log(clickedGameObject);
            //     }
            // }
        }
    }
}