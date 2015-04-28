using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(FSMModel))]
public class FSMModelEditor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		FSMModel model = target as FSMModel;

		EditorGUILayout.BeginVertical();

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("Name:");
		EditorGUILayout.TextField(model.name);
		EditorGUILayout.EndHorizontal();

		if (GUILayout.Button("Edit...", GUILayout.MinWidth(120)))
		{
			FSMModelEditorWindow.CreateWindow(model);
		}
		EditorGUILayout.EndVertical();
	}
}
