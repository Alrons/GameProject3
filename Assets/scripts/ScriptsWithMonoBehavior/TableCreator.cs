using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableCreator : MonoBehaviour
{
    public Transform Canvas;
    public Transform cellBackgroundTransform; // “рансформ объекта фона €чейки
    public Transform cellContentTransform; // “рансформ объекта содержимого €чейки
    public Text OurLinePower;
    private RectTransform rectTransfrom;

    // save our Cell, number in list will be count number this cell
    public List<GameObject> ourCell = new List<GameObject>();
    public List<Text> textsLinePower = new List<Text>();

    private int cellSpacing = 100; // –ассто€ние между €чейками

    public void CreateTable(int width, int height, Vector3 Position, double Rotate)
    {
        // —оздаем фон таблицы
        GameObject gameObject = CopyPref(cellBackgroundTransform.gameObject);
        gameObject.transform.SetParent(Canvas);
        gameObject.transform.position = Position;

        // —оздаем таблицу
        for (int i = 0; i < height; i++)
        {
            // for the future
            //int startNumberCell = ourCell.Count;

            for (int j = 0; j < width; j++)
            {
                // —оздаем копию €чейки
                GameObject cellBackground = CopyPref(cellContentTransform.gameObject);

                // сохран€ем нашу €чейку, ее пор€док создани€ будет определ€ть ее номер
                ourCell.Add(cellBackground);

                cellBackground.transform.localScale = new Vector3(1, 1, 1); // измен€ем размер €чейки
                cellBackground.transform.SetParent(gameObject.transform);

                cellBackground.transform.position = gameObject.transform.position + new Vector3(j * (100 + cellSpacing), -i * (30 + cellSpacing), 0); // измен€ем позицию €чейки

                if (j == (width - 1))
                {
                    Text lineText = Instantiate(OurLinePower);
                    lineText.transform.SetParent(gameObject.transform);
                    lineText.transform.position = gameObject.transform.position + new Vector3((j + 1) * (100 + cellSpacing), -i * (30 + cellSpacing), 0);
                    
                    
                    // for the future

                    //PowerForLine powerForLine = lineText.GetComponent<PowerForLine>();
                    //powerForLine.StartNumberCell = startNumberCell;
                    //powerForLine.EndNumberCell = startNumberCell + width;
                    //textsLinePower.Add(lineText);
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
