using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public class ItemController : MonoBehaviour
{

    private List<GameObject> _items;

	private void Awake ()
    {
        _items = Resources.LoadAll<GameObject>("Items").ToList();
    }

    public GameObject GetRandomItem()
    {
        if (_items.Count == 0)
        {
            Debug.LogError("There are no items to get. Please see the inspector.");
        }
        var rand = Random.Range(0, _items.Count);
        return _items[rand];
    }

}
