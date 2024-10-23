using System.Collections.Generic;
using UnityEngine;

namespace GUtils
{
    public static class GameObjectExtensions
    {
        public static IEnumerable<T> GetDirectOrderedChildComponents<T>(this GameObject go) where T : MonoBehaviour
        {
            var tempStacks = new List<T>();
            for (int i = 0; i < go.transform.childCount; ++i)
            {
                var child = go.transform.GetChild(i);
                if (child.GetComponent<T>())
                {
                    tempStacks.Add(child.GetComponent<T>());
                }
            }

            return tempStacks;
        }

        public static IEnumerable<T> GetDirectOrderedChildComponents<T>(this Transform transform) where T : MonoBehaviour
        {
            return transform.gameObject.GetDirectOrderedChildComponents<T>();
        }
    }
}