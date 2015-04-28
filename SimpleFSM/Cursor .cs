using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleFSM
{
	public class Cursor<STATE_ID>: FSM.Cursor<String>
	{
		public Cursor(SimpleFSM fsm)
			: base(fsm)
		{
		} 
	}
}
