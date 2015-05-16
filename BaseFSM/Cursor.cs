using System;
using System.Collections.Generic;
using System.Text;

namespace BaseFSM
{
	public class Cursor<STATE_ID>
	{
		public event System.Action<Cursor<STATE_ID>> onChange;

		Stack<State<STATE_ID>> curStates;
		FSM<STATE_ID> fsm; //todo убрать! есть в стеке

		public Cursor(FSM<STATE_ID> fsm)
		{
			this.fsm = fsm;
			SetCursor();
		}

		~Cursor()
		{
			while(curStates.Count > 0)
			{
				State<STATE_ID> state = curStates.Pop();
				if (state != null)
					state.fsm.OnStateChanged -= OnStateChanged;
			}
		}

		public void DoAction(Object action)
		{
			if (curState != null)
				curState.fsm.DoAction(action);
		}

		public State<STATE_ID> curState
		{
			get
			{
				return curStates.Peek();
			}
		}

		protected void OnStateChanged(FSM<STATE_ID> fsm, STATE_ID stateID)
		{
			SetCursor();
			if (onChange != null)
				onChange(this);
		}

		protected void OnFinish(FSM<STATE_ID> fsm, STATE_ID stateID)
		{
			fsm.OnStateChanged -= OnStateChanged;
			fsm.OnFinish -= OnFinish;

			while (curStates.Count > 0)
			{
				State<STATE_ID> state = curStates.Pop();
				if (state != null)
				{
					state.fsm.OnStateChanged -= OnStateChanged;
					if (state.fsm == fsm)
						break;
				}
			}
		}

		protected void SetCursor()
		{
			curStates = new Stack<State<STATE_ID>>();
			SetCursor(fsm, curStates);
		}

		protected void SetCursor(FSM<STATE_ID> fsm, Stack<State<STATE_ID>> curStates)
		{
			fsm.OnStateChanged += OnStateChanged;
			fsm.OnFinish += OnFinish;

			State<STATE_ID> curState = fsm.curState;
			curStates.Push(curState);

			IStateWithFSM<STATE_ID> fsmState = curState as IStateWithFSM<STATE_ID>;
			if (fsmState != null)
			{
				FSM<STATE_ID> subFSM = fsmState.GetSubFSM();
				SetCursor(subFSM, curStates);
			}
		}

		public override string ToString()
		{
			StringBuilder s = new StringBuilder();
			s.Append("\n-> \n");
			if (curStates.Count > 0)
			{
				foreach (State<STATE_ID> state in curStates)
				{
					if (state == null)
					{
						s.Append("<null>\n");
					}
					else
					{
						if (state.fsm == null)
							s.Append("<null FSM>");
						else
							s.Append(state.fsm.ToString());
						s.Append(" : ");
						s.Append(state.ToString());
						s.Append(";\n");
					}
				}
			}
			else
			{
				s.Append("[null]\n");
			}
			return s.ToString();
		}
	}
}
