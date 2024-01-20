using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Scripts
{

    public class CharacterBase : MonoBehaviour
    {
        [SerializeField] protected Animator animator;
        [SerializeField] protected Rigidbody2D rb;
        [SerializeField] protected float walkSpeed;

        protected const string IDLE_ANIMATION = "IsIdle";
        protected const string DEAD_ANIMATION = "isDead";
        protected const string JUMP_ANIMATION = "isJump";
        protected const string WALK_ANIMATION = "isWalk";


        protected bool isIdle = true;
        protected bool isDead = false;
        protected bool isJump = false;
        protected bool isWalk = false;

        protected void SetAnimationParameters()
        {
            animator.SetBool(IDLE_ANIMATION, isIdle);
            animator.SetBool(DEAD_ANIMATION, isDead);
            animator.SetBool (JUMP_ANIMATION, isJump);
            animator.SetBool(WALK_ANIMATION, isWalk);
        }
    }
    
}
