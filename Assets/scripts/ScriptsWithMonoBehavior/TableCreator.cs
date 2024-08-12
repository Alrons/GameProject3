using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableCreator : MonoBehaviour
{
    public Transform canvas;
    public Transform cellBackgroundTransform;
    public Transform cellContentTransform;
    public Text ourLinePower;
    private RectTransform rectTransfrom;

    // save our Cell, number in list will be count number this cell
    public List<GameObject> ourCell = new List<GameObject>();
    public List<Text> textsLinePower = new List<Text>();

    private int cellSpacing = 100; // distance between the cells

    public void CreateTable(int width, int height, Vector3 Position, double Rotate)
    {
        // Creating a table background
        GameObject gameObject = CopyPref(cellBackgroundTransform.gameObject);
        gameObject.transform.SetParent(canvas);
        gameObject.transform.position = Position;

        // Creating a table

        for (int i = 0; i < height; i++)
        {
            ourCell.RemoveAll(item => item == null);
            textsLinePower.RemoveAll(item => item == null);
            int startNumberCell = ourCell.Count;

            for (int j = 0; j < width; j++)
            {
                // copy of the cell
                GameObject cellBackground = CopyPref(cellContentTransform.gameObject);
                
                // save our cell
                ourCell.Add(cellBackground);

                cellBackground.transform.localScale = new Vector3(1, 1, 1); // change cell size
                cellBackground.transform.SetParent(gameObject.transform);

                cellBackground.transform.position = gameObject.transform.position + new Vector3(j * (100 + cellSpacing), -i * (30 + cellSpacing), 0); // changing position of the cell


                if (j == (width - 1))
                {
                    Text lineText = Instantiate(ourLinePower);
                    lineText.transform.SetParent(gameObject.transform);
                    lineText.transform.position = gameObject.transform.position + new Vector3((j + 1) * (100 + cellSpacing), -i * (30 + cellSpacing), 0);

                    PowerForLine powerForLine = lineText.GetComponent<PowerForLine>();
                    powerForLine.StartNumberCell = startNumberCell;
                    powerForLine.EndNumberCell = startNumberCell + width;
                    textsLinePower.Add(lineText);
                }
            }

        }

        gameObject.transform.localScale = new Vector3(1, 1, 1);
        rectTransfrom = gameObject.GetComponent<RectTransform>();
        rectTransfrom.sizeDelta = new Vector2(((width - 1) * 200) + 170, ((height - 1) * 130) + 75);
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
