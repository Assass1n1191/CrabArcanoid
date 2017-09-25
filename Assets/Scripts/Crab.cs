using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crab : MonoBehaviour 
{
    public float _moveSpeed = 5f;

	private void Awake () 
	{
		
	}

	private void Start () 
	{
		
	}
	
	private void Update () 
	{
        if (!GameScreen.Instance.GameIsStarted) return;

        #if UNITY_EDITOR || UNITY_STANDALONE
        float horizontal = Input.GetAxis("Horizontal") * _moveSpeed * Time.deltaTime;
        #endif

        if (transform.position.x > 4.5f && horizontal > 0f) horizontal = 0f;
        if (transform.position.x < -4.5f && horizontal < 0f) horizontal = 0f;


        transform.Translate(Vector3.right * horizontal);
    }
}
