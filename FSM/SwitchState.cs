using System;
using System.Collections.Generic;

namespace FSM
{
	public class StateTest
	{
		public virtual bool Test()
		{
			return false;
		}
	}

	class StateTestFunc : StateTest
	{
		System.Func<bool> t;
		public StateTestFunc(System.Func<bool> t) { this.t = t; }
		public override bool Test()
		{
			return t();
		}
	}

	public class SwitchState<STATE_ID, TEST_ID> : State<STATE_ID>
	{
		List<KeyValuePair<StateTest, int>> exits;

		private static readonly int defaultExitId = -1;

		public SwitchState(STATE_ID id)
			: base(id)
		{
			exits = new List<KeyValuePair<StateTest, int>>();
		}

		public StateExit<STATE_ID> AddTestExit(StateTest test)
		{
			int exit = exits.Count;
			exits.Add(new KeyValuePair<StateTest, int>(test, exit));
			return new StateExit<STATE_ID>(this, exit);
		}
		public StateExit<STATE_ID> AddTestExit(System.Func<bool> test)
		{
			StateTestFunc stateTest = new StateTestFunc(test);
			return AddTestExit(stateTest);
		}

		public StateExit<STATE_ID> DefaultExit()
		{
			return new StateExit<STATE_ID>(this, defaultExitId);
		}

		public override void Enter()
		{
			for(int i = 0; i < exits.Count; ++i)
			{
				StateTest test = exits[i].Key;
				bool testResult = test.Test();
				if (testResult)
				{
					DoExitState(exits[i].Value);
					return;
				}
			}

			DoExitState(defaultExitId);
		}
	}
}
