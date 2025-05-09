using Assets.scripts.Interface.Models;
using UnityEngine;
public class CellNumberModel
{
    public GameObject cell;

    public int cellNumber;

    public int tableNumber;

    public string group;

    public CellSizeDto cellSize;

    public CellNumberModel (GameObject cell, int cellNumber, int tableNumber, string group, CellSizeDto cellSize)
    {
        this.cell = cell;
        this.cellNumber = cellNumber;
        this.tableNumber = tableNumber;
        this.group = group;
        this.cellSize = cellSize;
    }
}

