using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "JSONDataContainer", menuName = "JSONDataContainer", order = 52)]
public class JSONDataContainer : ScriptableObject
{
    private TextAsset JSONFile;

    public TextAsset GetJSON() => JSONFile;

    public void SetJSON(TextAsset newJSON){
        JSONFile = newJSON;
    }
}
