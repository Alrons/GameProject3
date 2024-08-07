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
        power = 0;
        for (int i = StartNumberCell; i < EndNumberCell; i++)
        {
            TableCreator tableCreator = mainCamera.GetComponent<TableCreator>();
            GameObject cell = tableCreator.ourCell[i];
            foreach (Transform item in cell.transform)
            {
                DragDrop dragDrop = item.GetComponent<DragDrop>();
                power += dragDrop.Power;
            }
        }
        ourLineText.text = $"{power}";
        return true;
    }
}
