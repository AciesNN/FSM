using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FSMModel : MonoBehaviour {

	[System.Serializable]
	public class EditorItem
	{
		public Rect position = new Rect(230, 30, 10, 10);

		public EditorItem() { }

		public EditorItem(Vector2 startPosition)
		{
			position.center = startPosition;
		}

		public virtual void Draw(int id)
		{
		}
	}

	[HideInInspector]
	public List<EditorItem> items = new List<EditorItem>();

	public void AddSubFSM()
	{
		FSMStateModel state = new FSMSwitchStateModel();
		items.Add(state);
	}

	public void AddSwitchState()
	{
		FSMStateModel state = new FSMSwitchStateModel();
		items.Add(state);
	}

	public void AddStableState()
	{
		FSMStateModel state = new FSMSwitchStateModel();
		items.Add(state);
	}

	public void Clear()
	{
		items = new List<EditorItem>();
	}
}

