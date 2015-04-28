using System;
using System.Collections.Generic;

namespace FSM
{
	public class FSM<STATE_ID>: ID<String>
	{
		public event System.Action<FSM<STATE_ID>, STATE_ID> OnFinish;
		public event System.Action<FSM<STATE_ID>, STATE_ID> OnStart;
		public event System.Action<FSM<STATE_ID>, STATE_ID> OnStateChanged;

		protected Dictionary<STATE_ID, State<STATE_ID>> states;
		protected Dictionary<STATE_ID, Dictionary<int, STATE_ID>> transitions;
		public STATE_ID StartStateID
		{
			get;
			private set;
		}

		public STATE_ID FinishStateID
		{
			get;
			private set;
		}

		public FSM(String id)
			:base(id)
		{
			states = new Dictionary<STATE_ID, State<STATE_ID>>();
			transitions = new Dictionary<STATE_ID, Dictionary<int, STATE_ID>>();
		}

		public State<STATE_ID> curState
		{
			get;
			protected set;
		}

		public void DoAction(Object action)
		{
			if (curState != null)
				curState.DoAction(action);
		}

		public Cursor<STATE_ID> GetCursor()
		{
			return new Cursor<STATE_ID>(this);
		}

		public void AddState(State<STATE_ID> state)
		{
			if (!states.ContainsKey(state.id))
			{
				states[state.id] = state;
				state.fsm = this;

				if (state.isStart)
					StartStateID = state.id;
				if (state.isFinish)
					FinishStateID = state.id;
			}
		}

		public void AddState(State<STATE_ID> state, Dictionary<int, STATE_ID> state_transitions)
		{
			AddState(state);
			transitions[state.id] = state_transitions;
		}

		public void AddStateExit(State<STATE_ID> state, int exitId, STATE_ID nextState)
		{
			AddState(state);
			if (!transitions.ContainsKey(state.id))
				transitions[state.id] = new Dictionary<int, STATE_ID>();
			transitions[state.id][exitId] = nextState;
		}

		public void ExitState(State<STATE_ID> state, int exit = 0)
		{
			if (transitions.ContainsKey(state.id))
			{
				Dictionary<int, STATE_ID> exits = transitions[state.id];
				if (exits.ContainsKey(exit))
				{
					STATE_ID next_state_id = exits[exit];
					if (states.ContainsKey(next_state_id))
					{
						State<STATE_ID> next_state = states[next_state_id];
						SetState(next_state);
					}
				}
			}
		}

		protected void SetState(State<STATE_ID> next_state)
		{
			if (curState != null)
				curState.Exit();
			curState = next_state;
			System.Console.WriteLine(this.ToString() + " Set current state: " + curState);
			curState.Enter();
			if (OnStateChanged != null)
				OnStateChanged(this, curState.id);
			if (curState.isFinish)
				Finish();
		}

		private void Finish()
		{
			if (OnFinish!= null)
				OnFinish(this, curState.id);
		}

		public override string ToString()
		{
			return String.Format("[FSM id = {0}]", id);
		}

		public virtual void Start(State<STATE_ID> state)
		{
			SetState(state);
			if (OnStart != null)
				OnStart(this, state.id);
		}

		public virtual void Start()
		{
			Start(states[StartStateID]);
		}

		~FSM()
		{
			OnFinish = null;
			OnStart = null;
			OnStateChanged = null;
		}
	}
}
