using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extension
{
    public static class GameObjectExtension
    {
        public static void SetLayerMaskToAllChildren( GameObject item, string layerName)
        {
            int layer = LayerMask.GetMask(layerName);
            item.layer = layer;
            foreach (Transform child in
            item.GetComponentsInChildren<Transform>())
            {
                child.gameObject.layer = layer;
            }
        }
    }
}