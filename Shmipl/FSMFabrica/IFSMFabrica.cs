using System;
using System.Collections.Generic;

namespace Shmipl
{
	interface IFSMFabrica
	{
		SimpleFSM.FSM MakeFSM(string name);
	}
}
