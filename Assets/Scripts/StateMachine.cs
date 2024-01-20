using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts{
    
    public class StateMachine 
    {

        private PlayerController player;
        private string currentState;
        // Start is called before the first frame update

        private StateMachine(PlayerController player)
        {
            this.player = player;
        }
        private void TransitionToState(string nextState)
        {
            if(nextState == currentState) { return; }

        }
    }
    
}
