using System;
using System.Collections.Generic;

namespace FSM
{
	public struct StateExit<STATE_ID>
	{
		int exitId;
		State<STATE_ID> state;

		public StateExit(State<STATE_ID> state, int exitId)
		{
			this.state = state;
			this.exitId = exitId;
		}

		public void AddExitTransition(FSM<STATE_ID> fsm, STATE_ID nextState)
		{
			fsm.AddStateExit(state, exitId, nextState);
		}
	}

	public class State<STATE_ID> : ID<STATE_ID>
	{
		public bool isStart = false;
		public bool isFinish = false;

		public event System.Action onExit;

		public FSM<STATE_ID> fsm
		{
			get;
			set;
		}

		public State(STATE_ID id)
			: base(id)
		{
		}

		public virtual void Enter()
		{
		}

		public void DoExitState(int exit)
		{
			fsm.ExitState(this, exit);
		}
		
		public virtual void Exit()
		{
			if (onExit != null)
				onExit();
		}

		public virtual void DoAction(Object actionObject)
		{
		}

		public override string ToString()
		{
			return String.Format("[State {1} id = {2}]", fsm, GetType(), id);
		}
	}
}
