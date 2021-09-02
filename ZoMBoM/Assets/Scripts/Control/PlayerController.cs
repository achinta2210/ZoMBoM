
using UnityEngine;
using ZOMBOM.Movement;
using ZOMBOM.InputManager;
using ZOMBOM.Data;
using ZOMBOM.Combat;

namespace ZOMBOM.Control
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] Transform groundCheck = null;
        [SerializeField] D_PlayerData data = null;
        [SerializeField] Transform body = null;
        int jumpsLeft;
        InputHandler input;
        PlayerCombat combat;
        public Mover mover { get; private set; }


        float timeSinceJumpStarted;
        Vector3 lastDir;
        bool canAttck;
        private void Start()
        {
            mover = GetComponent<Mover>();
            input = GetComponent<InputHandler>();
            combat = GetComponent<PlayerCombat>();
            jumpsLeft = data.jumpCount;
            timeSinceJumpStarted = data.minTimeForFullJump;
        }
        private void Update()
        {
            mover.MoveHorizontal(input.AxisInput * data.movementSpeed);
            if (input.AxisInput.magnitude != 0)
            {
                lastDir = new Vector3(input.AxisInput.x, 0.0f, input.AxisInput.y);
            }
            mover.RotateBody(body, lastDir);
            HandleJumop();
            HandleDash();
        }
        private void FixedUpdate()
        {
            AggrovateEnemy();
        }

        private void HandleJumop()
        {
            if (input.JumpDown && CanJump())
            {
                mover.Jump(data.jumpHeight);
                jumpsLeft -= 1;
                timeSinceJumpStarted = data.minTimeForFullJump;

            }
            if (mover.CheckForGround() && mover.rb.velocity.y < -1f)
            {
                jumpsLeft = data.jumpCount;

            }
            
            if (input.JumpUp && !mover.CheckForGround() && timeSinceJumpStarted > 0.0f)
            {
                mover.ApplyDownForce(data.counterJumpForce);
            }
            timeSinceJumpStarted = Mathf.Min(timeSinceJumpStarted - Time.deltaTime);
        }

        

        public void HandleDash()
        {
            if (input.Dash)
            {
                mover.Dash(data.dashForce, body.forward, data.dashTime , data.timeBetweenDash);
            }   
        }
        
        public void AggrovateEnemy()
        {
            Collider[] colliders = 
            Physics.OverlapSphere(transform.position , data.enemyAgroRadious , data.enemyMask);
            foreach(Collider collider in colliders)
            {
                collider.GetComponent<EnemyController>().SetTarget(this.transform);
            }
        }
       
        bool CanJump()
        {
            return jumpsLeft > 0;
        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position , data.enemyAgroRadious);
        }


    }

}