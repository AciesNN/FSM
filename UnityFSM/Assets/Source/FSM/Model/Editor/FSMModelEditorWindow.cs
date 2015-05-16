using UnityEngine;
using UnityEditor;
using System.Collections;

public class FSMModelEditorWindow : EditorWindow {

	int leftBarWidth = 200;
	public FSMModel model;

	public static FSMModelEditorWindow CreateWindow(FSMModel model)
	{
		FSMModelEditorWindow window = (FSMModelEditorWindow)EditorWindow.GetWindow(typeof(FSMModelEditorWindow));
		window.model = model;
		window.Init();
		window.Show();
		return window;
	}

	public void Init()
	{
		
	}

	void OnGUI()
	{
		EditorGUILayout.BeginHorizontal();
		
		OnGUIMenu();
		OnGUIDrawStates();

		EditorGUILayout.EndHorizontal();
	}

	private void OnGUIMenu()
	{
		EditorGUILayout.BeginVertical(GUILayout.Width(leftBarWidth));
		OnGUIModel();
		EditorGUILayout.Space();
		OnGUIAddButtons();
		GUILayout.FlexibleSpace();
		OnGUISave();
		EditorGUILayout.EndVertical();
		
	}

	private void OnGUISave()
	{
		if (GUILayout.Button("Clear"))
		{
			model.Clear();
		}
		if (GUILayout.Button("Save"))
		{
			EditorUtility.SetDirty(model.gameObject);
		}
	}

	private void OnGUIAddButtons()
	{
		if (GUILayout.Button("Add sub FSM"))
		{
			model.AddSubFSM();
		}
		if (GUILayout.Button("Add switch state"))
		{
			model.AddSwitchState();
		}
		if (GUILayout.Button("Add stable state"))
		{
			model.AddStableState();
		}
	}

	private void OnGUIModel()
	{
		EditorGUILayout.LabelField("FSM name:");
		model.name = EditorGUILayout.TextField(model.name);
	}

	private void OnGUIDrawStates()
	{
		BeginWindows();

		for ( int i = 0; i < model.items.Count; ++i )
			DrawState(i, model.items[i]);

		EndWindows();
	}

	private void DrawState(int i, FSMStateModel item)
	{
		if (item == null)
			return;
		item.position = GUILayout.Window(i, item.position, (id) => StateWndProc(i, item), item.GetType().ToString());
	}

	private void StateWndProc(int id, FSMStateModel item)
	{
		item.Draw(id);
		GUI.DragWindow();
	}
}
