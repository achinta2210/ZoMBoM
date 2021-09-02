
using UnityEngine;

namespace ZOMBOM.Combat
{
    public class Projectile : MonoBehaviour
    {
        public Health player;
        public Rigidbody rb;
        public float projectileHeight = 4f;
        private void Start()
        {
            Vector3 dir = ( player.transform.position - transform.position);
            rb.velocity = new Vector3
                (dir.x, 15f - transform.position.y  , dir.z );
        }
        
        private void OnTriggerEnter(Collider other)
        {
            Destroy(gameObject);
        }

    }

}