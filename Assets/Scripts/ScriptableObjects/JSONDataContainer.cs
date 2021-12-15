using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "JSONDataContainer", menuName = "JSONDataContainer", order = 52)]
public class JSONDataContainer : ScriptableObject
{
	[SerializeField]
    private TextAsset JSONFile;
	private string path;

    public TextAsset GetJSON() => JSONFile;
	public string GetPath() => path;

    public void SetJSON(TextAsset newJSON){
        JSONFile = newJSON;
    }

	public void SetPath(string newPath)
	{
		path = newPath;
	}
}
