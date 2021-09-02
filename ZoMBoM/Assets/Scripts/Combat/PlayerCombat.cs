
using UnityEngine;

namespace ZOMBOM.Combat
{
    public class PlayerCombat : MonoBehaviour
    {
        public float attackRadious;
        public LayerMask enemyMask;
        public Transform attackPoint;
        public float damage = 50f;
        public void Attack()
        {
            print("Attack");
            Collider[] colliders = Physics.OverlapSphere(attackPoint.position , attackRadious , enemyMask);
            foreach (var collider in colliders)
            {
                Health health = collider.GetComponent<Health>();
                if (health)
                {
                    health.GetDamage(damage);
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position , attackRadious);
        }
    }

}