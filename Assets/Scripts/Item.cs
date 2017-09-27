using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Shrimp1,
    Shrimp2,
    Shrimp3,
    SeaUrchin,
    Clock
}

public class Item : MonoBehaviour 
{
    public ItemType Type;
    public float SpawnProbability;

    private float groundY = -9f;
    private GameObject _bubble;

    private Vector2 _posInField;

    public bool IsOnTheGround = false;

	private void Awake () 
	{
		
	}

	private void Start () 
	{
        _bubble = transform.GetChild(0).gameObject;
	}
	
	private void Update () 
	{
	    //if(Input.GetKeyDown(KeyCode.Q))
     //   {
     //       MoveTo();
     //   }

     //   if (Input.GetKeyDown(KeyCode.W))
     //   {
     //       Fall();
     //   }

     //   if (Input.GetKeyDown(KeyCode.E))
     //   {
     //       GetScore();
     //   }
    }

    public void OnBallHit()
    {
        switch(Type)
        {
            case ItemType.Shrimp1:
                GetScore();
                GameScreen.Instance.ChangeScore(100);
                break;
            case ItemType.Shrimp2:
                GetScore();
                GameScreen.Instance.ChangeScore(200);
                break;
            case ItemType.Shrimp3:
                GetScore();
                GameScreen.Instance.ChangeScore(300);
                break;
            case ItemType.SeaUrchin:
                FieldController.Instance.ItemIsDestroyed(_posInField);
                Fall();
                break;
            case ItemType.Clock:
                FieldController.Instance.ItemIsDestroyed(_posInField);
                Fall();
                break;
        }
    }

    public void MoveDown(float desinationY, int posCount)
    {
        _posInField.y -= posCount;
        iTween.MoveTo(gameObject, new Vector2(transform.position.x, desinationY/*transform.position.y - verticalDistance*/), 1f);
    }

    public void Fall()
    {
        IsOnTheGround = true;

        iTween.ColorTo(_bubble, new Color(1f, 1f, 1f, 0f), 0.2f);
        iTween.ScaleTo(_bubble, new Vector3(1.2f, 1.2f, 1.2f), 0.2f);
        iTween.MoveTo(gameObject, iTween.Hash("position", new Vector3(transform.position.x, groundY, 0f),
                                              "delay", 0.1f,
                                              "time", 2f/*,
                                              "easetype", iTween.EaseType.linear*/));

        StartCoroutine(DisappearFromTheGround());
    }

    public void GetScore()
    {
        GetComponent<Collider2D>().enabled = false;
        iTween.ColorTo(_bubble, new Color(1f, 1f, 1f, 0f), 0.2f);
        iTween.ScaleTo(_bubble, new Vector3(1.2f, 1.2f, 1.2f), 0.2f);
        iTween.MoveTo(gameObject, iTween.Hash("position", new Vector3(transform.position.x, transform.position.y + 1f, 0f),
                                              "delay", 0.1f,
                                              "time", 1f));
        iTween.ColorTo(gameObject, iTween.Hash("color", new Color(1f, 1f, 1f, 0f),
                                               "oncomplete", "Destroy",
                                               "delay", 0.3f,
                                               "time", 0.5f));
    }

    public void Destroy()
    {
        FieldController.Instance.ItemIsDestroyed(_posInField);
        Destroy(gameObject);
    }

    public void SetUpItemInField(Vector2 posInField)
    {
        _posInField = posInField;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag.Equals("Crab"))
        {
            switch (Type)
            {
                case ItemType.SeaUrchin:
                    GameScreen.Instance.ChangeHealthAmount(-1);
                    Destroy(gameObject);
                    break;
                case ItemType.Clock:
                    GameScreen.Instance.ChangeTime(5);
                    Destroy(gameObject);
                    break;
            }
        }
    }

    private IEnumerator DisappearFromTheGround()
    {
        yield return new WaitForSeconds(2f);
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        for(int i = 0; i < 2; i++)
        {
            sr.color = new Color(1f, 1f, 1f, 0.5f);
            yield return new WaitForSeconds(1f);
            sr.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(1f);
        }

        Destroy(gameObject);
    }
}
