using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleFSM
{
	public class Cursor<STATE_ID>: BaseFSM.Cursor<String>
	{
		public Cursor(FSM fsm)
			: base(fsm)
		{
		} 
	}
}
