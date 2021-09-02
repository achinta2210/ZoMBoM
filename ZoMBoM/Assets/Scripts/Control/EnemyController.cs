
using UnityEngine;
using UnityEngine.AI;
using ZOMBOM.Data;
using System.Collections;
namespace ZOMBOM.Control
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] PlayerController player;
        public Transform rangeDisplay;
        public SkinnedMeshRenderer mesh;
        public Transform explosionPoint;
        public D_EnemyData data;
        NavMeshAgent agent;
        Transform target;
        public Animator anim;
        float explosionRadious = 2.0f;
        float explosionDeley = 0.7f;

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            //SetTarget(player.transform);
            explosionRadious = Random.Range(data.minExplosionRad , data.maxExplosionRad);
            explosionDeley = Random.Range(data.minExplosionDeley, data.maxExplosionRad);
            if (rangeDisplay != null)
            {
                rangeDisplay.localScale = new Vector3
               (explosionRadious * 2 , rangeDisplay.localScale.y , explosionRadious * 2);
            }

        }

        private void Update()
        {

            MoveToTarget();
            

        }
        private void FixedUpdate()
        {
            if (CheckIfInRange())
            {
                StartCoroutine(HandleExplosion());
            }
        }
        IEnumerator HandleExplosion()
        {
            CancelTarget();
            
            mesh.sharedMaterial = data.explosionMat;
            yield return new WaitForSeconds(explosionDeley);
            Explode();
        }
        void MoveToTarget()
        {
            if (target == null) return;
            agent.SetDestination(target.position);

        }
        
        public void SetTarget(Transform target)
        {
            this.target = target;
            anim.SetBool("Idle", false);
            anim.SetBool("Walk", true);
        }

        public void CancelTarget()
        {
            this.target = null;
            agent.Stop();
            anim.SetBool("Idle", true);
            anim.SetBool("Walk", false);
        }

        void Explode()
        {
            if (Vector3.Distance(player.transform.position , transform.position) <= explosionRadious )
            {
                Vector3 dir = (player.transform.position - explosionPoint.position).normalized;
                player.mover.AddExplosionForce(dir, data.explosionForce, data.playerStunTime);
            }
            
            Instantiate(data.explosionPartice.gameObject , transform.position , Quaternion.identity);
            Destroy(gameObject);
        }

        


        bool CheckIfInRange()
        {
            return Physics.CheckSphere(transform.position , explosionRadious , data.playerMask);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position , explosionRadious);
        }

        

    }
}
