using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZOMBOM.Effects
{
    public class Effect : MonoBehaviour
    {
        public float autoDestroyTime = 2.0f;
        private void Start()
        {
            Destroy(gameObject, autoDestroyTime);
        }
    }

}