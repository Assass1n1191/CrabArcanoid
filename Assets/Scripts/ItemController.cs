using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public class ItemController : MonoBehaviour
{

    private List<Item> _items;
    private float _totalSpawnProbabilityValue;

	private void Awake ()
	{
	    _items = Resources.LoadAll<GameObject>("Items").ToList()
            .Select(x => x.GetComponent<Item>()).ToList();
        _totalSpawnProbabilityValue = 0;
        _items.ForEach(x => _totalSpawnProbabilityValue += x.SpawnProbability);
	}

    public GameObject GetRandomItem()
    {
        if (_items.Count == 0)
        {
            Debug.LogError("There are no items to get. Please see the inspector.");
        }


        float randomPoint = Random.value * _totalSpawnProbabilityValue;

        for (int i = 0; i < _items.Count; i++)
        {
            var nextItem = _items[i];
            if (randomPoint < nextItem.SpawnProbability)
            {
                return nextItem.gameObject;
            }
            else
            {
                randomPoint -= nextItem.SpawnProbability;
            }
        }
        return _items.Last().gameObject;
    }

}
