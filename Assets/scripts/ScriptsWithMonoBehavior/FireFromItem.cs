using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFromItem : MonoBehaviour
{
    public int level;
    public Vector3 targetPosition;
    public float spawnInterval = 1f; // adjust this value to change the spawn interval
    public GameObject enemy;
    public GameObject mainCamera;
    private bool isAdedItem;

    private float timer;

    void Start()
    {
        timer = 0f;
        isAdedItem = false;
        CheckIsAddedItem();
        InitializeLevel();
    }

    void Update()
    {

        if (enemy.GetComponent<WaveMovement>().levelDevenc <= level)
        {
            targetPosition = enemy.transform.position;
            timer += Time.deltaTime;
            if (timer >= spawnInterval)
            {
                SpawnSquare();
                timer = 0f;
            }
        }

    }
    private void CheckIsAddedItem()
    {
        TableCreator tableCreator = mainCamera.GetComponent<TableCreator>();
        int Count = 1;
        foreach (CellNumberModel Cell in tableCreator.hashSetCellNumber)
        {
            GameObject gameobj = Cell.cell;
            
           if(gameobj.transform == transform.parent)
           {
               isAdedItem = true; break;
           }
            
        }
    }

    private int FindTableNumber(int NumberCell)
    {
        TableCreator tableCreator = mainCamera.GetComponent<TableCreator>();
        foreach (CellNumberModel cellClass in tableCreator.hashSetCellNumber)
        {
            if (cellClass.cellNumber == NumberCell)
            {
                return cellClass.tableNumber;
            }
        }
        return -1;
    }
    public void InitializeLevel()
    {
        if (isAdedItem)
        {
            int place = transform.GetComponent<DragDrop>().Place;

            switch (FindTableNumber(place))
            {
                case 1:
                    level = 4; break;
                case 2:
                    level = 3; break;
                case 3:
                    level = 2; break;
                case 4:
                    level = 1; break;

            }
        }
        
    }
    void SpawnSquare()
    {
        // create a new square game object
        GameObject square = GameObject.CreatePrimitive(PrimitiveType.Cube);
        square.transform.position = transform.position;
        square.transform.localScale = new Vector3(10, 10, 0);

        // add a script to move the square to the target position
        SquareMover mover = square.AddComponent<SquareMover>();
        mover.targetPosition = targetPosition;
        mover.duration = 5f;

    }
}

public class SquareMover : MonoBehaviour
{
    public Vector3 targetPosition;
    public float duration;

    private Vector3 startPosition;
    private float timer;

    void Start()
    {
        startPosition = transform.position;
        timer = 0f;
    }

    void Update()
    {
        timer += Time.deltaTime;
        float t = timer / duration;
        transform.position = Vector3.Lerp(startPosition, targetPosition, t);

        // Check if the square has reached the target position
        if (t >= 1f)
        {
            // Remove the square game object
            Destroy(gameObject);
        }
    }
}
