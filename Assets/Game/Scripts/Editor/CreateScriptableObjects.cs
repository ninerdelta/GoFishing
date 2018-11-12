using System.Collections;
using UnityEngine;
using UnityEditor;

public class CreateScriptableObjects
{
	[MenuItem("Assets/AR Fishing/Fish Description")]
	public static void CreateFishDescription()
	{
		var desc = ScriptableObject.CreateInstance<FishDescription>();

		AssetDatabase.CreateAsset(desc, "Assets/Game/Scripts/Data/NewFishDescription.asset");
		AssetDatabase.SaveAssets();
	}
}
