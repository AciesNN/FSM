using System;
using System.Collections.Generic;

namespace SimpleFSM
{
	public class StateWithFSM : BaseFSM.StateWithFSM<String>
	{
		public StateWithFSM(string name)
			: base(name)
		{
		}
	}
}
