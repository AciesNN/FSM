using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;

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

[System.Serializable]
public class FSMStateModel : EditorItem
{
	[System.Serializable]
	public class ActionExit
	{
		public String action;
		public String test;

		public ActionExit(String a, String t)
		{
			test = t;
			action = a;
		}
	}

	//public struct TestActionExit 

	public String stateName;
	public ActionExit defaultExit;
	public Dictionary<String, ActionExit> rows;

	public FSMStateModel()
	{
		rows = new Dictionary<string, ActionExit>();
		defaultExit = new ActionExit("", "");
	}

	public void Draw(int id)
	{
		stateName = EditorGUILayout.TextField("Name: ", stateName);

		DrawRows();
		DrawAddButton();
		DrawDefaultRow();
	}

	private void DrawDefaultRow()
	{
		GUILayout.BeginHorizontal();

		GUILayout.Label("default");
		DrawActionExit(defaultExit);

		GUILayout.EndHorizontal();
	}

	private void DrawAddButton()
	{
		GUILayout.BeginHorizontal();

		GUILayout.FlexibleSpace();

		if (GUILayout.Button("+"))
		{
			rows[""] = new ActionExit("", "");
		}

		GUILayout.EndHorizontal();
	}

	private void DrawRows()
	{
		foreach (String test in rows.Keys)
		{
			GUILayout.BeginHorizontal();

			GUILayout.TextField(test);
			DrawActionExit(rows[test]);

			GUILayout.EndHorizontal();
		}
	}

	void DrawActionExit(ActionExit actionExit)
	{
		GUILayout.TextField(actionExit.action);
		//GUILayout.TextField(actionExit.exit);
	}
}
