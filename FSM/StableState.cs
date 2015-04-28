using System;
using System.Collections.Generic;

namespace FSM
{
	public class TestAndEvent
	{
		public virtual bool Test(Object action)
		{
			return true;
		}

		public virtual void Event(Object action)
		{
		}
	}

	class TestAndEventAction : FSM.TestAndEvent
	{
		public System.Func<Object, bool> test;
		public System.Action<Object> _event;

		public TestAndEventAction(System.Func<Object, bool> test, System.Action<Object> _event)
		{
			this.test = test;
			this._event = _event;
		}

		public override bool Test(Object action)
		{
			return test(action);
		}

		public override void Event(Object action)
		{
			this._event(action);
		}
	}

	public class StableState<STATE_ID> : State<STATE_ID>
	{
		Dictionary<Object, int> exits;
		Dictionary<Object, TestAndEvent> actions;

		public StableState(STATE_ID id)
			: base(id)
		{
			exits = new Dictionary<Object, int>();
			actions = new Dictionary<Object, TestAndEvent>();
		}

		public StateExit<STATE_ID> AddAction(Object action, TestAndEvent testEndEvent)
		{
			int exit = exits.Count;
			exits[action] = exit;
			actions[action] = testEndEvent; 
			return new StateExit<STATE_ID>(this, exit);
		}

		public StateExit<STATE_ID> AddAction(Object action, System.Func<Object, bool> test, System.Action<Object> _event)
		{
			TestAndEventAction testEndEvent = new TestAndEventAction(test, _event);
			return AddAction(action, testEndEvent);
		}

		public override void DoAction(Object action)
		{
			if (action == null)
				throw new Exception(); //todo

			if (!exits.ContainsKey(action))
				throw new Exception(); //todo

			if (!actions.ContainsKey(action) || actions[action] == null)
				throw new Exception(); //todo

			bool testResult = actions[action].Test(action);
			if (!testResult)
				throw new Exception(); //todo

			actions[action].Event(action);
			int exit_id = exits[action];
			DoExitState(exit_id);
		}
	}
}
