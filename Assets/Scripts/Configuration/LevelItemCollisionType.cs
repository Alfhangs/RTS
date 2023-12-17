using System;

namespace Configuration
{
    [Serializable]
    public enum LevelItemCollisionType 
    {
        None,
        Rigidbody,
        NavMesh
    }
}