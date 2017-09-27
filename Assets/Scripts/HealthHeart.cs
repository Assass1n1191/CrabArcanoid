using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHeart : MonoBehaviour 
{
    public Sprite FullHeart;
    public Sprite EmptyHeart;
    private Image _myImageComponent;

	private void Awake () 
	{
		
	}

	private void Start () 
	{
        _myImageComponent = GetComponent<Image>();
	}
	
	public void ChangeSprite(bool isFull)
    {
        _myImageComponent.sprite = isFull ? FullHeart : EmptyHeart;
    }
}
