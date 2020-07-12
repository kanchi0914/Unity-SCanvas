using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace Assets.Scripts.Extensions
{

    public static class TransformExtensions
    {
        #region PosSetters
        public static void SetPos(this Transform transform, float x, float y, float z)
        {
            transform.position = new Vector3(x, y, z);
        }

        public static void AddPos(this Transform transform, float x, float y, float z)
        {
            transform.position = new Vector3(transform.position.x + x,
                transform.position.y + y, transform.position.z + z);
        }

        public static void SetPosX(this Transform transform, float x)
        {
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }

        public static void SetPosY(this Transform transform, float y)
        {
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }

        public static void SetPosZ(this Transform transform, float z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, z);
        }

        public static void AddPosX(this Transform transform, float x)
        {
            transform.position = new Vector3(transform.position.x + x, transform.position.y, transform.position.z);
        }

        public static void AddPosY(this Transform transform, float y)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + y, transform.position.z);
        }

        public static void AddPosZ(this Transform transform, float z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + z);
        }
        #endregion

        #region LocalPosSetters
        public static void AddLocalPosX(this Transform transform, float x)
        {
            transform.localPosition = new Vector3(transform.localPosition.x + x,
				transform.localPosition.y, transform.localPosition.z);
        }

        public static void AddLocalPosY(this Transform transform, float y)
        {
            transform.localPosition = new Vector3(transform.localPosition.x,
				transform.localPosition.y + y, transform.localPosition.z);
        }
        #endregion
    }
}
