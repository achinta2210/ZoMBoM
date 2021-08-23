using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZOMBOM.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class Mover : MonoBehaviour
    {
        public Rigidbody rb { get; private set; }
        public MoverConstrains constrains;
        

        Vector3 movementVector;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            movementVector = new Vector3();
            constrains = new MoverConstrains();
        }

        public void MoveHorizontal(Vector2 movement)
        {
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
            body.localRotation = Quaternion.LookRotation(targetEularAngle);
        }
        private void Update()
        {
            constrains.isMoving = rb.velocity.magnitude != 0.0f;
        }

        public struct MoverConstrains
        {
            public bool isMoving;
        }

    }

}