using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZOMBOM.Data
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Data/New Player Data", order = 1)]
    public class D_PlayerData : ScriptableObject
    {
        [Header("Checks")]
        public float groundCheckRadious = 0.3f;
        public LayerMask groundMask;
        [Space]
        [Header("Properties")]
        public float movementSpeed = 10.0f;
        public float jumpHeight = 2f;
        public float fullJumpTime = 1.0f;
        public int jumpCount = 2;
        public float minTimeForFullJump = 1f;
        public float counterJumpForce = 4f;
    }

}