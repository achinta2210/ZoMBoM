
using UnityEngine;
using ZOMBOM.Effects;

namespace ZOMBOM.Data
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Data/New Enemy Data", order = 3)]
    public class D_EnemyData : ScriptableObject
    {
        public Effect explosionPartice;
        public float minExplosionRad = 1.5f;
        public float maxExplosionRad = 3.5f;
        public LayerMask playerMask;
        
        public float explosionForce = 10f;
        public float playerStunTime = 1.1f;
        public float minExplosionDeley = 0.1f;
        public float maxExplosionDeley = 0.9f;
        public Material explosionMat;
    }

}