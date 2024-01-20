using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts{
    
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] Vector2 input;
        [SerializeField] bool isDie = false;
        private Rigidbody2D rb;
        [SerializeField] float walkSpeed;
        [SerializeField] float jumpSpeed;
        [SerializeField] bool isHorizontal = false;
        Animator animator;

        [SerializeField] string currentState;
        //public string Walk = "Walk";

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();   
            animator = GetComponent<Animator>();
        }
    
        // Update is called once per frame
        void Update()
        {
            Walk();
            FlipFace();
           
        }

        private void TransitionToState(string nextState)
        {
            if (nextState == currentState) { return; }
            animator.SetBool(nextState, true);
        }

        private void OnMove(InputValue value)
        {
            if(isDie) { return; }
            input = value.Get<Vector2>();
        }


        private void Walk()
        {
            isHorizontal = input.x != 0;
            Vector2 myVelocity = new Vector2(input.x * walkSpeed, rb.velocity.y);
            rb.velocity = myVelocity;
        }

        private void OnJump(InputValue value)
        {
            if(isDie) { return; }   
            if(value.isPressed)
            {
                rb.velocity = new Vector2(0,jumpSpeed);
            }
        }

        private void FlipFace()
        {
            Vector3 velo = transform.localScale;
            if(isHorizontal)
            {
                float x = Mathf.Sign(input.x);
                transform.localScale = new Vector3(x , velo.y , velo.z);
            }
        }
    }
    
}
