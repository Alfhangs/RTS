using UnityEngine;
using Configuration;

[CreateAssetMenu(menuName = "RTS/New Unit")]
public class UnitData : BaseCharacterData
{
    public UnitType type;
    public int level;
    public float levelMultiplier;
    public ActionType actions;
}

