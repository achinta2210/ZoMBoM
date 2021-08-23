
using UnityEngine;

namespace ZOMBOM.InputManager
{
    public class InputHandler : MonoBehaviour
    {
        public Vector2 AxisInput { get; private set; }
        public bool JumpDown { get; private set; }
        public bool JumpUp { get; private set; }

        private void Update()
        {
            AxisInput = new Vector2(Input.GetAxisRaw("Horizontal") , Input.GetAxisRaw("Vertical")).normalized;
            JumpDown = Input.GetButtonDown("Jump");
            JumpUp = Input.GetButtonUp("Jump");
        }
    }
}
