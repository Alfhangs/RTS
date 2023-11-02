using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSpawnMessage : IMessage
{
    public Vector3 Position;
    public Quaternion Rotation;
    public float Damage;
}
