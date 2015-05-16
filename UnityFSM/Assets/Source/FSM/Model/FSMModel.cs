using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FSMModel : MonoBehaviour {

	[HideInInspector]
	public List<FSMStateModel> items = new List<FSMStateModel>();

	public void AddSubFSM()
	{
		FSMStateModel state = new FSMStateModel();
		items.Add(state);
	}

	public void AddSwitchState()
	{
		FSMStateModel state = new FSMStateModel();
		items.Add(state);
	}

	public void AddStableState()
	{
		FSMStateModel state = new FSMStateModel();
		items.Add(state);
	}

	public void Clear()
	{
		items = new List<FSMStateModel>();
	}
}

