using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS.Store
{
    [Serializable]
    public class ResourceStoreItem : StoreItem
    {
        public ResourceType Resource;
    }
}