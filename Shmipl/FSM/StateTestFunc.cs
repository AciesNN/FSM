using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shmipl
{
	class StateTestFunc: BaseFSM.StateTestFunc
	{
		IContext context;

		public StateTestFunc(IContext context, System.Func<bool> t)
			: base(t)
		{
			this.context = context;
		}
	}
}
