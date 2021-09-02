using System.Collections;
using UnityEngine;

namespace ZOMBOM.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class Mover : MonoBehaviour
    {
        [SerializeField] Transform groundCheck = null;
        [SerializeField] float groundCheckRadious = 0.2f;

        [SerializeField] LayerMask groundMask;
        public Rigidbody rb { get; private set; }
        public MoverConstrains constrains;
        Animator anim;

        Vector3 movementVector;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            anim = GetComponent<Animator>();
            movementVector = new Vector3();
            constrains = new MoverConstrains();
            constrains.canMove = true;
            constrains.canDash = true;
        }

        public void MoveHorizontal(Vector2 movement)
        {
            if (constrains.isDashing || !constrains.canMove) return;
            if (movement.magnitude == 0.0f)
            {
                movementVector = new Vector3(0.0f , rb.velocity.y , 0.0f);
            }
            movementVector.Set(movement.x , rb.velocity.y, movement.y);
            rb.velocity = movementVector;
            
        }
        public void Jump(float height)
        {
            float jumpForce = Mathf.Sqrt(Mathf.Abs(Physics.gravity.y * -2f * height ));
            movementVector.y = jumpForce;
            rb.velocity = movementVector;
        }
        public void ApplyDownForce(float force)
        {
            movementVector.y = -force;
            rb.velocity = movementVector;
        }
        public void RotateBody(Transform body , Vector3 targetEularAngle )
        {
            body.forward = targetEularAngle.normalized;
        }
        private void Update()
        {
            
            HandleAnimations();
        }
        void HandleAnimations()
        {
            if (Mathf.Abs(new Vector2(movementVector.x , movementVector.z).magnitude) > 0.0f &&
                CheckForGround() && !constrains.isDashing && constrains.canMove)
            {
                anim.SetBool("Walk" , true);
                anim.SetBool("Idle", false);
                anim.SetBool("Jump", false);
                
            }else if (!CheckForGround() && Mathf.Abs(movementVector.y) > 0)
            {
                anim.SetBool("Walk", false);
                anim.SetBool("Idle", false);
                anim.SetBool("Jump", true);
            }else
            {
                anim.SetBool("Walk", false);
                anim.SetBool("Idle", true);
                anim.SetBool("Jump", false);
            }
            
        }
        public void Dash(float force , Vector3 dir , float time , float timeBetweenDash)
        {
            if (constrains.canDash)
            {
                StartCoroutine(MakeDash(force, dir, time, timeBetweenDash));

            }
        }
        IEnumerator MakeDash(float force, Vector3 dir,float dashTime , float timeBetweenDash)
        {
            if (!constrains.isDashing   && constrains.canMove)
            {
                movementVector.Set(dir.x * force, rb.velocity.y, dir.z * force);
                rb.velocity = movementVector;
                constrains.isDashing = true;
                constrains.canDash = false;
            }
            yield return new WaitForSeconds(dashTime);
            constrains.isDashing = false;
            movementVector.Set(0.0f,rb.velocity.y , 0.0f);
            rb.velocity = movementVector;
            yield return new WaitForSeconds(timeBetweenDash);
            constrains.canDash = true;
        }
        public bool CheckForGround()
        {
            if (!groundCheck) return false;
            return Physics.CheckSphere(groundCheck.position, groundCheckRadious, groundMask);
        }
         public void AddExplosionForce(Vector3 dir , float force , float stunTime)
         {
            StartCoroutine(AddExpforce(dir , force , stunTime));
         }
        
        IEnumerator AddExpforce(Vector3 dir, float force, float stunTime)
        {
            rb.velocity = dir * force;
            constrains.canMove = false;
            yield return new WaitForSeconds(stunTime);
            constrains.canMove = true;
        }
        public struct MoverConstrains
        {
            public bool isMoving;
            public bool isDashing;
            public bool canMove;
            public bool canDash;
        }
        private void OnDrawGizmos()
        {
            if (groundCheck)
            {
                Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadious);
            }
            
        }

         

    }

}