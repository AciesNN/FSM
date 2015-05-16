using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shmipl
{
	class FSMFabrica: IFSMFabrica
	{
		IContext context;

		public FSMFabrica(IContext context)
		{
			this.context = context;
		}

		public SimpleFSM.FSM MakeFSM(string name)
		{
			FSM fsm = new FSM();

			return fsm;
		}
	}
}
