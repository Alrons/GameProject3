using UnityEngine;

using UnityEngine.UI;

//this script is bound to the ourLineText variable, here the power is calculated for the variable to which it is bound
public class PowerForLine : MonoBehaviour
{
    public int StartNumberCell { get; set; }

    public int EndNumberCell { get; set; }

    private int power = 0;

    public Text ourLineText;

    public GameObject mainCamera;

    public bool CulculateLine()
    {
        TableCreator tableCreator = mainCamera.GetComponent<TableCreator>();
        power = 0;
        foreach (CellNumberModel cellClass in tableCreator.hashSetCellNumber)
        {
            if (cellClass.cellNumber <= EndNumberCell && cellClass.cellNumber > StartNumberCell)
            {
                GameObject cell = cellClass.cell;
                foreach (Transform item in cell.transform)
                {
                    DragDrop dragDrop = item.GetComponent<DragDrop>();
                    power += dragDrop.Power;
                }
            }
            
        }
        ourLineText.text = $"{power}";
        return true;
    }
}
