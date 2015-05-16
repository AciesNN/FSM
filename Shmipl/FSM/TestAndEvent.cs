using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shmipl
{
	class TestAndEvent : BaseFSM.TestAndEventAction
	{
		IContext context;

		public TestAndEvent(IContext context, System.Func<Object, bool> test, System.Action<Object> _event)
			: base(test, _event)
		{
			this.context = context;
		}
	}
}
