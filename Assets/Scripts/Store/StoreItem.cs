using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS.Store
{
    [Serializable]
    public class StoreItem
    {
        public string Description;
        public int PriceGold;
        public int PriceResource;
        public ResourceType CurrencyResource;
        public Sprite Image;
        public GameObject Prefab;
    }
}