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
        BoxCollider2D foot;
        private void Awake()
        {
        }

        // Start is called before the first frame update
        void Start()
        {
            foot = GetComponent<BoxCollider2D>();
        }

        // Update is called once per frame
        void Update()
        {
            ConditionState();
            Walk();
            FlipFace();
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right,1f,LayerMask.GetMask("Ground"));
            Debug.DrawRay(transform.position, hit.point, Color.yellow);
            if(hit.collider != null)
            {
                Debug.Log(hit.collider.name);

            }
        }

        private void ConditionState()
        {
            animator.SetFloat("yVelocity", rb.velocity.y);
            isWalk = isHorizontal;
            isJump = !IsTouchGround;
            if (isJump && isWalk)
            {
                isWalk = false;
                isJump = true;
            }
            if (capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) && rb.velocity == Vector2.zero)
            {
                isIdle = true;
                SetAnimationParameters();
            }
            else
            {
                isIdle = false;
            }
            
        }

        private void OnMove(InputValue value)
        {
            if (isDie) { return; }
            input = value.Get<Vector2>();
        }


        private void Walk()
        {
            isHorizontal = input.x != 0;
            Vector2 myVelocity;
            if (foot.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                myVelocity = new Vector2(input.x * walkSpeed, rb.velocity.y);
            }
            else
            {
                myVelocity = rb.velocity;
            }
            rb.velocity = myVelocity;
            SetAnimationParameters();
        }

        private void OnJump(InputValue value)
        {
            if (isDie) { return; }
            if (value.isPressed && foot.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {           
                rb.velocity = new Vector2(0, jumpSpeed);
                SetAnimationParameters() ;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                IsTouchGround = true;
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
