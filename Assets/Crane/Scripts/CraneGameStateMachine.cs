using Unity.VisualScripting;
using UnityEngine;

namespace Crane
{
    public class CraneGameStateMachine : MonoBehaviour
    {
        public enum State
        {
            Idle,
            HorizontalMoving,
            VerticalMoving,
            Dropping,
            Returning,
        }

        private State _state;
            
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void ChangeState( State state)
        {
            _state = state;
        }

        public State GetState()
        {
            return _state;
        }
    }
}
