using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public enum State 
    {
        Idle,
        Dead,
        Jump,
        Walk
    }

    public class CharacterBase : MonoBehaviour
    {
        [SerializeField] protected Animator animator;
        [SerializeField] protected Rigidbody2D rb;
        [SerializeField] protected float walkSpeed;
        protected bool _isIdle = true;
        protected bool _isDead = false;
        protected bool _isJump = false;
        protected bool _isWalk = false;

        
    }
    
}
