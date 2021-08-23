
using UnityEngine;
using ZOMBOM.Movement;
using ZOMBOM.InputManager;
using ZOMBOM.Data;

namespace ZOMBOM.Control
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] Transform groundCheck = null;
        [SerializeField] D_PlayerData data = null;
        [SerializeField] Transform body = null;
        int jumpsLeft;
        Mover mover;
        InputHandler input;
        float timeSinceJumpStarted;
        Vector3 lastDir;
        private void Start()
        {
            mover = GetComponent<Mover>();
            input = GetComponent<InputHandler>();
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

        }

        private void HandleJumop()
        {
            if (input.JumpDown && CanJump())
            {
                mover.Jump(data.jumpHeight);
                jumpsLeft -= 1;
                timeSinceJumpStarted = data.minTimeForFullJump;

            }
            if (CheckForGround() && mover.rb.velocity.y <= 0.0f)
            {
                jumpsLeft = data.jumpCount;
            }
            if (input.JumpUp && !CheckForGround() && timeSinceJumpStarted > 0.0f)
            {
                mover.ApplyDownForce(data.counterJumpForce);
            }
            timeSinceJumpStarted = Mathf.Min(timeSinceJumpStarted - Time.deltaTime);
        }

        bool CheckForGround()
        {
            return Physics.CheckSphere(groundCheck.position , data.groundCheckRadious , data.groundMask);
        }
        bool CanJump()
        {
            return jumpsLeft > 0;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(groundCheck.position , data.groundCheckRadious);
        }
    }

}