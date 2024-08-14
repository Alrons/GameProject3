using UnityEngine;

public class CellNumberModel
{
    public GameObject cell;

    public int cellNumber;

    public int tableNumber;

    public CellNumberModel (GameObject cell, int cellNumber, int tableNumber)
    {
        this.cell = cell;
        this.cellNumber = cellNumber;
        this.tableNumber = tableNumber;
    }
}

