using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS.Store
{
    [CreateAssetMenu(menuName = "RTS/New Resource Store")]
    public class ResourceStoreData : ScriptableObject
    {
        public List<ResourceStoreItem> items = new List<ResourceStoreItem>();
    }
}