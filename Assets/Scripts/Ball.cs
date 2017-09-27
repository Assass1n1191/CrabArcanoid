using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float MoveSpeed = 3f;
    private Vector3 _moveDir = Vector3.zero;

    private Vector3 _moveDirection
    {
        get
        {
            return _moveDir;
        }
        set
        {
            value.Normalize();
            _moveDir = value;
        }
    }


	private void Awake () 
	{
		
	}

	private void Start () 
	{

    }
	
	private void Update () 
	{
        if(!GameScreen.Instance.GameIsStarted & Input.GetKeyDown(KeyCode.Space))
        {
            InitMoveDirection();
            GameScreen.Instance.GameIsStarted = true;
        }

        if (!GameScreen.Instance.GameIsStarted) return;

        transform.Translate(_moveDirection * MoveSpeed * Time.deltaTime);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.tag)
        {
            case "Crab":
                OnCrabTouch(collision.transform.position);
                break;
            case "Top Edge":
                OnEdgeTouch(yMultiplier : -1);
                break;
            case "Bottom Edge":
                OnEdgeTouch(yMultiplier: -1);
                //You lose
                break;
            case "Left Edge":
                OnEdgeTouch(xMultiplier : -1f);
                break;
            case "Right Edge":
                OnEdgeTouch(xMultiplier: -1f);
                break;
            case "Item":
                collision.GetComponent<Item>().GetScore();
                OnCrabTouch(collision.transform.position);
                break;

            default:
                break;
        }
    }

    private void InitMoveDirection()
    {
        Vector2 targetDirection;
        float xAxisDirectionRandomize = Random.Range(-2f, 2f);
        float yAxisDirectionRandomize = Random.Range(transform.position.y, transform.position.y + 2f);

        targetDirection = new Vector2(xAxisDirectionRandomize, yAxisDirectionRandomize);

        _moveDirection = targetDirection - (Vector2)transform.position;
    }

    private void OnEdgeTouch(float xMultiplier = 1f, float yMultiplier = 1f)
    {
        _moveDirection = new Vector3(_moveDirection.x * xMultiplier, _moveDirection.y * yMultiplier);
    }

    private void OnCrabTouch(Vector2 crabPos)
    {
        _moveDirection = (crabPos - (Vector2)transform.position) * -1f;
    }
}
