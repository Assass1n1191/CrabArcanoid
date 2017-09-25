using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScreen : MonoBehaviour 
{
    public static GameScreen Instance;

    public bool GameIsStarted = false;

	private void Awake () 
	{
        Instance = this;
	}

	private void Start () 
	{
		
	}
	
	private void Update () 
	{
		
	}
}
