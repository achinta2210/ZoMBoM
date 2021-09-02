
using UnityEngine;

namespace ZOMBOM.InputManager
{
    public class InputHandler : MonoBehaviour
    {
        public float accelerationTime;
        public float decelerationTime;

        public Vector2 AxisInput { get; private set; }
        
        public bool JumpDown { get; private set; }
        public bool JumpUp { get; private set; }
        public bool Dash { get; private set; }
        //float xAcceleration, yAcceleration;
        //Vector2 rawAxisInput;


        private void Update()
        {
            AxisInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
            JumpDown = Input.GetButtonDown("Jump");
            JumpUp = Input.GetButtonUp("Jump");
            Dash = Input.GetButtonDown("Dash");
        }
    }
}
