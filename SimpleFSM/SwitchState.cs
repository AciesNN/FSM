using System;
using System.Collections.Generic;

namespace SimpleFSM
{
	public abstract class StateTest
	{
		public abstract bool Test();
	}

	public class SwitchState : FSM.SwitchState<String, String>
	{
		public SwitchState(String name)
			: base(name)
		{
		}
	}
}
