using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class FSMSwitchStateModel : FSMStateModel
{
	[System.Serializable]
	public struct ActionExit
	{
		public String action;
		public String test;

		public ActionExit(String a, String t)
		{
			test = t;
			action = a;
		}
	}

	public struct TestActionExit 

	public String stateName; 
	public ActionExit defaultExit;
	public Dictionary<String, ActionExit> rows;

	public FSMSwitchStateModel()
	{
		rows = new Dictionary<string, ActionExit>();
		defaultExit = new ActionExit("", "");
	}

	public override void Draw(int id)
	{
		EditorGUILayout.LabelField("Name: ");
		stateName = EditorGUILayout.TextField(stateName);

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
		GUILayout.TextField(actionExit.exit);
	}
}
