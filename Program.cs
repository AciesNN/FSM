using System;
using System.Collections.Generic;

enum StatesID
{
	ONE,
	TWO,
	THREE,
	FOUR,

	SUB_ONE,
	SUB_TWO
}

class StableState : BaseFSM.StableState<StatesID>
{
	public StableState(StatesID id)
		: base(id)
	{
	}

	public override void Enter()
	{
		System.Console.WriteLine("{0} Enter", this);
		Program.condition = !Program.condition;
	}

	public override void Exit()
	{
		base.Exit();
		System.Console.WriteLine("{0} Exit", this);
	}
}

class SwitchState : BaseFSM.SwitchState<StatesID, String>
{
	public SwitchState(StatesID id)
		: base(id)
	{
	}

	public override void Enter()
	{
		System.Console.WriteLine("{0} Enter", this);
		base.Enter();
	}

	public override void Exit()
	{
		base.Exit();
		System.Console.WriteLine("{0} Exit", this);
	}
}

class StateWithFSM : BaseFSM.StateWithFSM<StatesID>
{
	public StateWithFSM(StatesID id)
		: base(id)
	{
	}

	public override void LoadFSM()
	{
		BaseFSM.FSM<StatesID> subFsm = new BaseFSM.FSM<StatesID>("Sub");
		subFsm.OnFinish += (x, y) => { System.Console.WriteLine("*** subFSM onFinish()"); };

		SetSubFSM(subFsm);

		StableState stateSubOne = new StableState(StatesID.SUB_ONE) { isStart = true };
		stateSubOne.AddAction("DoSubA", (o) => true, (o) => System.Console.WriteLine("sub action a"))
			.AddExitTransition(subFsm, StatesID.SUB_TWO);

		SwitchState stateSubTwo = new SwitchState(StatesID.SUB_TWO) { isFinish = true };
		subFsm.AddState(stateSubTwo);
	}

	public override void UnloadFSM()
	{
		subFsm = null;
	}
}

class Program
{
	public static bool condition = true;

	static void Main(string[] args)
	{
		BaseFSM.FSM<StatesID> fsm = new BaseFSM.FSM<StatesID>("Main");

		/*************/

		//Main
		SwitchState stateOne = new SwitchState(StatesID.ONE) { isStart = true };
		stateOne.DefaultExit()
			.AddExitTransition(fsm, StatesID.FOUR);
		stateOne.AddTestExit(() => condition)
			.AddExitTransition(fsm, StatesID.TWO);

		SwitchState stateTwo = new SwitchState(StatesID.TWO);
		stateTwo.DefaultExit()
			.AddExitTransition(fsm, StatesID.FOUR);
		stateTwo.AddTestExit(() => condition)
			.AddExitTransition(fsm, StatesID.THREE);

		StableState stateThree = new StableState(StatesID.THREE);
		stateThree.AddAction("DoA", (o) => !condition, (o) => System.Console.WriteLine("action a"))
			.AddExitTransition(fsm, StatesID.ONE);
		stateThree.AddAction("DoB", (o) => condition, (o) => System.Console.WriteLine("action b"))
			.AddExitTransition(fsm, StatesID.FOUR);

		StateWithFSM stateFour = new StateWithFSM(StatesID.FOUR);
		stateFour.SubFSMExit()
			.AddExitTransition(fsm, StatesID.THREE);

		/////////////

		BaseFSM.Cursor<StatesID> cursor = fsm.GetCursor();
		System.Console.WriteLine(cursor);

		fsm.Start();
		System.Console.WriteLine(cursor);

		if (cursor.curState.id == StatesID.THREE)
			cursor.DoAction("DoA");
		System.Console.WriteLine(cursor);

		if (cursor.curState.id == StatesID.SUB_ONE)
			cursor.DoAction("DoSubA");
		System.Console.WriteLine(cursor);
	}
}

