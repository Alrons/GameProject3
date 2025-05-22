using UnityEngine;

public class FireFromItem : MonoBehaviour
{
    public int level;
    public Vector3 targetPosition;
    public float spawnInterval = 1f;
    public GameObject enemy;
    public GameObject mainCamera;
    private bool isAdedItem;
    private float timer;
    private float damagePerBullet;

    void Start()
    {
        timer = 0f;
        isAdedItem = false;
        CheckIsAddedItem();
        InitializeLevel();
        CalculateBulletDamage();
    }

    void Update()
    {
        if (enemy.GetComponent<Wave>().levelDevenc <= level)
        {
            targetPosition = enemy.transform.position;
            timer += Time.deltaTime;
            if (timer >= spawnInterval)
            {
                CalculateBulletDamage();
                SpawnSquare();
                timer = 0f;
            }
        }
    }

    private void CalculateBulletDamage()
    {
        float power = transform.GetComponent<DragDrop>().Power;
        damagePerBullet = power * spawnInterval;
    }

    private void CheckIsAddedItem()
    {
        TableCreator tableCreator = mainCamera.GetComponent<TableCreator>();
        foreach (CellNumberModel Cell in tableCreator.hashSetCellNumber)
        {
            GameObject gameobj = Cell.cell;
            if (gameobj.transform == transform.parent)
            {
                isAdedItem = true;
                break;
            }
        }
    }

    private int FindTableNumber(int NumberCell)
    {
        TableCreator tableCreator = mainCamera.GetComponent<TableCreator>();
        foreach (CellNumberModel cellClass in tableCreator.hashSetCellNumber)
        {
            if (cellClass.group == "Group 1" && cellClass.cellNumber == NumberCell)
            {
                return 1;
            }
            if (cellClass.group == "Group 2" && cellClass.cellNumber == NumberCell)
            {
                return 2;
            }
            if (cellClass.group == "Group 3" && cellClass.cellNumber == NumberCell)
            {
                return 3;
            }
            if (cellClass.group == "Group 4" && cellClass.cellNumber == NumberCell)
            {
                return 4;
            }
            if (cellClass.group == "Group 5" && cellClass.cellNumber == NumberCell)
            {
                return 5;
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
                case 1: level = 5; break;
                case 2: level = 4; break;
                case 3: level = 3; break;
                case 4: level = 2; break;
                case 5: level = 1; break;
            }
        }
    }

    void SpawnSquare()
    {
        GameObject square = GameObject.CreatePrimitive(PrimitiveType.Cube);
        square.transform.position = transform.position;
        square.transform.localScale = new Vector3(10, 10, 0);

        SquareMover mover = square.AddComponent<SquareMover>();
        mover.targetPosition = targetPosition;
        mover.duration = 5f;
        mover.enemyWave = enemy.GetComponent<Wave>();
        mover.damage = damagePerBullet;
    }
}

public class SquareMover : MonoBehaviour
{
    public Vector3 targetPosition;
    public float duration;
    public Wave enemyWave;
    public float damage;

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

        if (t >= 1f)
        {
            if (enemyWave != null)
            {
                enemyWave.GetDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
