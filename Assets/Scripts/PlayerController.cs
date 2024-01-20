using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts
{

    public class PlayerController : CharacterBase
    {
        [SerializeField] CapsuleCollider2D capsuleCollider;
        [SerializeField] Vector2 input;
        [SerializeField] bool isDie = false;
        [SerializeField] float jumpSpeed;
        [SerializeField] bool isHorizontal = false;
        [SerializeField] float yVelocity;
        [SerializeField] bool IsTouchGround;
        State currentState;
        private void Awake()
        {
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            animator.SetFloat("yVelocity", rb.velocity.y);
            //if (capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
            //{
            //    if (rb.velocity == Vector2.zero)
            //    {
            //        animator.SetBool("Idle", true);
            //    }
            //    else
            //    {
            //        animator.SetBool("Idle", false);
            //        Walk();
            //        FlipFace();
            //    }

            //}
            if (capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) && rb.velocity == Vector2.zero)
            {
                animator.SetBool("Idle", _isIdle);
            }
            else
            {
                _isIdle = false;
               // animator.SetBool("Idle", false);
            }
            Walk();
            FlipFace();

        }

        private void OnMove(InputValue value)
        {
            if (isDie) { return; }
            input = value.Get<Vector2>();
        }


        private void Walk()
        {
            isHorizontal = input.x != 0;
            Vector2 myVelocity = new Vector2(input.x * walkSpeed, rb.velocity.y);
            rb.velocity = myVelocity;
            animator.SetBool("Walk", isHorizontal);
        }

        private void OnJump(InputValue value)
        {
            if (isDie) { return; }
            if (value.isPressed && capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                rb.velocity = new Vector2(0, jumpSpeed);
                animator.SetBool("Jump", true);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                IsTouchGround = true;
                animator.SetBool("Jump", false);
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                IsTouchGround = false;
            }
        }


        private void FlipFace()
        {
            Vector3 velo = transform.localScale;
            if (isHorizontal)
            {
                float x = Mathf.Sign(input.x);
                transform.localScale = new Vector3(x, velo.y, velo.z);
            }
        }
    }

}
