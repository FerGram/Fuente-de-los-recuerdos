using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.SceneManagement;

public class NPCController : MonoBehaviour
{
	DialogueDisplay dialogueDisplay;

	[SerializeField]
	string locVarName;

	string locVarValue;

    // Start is called before the first frame update
    void Start()
    {
		Invoke("CheckIfPresentInTheScene", 0.01f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void CheckIfPresentInTheScene()
	{
		dialogueDisplay = GameObject.FindObjectOfType<DialogueDisplay>(true);

		if (dialogueDisplay != null
				&& dialogueDisplay._currentDialogue.variablesState.
				GlobalVariableExistsWithName(locVarName)
				)
			locVarValue = (string)dialogueDisplay._currentDialogue.variablesState[locVarName];

		if (locVarValue != null && locVarValue != SceneManager.GetActiveScene().name)
			gameObject.SetActive(false);
	}
}
