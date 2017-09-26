using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldController : MonoBehaviour
{

    public Vector2 FieldSize = new Vector2(6,6);
    public GameObject Block1;
    public GameObject Block2;


    private float blocksOffsetX;
    private float blocksOffsetY;
    private static float SPAWN_STEP = 1.6f;

    private void Start ()
    {
        blocksOffsetX = FieldSize.x / 2;
        blocksOffsetY = FieldSize.y / 2;
        SetUpField();

	}

    private void SetUpField()
    {
        for (int col = 0; col < FieldSize.y; col++)
        {
            for (int row = 0; row < FieldSize.x; row++)
            {
                var block = (row % 2 == col % 2) ? Block1 : Block2;
                InstantiateBlock(block, new Vector2((col  - blocksOffsetX) * SPAWN_STEP + SPAWN_STEP/2, (row - blocksOffsetY) * SPAWN_STEP + SPAWN_STEP/2));
            }
        }
    }

    private void InstantiateBlock(GameObject block, Vector2 posInField)
    {
        GameObject newBlock = Instantiate(block, transform);
        newBlock.transform.localScale = Vector3.one;
        newBlock.transform.localPosition = posInField;
    }
	

}
