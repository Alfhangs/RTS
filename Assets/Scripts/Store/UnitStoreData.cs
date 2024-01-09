using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS.Store
{
    [CreateAssetMenu(menuName = "RTS/New Unit Store")]
    public class UnitStoreData : ScriptableObject
    {
        public List<UnitStoreItem> Items = new List<UnitStoreItem>();
    }
}
