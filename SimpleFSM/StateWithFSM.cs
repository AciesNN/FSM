using System;
using System.Collections.Generic;

namespace SimpleFSM
{
	public class StateWithFSM : FSM.StateWithFSM<String>
	{
		public StateWithFSM(string name)
			: base(name)
		{
		}
	}
}
