using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RTS.Store
{
    [Serializable]
    public class UnitStoreItem : StoreItem
    {
        public UnitType Unit;
        public bool IsUpgrade;
    }
}
