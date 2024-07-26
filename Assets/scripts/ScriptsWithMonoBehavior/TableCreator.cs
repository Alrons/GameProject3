using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableCreator : MonoBehaviour
{
    public Transform Canvas;
    public Transform CellBackgroundTransform;
    public Transform CellContentTransform;
    public Text OurLinePower;
    private RectTransform RectTransfrom;

    // save our Cell, number in list will be count number this cell
    public List<GameObject> OurCell = new List<GameObject>();
    public List<Text> TextsLinePower = new List<Text>();

    private int CellSpacing = 100; // distance between the cells

    public void CreateTable(int width, int height, Vector3 Position, double Rotate)
    {
        // Creating a table background
        GameObject gameObject = CopyPref(CellBackgroundTransform.gameObject);
        gameObject.transform.SetParent(Canvas);
        gameObject.transform.position = Position;

        // Creating a table

        for (int i = 0; i < height; i++)
        {
            // for the future
            //int startNumberCell = ourCell.Count;

            for (int j = 0; j < width; j++)
            {
                // copy of the cell
                GameObject cellBackground = CopyPref(CellContentTransform.gameObject);

                // save our cell
                OurCell.Add(cellBackground);

                cellBackground.transform.localScale = new Vector3(1, 1, 1); // change cell size
                cellBackground.transform.SetParent(gameObject.transform);

                cellBackground.transform.position = gameObject.transform.position + new Vector3(j * (100 + CellSpacing), -i * (30 + CellSpacing), 0); // changing position of the cell


                if (j == (width - 1))
                {
                    Text lineText = Instantiate(OurLinePower);
                    lineText.transform.SetParent(gameObject.transform);
                    lineText.transform.position = gameObject.transform.position + new Vector3((j + 1) * (100 + CellSpacing), -i * (30 + CellSpacing), 0);
                    
                    
                    // for the future

                    //PowerForLine powerForLine = lineText.GetComponent<PowerForLine>();
                    //powerForLine.StartNumberCell = startNumberCell;
                    //powerForLine.EndNumberCell = startNumberCell + width;
                    //textsLinePower.Add(lineText);
                }
            }

        }

        gameObject.transform.localScale = new Vector3(1, 1, 1);
        RectTransfrom = gameObject.GetComponent<RectTransform>();
        RectTransfrom.sizeDelta = new Vector2(((width - 1) * 200) + 170, ((height - 1) * 130) + 75);
        gameObject.transform.Rotate(0, 0, (float)Rotate);
    }

    //For copy
    public GameObject CopyPref(GameObject box)
    {
        var spawn = Instantiate(box);
        spawn.transform.localScale = new Vector3(1, 1, 1);
        return spawn;

    }
}
