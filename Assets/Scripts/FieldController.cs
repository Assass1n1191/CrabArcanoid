using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FieldController : MonoBehaviour
{
    public static FieldController Instance;

    public Vector2 FieldSize = new Vector2(6,6);
    public GameObject Block1;
    public GameObject Block2;
    public Transform Field;

    public class ItemInfo
    {
        public Vector2 Position { get; set; }
        public Item Item{ get; set; }

    //public ItemInfo(Item item, Vector2 position)
    //{
    //    Item = item;
    //    Position = position;
    //}
}
    

    private List<List<ItemInfo>> _items;
    private List<Vector2> _spawnPointsRow;

    private float _blocksOffsetX;
    private float _blocksOffsetY;
    private static float SPAWN_STEP = 1.6f;

    private ItemController _itemController;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        //DontDestroyOnLoad(gameObject);
    }

    private void Start ()
    {
        _itemController = GetComponent<ItemController>();
        _blocksOffsetX = FieldSize.x / 2;
        _blocksOffsetY = FieldSize.y / 2;
        SetUpField();
        
	}

    private void SetUpField()
    {
        _items = new List<List<ItemInfo>>();
        _spawnPointsRow = new List<Vector2>();
        for (int col = 0; col < FieldSize.y; col++)
        {
            _items.Add(new List<ItemInfo>());
            for (int row = 0; row < FieldSize.x; row++)
            {
                var block = (row % 2 == col % 2) ? Block1 : Block2;
                Vector2 spawnPos = new Vector2((col - _blocksOffsetX) * SPAWN_STEP + SPAWN_STEP / 2,
                                               (row - _blocksOffsetY) * SPAWN_STEP + SPAWN_STEP / 2);
                InstantiateBlock(block, spawnPos);

                _items[col].Add(InstantiateItem(_itemController.GetRandomItem(), spawnPos, new Vector2(col, row)));
            }
            _spawnPointsRow.Add(new Vector2((col - _blocksOffsetX) * SPAWN_STEP + SPAWN_STEP / 2, (FieldSize.x - _blocksOffsetY) * SPAWN_STEP + SPAWN_STEP / 2));
        }



        //потом удалить
        _spawnPointsRow.ForEach(x =>
        {

            GameObject newBlock = Instantiate(Block1, transform);
            newBlock.transform.localScale = new Vector3(.5f,.5f,.5f);
            newBlock.transform.localPosition = x;
        });
    }


    private void InstantiateBlock(GameObject block, Vector2 posInField)
    {
        GameObject newBlock = Instantiate(block, Field);
        newBlock.transform.localScale = Vector3.one;
        newBlock.transform.localPosition = posInField;
    }

    private ItemInfo InstantiateItem(GameObject item, Vector2 spawnPos, Vector2 posInField)
    {
        GameObject newItem = Instantiate(item, Field);
        //newItem.transform.localScale = Vector3.one;
        newItem.transform.localPosition = spawnPos;
        var itemScript = newItem.GetComponent<Item>();
        itemScript.SetUpItemInField(posInField);
        return new ItemInfo()
        {
            Item = itemScript,
            Position = spawnPos
        };
    }


    public void ItemIsDestroyed(Vector2 itemPos)
    {
        int posX = (int) itemPos.x;
        int posY = (int) itemPos.y;
        _items[posX].RemoveAt(posY);

        var newItemInfo = InstantiateItem(_itemController.GetRandomItem(), _spawnPointsRow[posX], new Vector2(posX, FieldSize.y));
        _items[(int)itemPos.x].Add(newItemInfo);

        for (int i = posY; i < _items.Count; i++)
        {
            var item = _items[(int) itemPos.x][i];
            var destinationY = item.Position.y - SPAWN_STEP;
            item.Position = new Vector2(item.Position.x, destinationY);
            _items[(int) itemPos.x][i].Item.MoveDown(destinationY/*SPAWN_STEP*/, 1);
            //_items[(int) itemPos.x][i].Position.x = new SPAWN_STEP;
        }
    }

}
