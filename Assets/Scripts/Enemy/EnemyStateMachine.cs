using System.Collections.Generic;
public class EnemyStateMachine 
{
   public void Initalize(EnemyBaseState startBaseState)
   {
       CurrentState = startBaseState;
   }
   public EnemyStateMachine()
   {
   }
   public EnemyBaseState CurrentState { get; private set; }
   public EnemyBaseState PreState { get; private set; }
   public void Update()
   {

       CurrentState?.Update();
   }
   public void ChangeState(EnemyBaseState state)
   {
        
       if(CurrentState == state) return;
        
       var curSate = CurrentState;
       var nextState = state;
        
       PreState = curSate;
       CurrentState = state;
       curSate?.Exit();
       nextState?.Enter();
   }

   public void SetState(EnemyBaseState state)
   {
       CurrentState = state;
       CurrentState?.Enter();
   }

   public void FixedUpdate()
   {
       CurrentState?.FixedUpdate();
   }
}