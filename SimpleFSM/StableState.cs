using System;
using System.Collections.Generic;

namespace SimpleFSM
{
	public class StableState : BaseFSM.StableState<String>
	{
		public StableState(String name)
			: base(name)
		{
		}
	}
}
