using UnityEngine;
using UnityEditor;
using System.Collections;

[System.Serializable]
public abstract class FSMStateModel : FSMModel.EditorItem 
{

	public string id;

	public FSMStateModel()
		: base()
	{
	}

	public FSMStateModel(Vector2 startPosition)
		: base(startPosition)
	{
	}
}
