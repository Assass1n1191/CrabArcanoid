using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public static Ball Instance;

    private Vector3 initPos = new Vector3(0f, -7.65f, 0f);
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
        Instance = this;
	}

	private void Start () 
	{

    }
	
	private void Update () 
	{
        if (!GameScreen.Instance.GameIsStarted || GameScreen.Instance.GameWasReseted) return;

        transform.Translate(_moveDirection * MoveSpeed * Time.deltaTime);
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        switch(col.tag)
        {
            case "Item":
                if (col.GetComponent<Item>().IsOnTheGround) return;
                col.gameObject.GetComponent<Item>().OnBallHit();
                OnItemTouch(col.transform.position);
                //OnCrabTouch(col.transform.position);
                break;
            case "Medusa":
                OnCrabTouch(col.transform.position);
                break;
            case "Top Edge":
                OnEdgeTouch(yMultiplier : -1);
                break;
            case "Bottom Edge":
                GameScreen.Instance.ResetToStart();
                //OnEdgeTouch(yMultiplier: -1);
                //You lose
                break;
            case "Left Edge":
                OnEdgeTouch(xMultiplier : -1f);
                break;
            case "Right Edge":
                OnEdgeTouch(xMultiplier: -1f);
                break;
            default:
                break;
        }
    }

    public void InitMoveDirection(Vector2 targetDirection)
    {
        //Vector2 targetDirection;
        //float xAxisDirectionRandomize = Random.Range(-2f, 2f);
        //float yAxisDirectionRandomize = Random.Range(transform.position.y, transform.position.y + 2f);

        //targetDirection = new Vector2(xAxisDirectionRandomize, yAxisDirectionRandomize);

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

    private void OnItemTouch(Vector3 itemPos)
    {
        float xDifference = 0f;
        float yDifference = 0f;

        xDifference = Mathf.Abs(transform.position.x - itemPos.x);
        yDifference = Mathf.Abs(transform.position.y - itemPos.y);

        if(xDifference > yDifference) //Left or right side
        {
            OnEdgeTouch(xMultiplier: -1f);

            //if (transform.position.x - itemPos.x > 0) //Right
            //{
            //    OnEdgeTouch(xMultiplier: -1f);
            //}
            //else //Left
            //{
            //    OnEdgeTouch(xMultiplier: -1f);
            //}
        }
        else //Top or bot
        {
            OnEdgeTouch(yMultiplier: -1);

            //if (transform.position.y - itemPos.y > 0) //Top
            //{
            //    OnEdgeTouch(yMultiplier: -1);
            //}
            //else //Bot
            //{
            //    OnEdgeTouch(yMultiplier: -1);
            //}
        }
    }

    public void ResetPosition()
    {
        transform.position = initPos;
        _moveDirection = Vector3.zero;
    }
}
