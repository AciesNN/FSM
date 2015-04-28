using System;
using System.Collections.Generic;

namespace FSM
{
	public interface IStateWithFSM<STATE_ID>
	{
		FSM<STATE_ID> GetSubFSM();
	}

	public class StateWithFSM<STATE_ID> : State<STATE_ID>, IStateWithFSM<STATE_ID>
	{
		protected FSM<STATE_ID> subFsm;

		private static readonly int subFSMExitID = -1;

		public StateWithFSM(STATE_ID id)
			: base(id)
		{
		}

		public void SetSubFSM(FSM<STATE_ID> subFsm)
		{
			this.subFsm = subFsm;
			subFsm.OnFinish += OnSubFSMFinish;
		}

		public override void Enter()
		{
			LoadFSM();
			subFsm.Start();
		}

		public override void Exit()
		{
			UnloadFSM();
		}

		public virtual void LoadFSM()
		{
		}

		public virtual void UnloadFSM()
		{
		}

		public void OnSubFSMFinish(FSM<STATE_ID> fsm, STATE_ID state_id)
		{
			subFsm.OnFinish -= OnSubFSMFinish;
			DoExitState(subFSMExitID);
		}

		public override void DoAction(object actionObject)
		{
			if (subFsm != null)
				subFsm.DoAction(actionObject);
		}

		public FSM<STATE_ID> GetSubFSM()
		{
			return subFsm;
		}

		public StateExit<STATE_ID> SubFSMExit()
		{
			return new StateExit<STATE_ID>(this, subFSMExitID);
		}
	}
}
