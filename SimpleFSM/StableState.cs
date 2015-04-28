using System;
using System.Collections.Generic;

namespace SimpleFSM
{
	public class StableState : FSM.StableState<String>
	{
		public StableState(String name)
			: base(name)
		{
		}
	}
}
