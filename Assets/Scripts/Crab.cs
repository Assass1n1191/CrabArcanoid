﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crab : MonoBehaviour 
{
    public float _moveSpeed = 5f;

    private int _screenCenter;

	private void Awake () 
	{
		
	}

	private void Start () 
	{
        _screenCenter = Screen.width / 2;
    }
	
	private void Update () 
	{
        if (!GameScreen.Instance.GameIsStarted) return;

        float horizontal = 0f;

        #if UNITY_EDITOR || UNITY_STANDALONE
        horizontal = Input.GetAxis("Horizontal") * _moveSpeed * Time.deltaTime;
#elif UNITY_ANDROID
        if(Input.touchCount > 0)
        {
            Touch currentTouch = Input.touches[0];

            if (currentTouch.position.x < _screenCenter)
                horizontal = -1 * _moveSpeed * Time.deltaTime;
            else
                horizontal = 1 * _moveSpeed * Time.deltaTime;
        }

#endif

        if (transform.position.x > 4.5f && horizontal > 0f) horizontal = 0f;
        if (transform.position.x < -4.5f && horizontal < 0f) horizontal = 0f;

        transform.Translate(Vector3.right * horizontal);
    }
}
