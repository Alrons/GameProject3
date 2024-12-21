
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
    private ItemState state;
    private int lustState;

    void Start()
    {
        timer = 0f;
        isAdedItem = false;
        state = transform.GetComponent<ItemState>();
        CheckIsAddedItem();
        InitializeLevel();
    }

    void Update()
    {
        if (enemy.GetComponent<WaveMovement>().levelDevenc <= level && state.itemState != 3)
        {
            state.itemState = 3;
        }
        else if (enemy.GetComponent<WaveMovement>().levelDevenc <= level)
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
               transform.GetComponent<ItemState>().itemState = 1;
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
        // Create a new square game object
        GameObject square = GameObject.CreatePrimitive(PrimitiveType.Cube);
        square.transform.position = transform.position; // Set the position of the square
        square.transform.localScale = new Vector3(10, 10, 0); // Set the scale of the square

        _ = square.AddComponent<BulletState>();

        // Add a script to move the square to the target position
        SquareMover mover = square.AddComponent<SquareMover>();
        mover.targetPosition = targetPosition; // Set the target position for the mover
        mover.duration = 5f; // Set the duration for the movement

        

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
            gameObject.GetComponent<BulletState>().DestroyObject();
        }
    }
}
