namespace Player.State
{
    public class PlayerStateMachine
    {
        public PlayerStateMachine()
        {
        }
        public PlayerBaseState CurrentState { get; private set; }
        public PlayerBaseState PreState { get; private set; }
        public void Update()
        {

            CurrentState?.Update();
        }
        public void ChangeState(PlayerBaseState state)
        {
        
            if(CurrentState == state) return;
        
            var curSate = CurrentState;
            var nextState = state;
        
            PreState = curSate;
            CurrentState = state;
            curSate?.Exit();
            nextState?.Enter();
        }

        public void SetState(PlayerBaseState state)
        {
            //var stateNode = GetOrAddNode(state);
            CurrentState = state;
            CurrentState?.Enter();
        }

        public void FixedUpdate()
        {
            CurrentState?.FixedUpdate();
        }
    }
}