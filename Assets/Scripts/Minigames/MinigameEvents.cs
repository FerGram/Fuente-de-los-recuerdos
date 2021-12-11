using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MinigameEvents : MonoBehaviour
{
	public static MinigameEvents current;

	public event Action<int> onLoadMinigame;
	public event Action<int> onUnloadMinigame;

	public bool insideMinigame;

	void Awake()
    {
		current = this;
    }

    public void LoadMinigame(int minigame)
	{
		if (onLoadMinigame != null) {
			insideMinigame = true;
			onLoadMinigame(minigame);
		}
			
	}

	public void UnloadMinigame(int minigame)
	{
		if (onUnloadMinigame != null)
		{
			insideMinigame = false;
			onUnloadMinigame(minigame);
		}
			
	}
}
