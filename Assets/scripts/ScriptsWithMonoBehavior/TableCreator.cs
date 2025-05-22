using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.scripts.Interface.Models;

public class TableCreator : MonoBehaviour
{
    public Transform canvas;
    public Transform cellBackgroundTransform;
    public Transform cellContentTransform;
    public Transform tableContainer;
    public Text ourLinePower;

    private int totalTableCount;
    private int totalCellCount;

    // save our Cell, number in list will be count number this cell
    public List<Text> textsLinePower = new List<Text>();
    public HashSet<CellNumberModel> hashSetCellNumber = new HashSet<CellNumberModel>();

    public void CreateTable(TableDto tableDto)
    {
        totalTableCount += 1;
        
        GameObject table = Instantiate(tableContainer.gameObject, canvas);
        table.transform.localScale = Vector3.one;


        table.transform.localPosition = new Vector3((float)tableDto.PosX, (float)tableDto.PosY);
        table.transform.localRotation = Quaternion.Euler(0, 0, (float)tableDto.Rotate);

        RectTransform tableRect = table.GetComponent<RectTransform>();

        GridLayoutGroup grid = table.GetComponent<GridLayoutGroup>();
        ContentSizeFitter fitter = table.GetComponent<ContentSizeFitter>();

        grid.cellSize = new Vector2(tableDto.CellSize.Width, tableDto.CellSize.Height);
        grid.spacing = new Vector2(tableDto.CellSpacing.Horizontal, tableDto.CellSpacing.Vertical);
        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.constraintCount = tableDto.Width + 1;
        
        fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        
        int indexCell = 0;
        for (int i = 0; i < tableDto.Height; i++)
        {
            int startNumberCell = hashSetCellNumber.Count;

            for (int j = 0; j < tableDto.Width; j++)
            {
                totalCellCount += 1;

                GameObject cell = CopyPref(cellContentTransform.gameObject, tableDto.CellSize.Width, tableDto.CellSize.Height);
                cell.transform.SetParent(table.transform, false);
                cell.transform.localScale = Vector3.one;

                hashSetCellNumber.Add(new CellNumberModel(cell, totalCellCount, totalTableCount, tableDto.Cells[indexCell].Group, tableDto.CellSize));

                if (j == tableDto.Width - 1)
                {
                    Text lineText = Instantiate(ourLinePower);
                    lineText.transform.SetParent(table.transform, false);
                    lineText.transform.localScale = Vector3.one;
                    PowerForLine powerForLine = lineText.GetComponent<PowerForLine>();
                    powerForLine.StartNumberCell = startNumberCell;
                    
                    powerForLine.EndNumberCell = startNumberCell + tableDto.Width;
                    textsLinePower.Add(lineText);
                }
                cell.transform.localScale = Vector3.one;
                indexCell++;
            }
        }
        textsLinePower.RemoveAll(text => text == null);
    }

    //For copy
    public GameObject CopyPref(GameObject box, float targetWidth, float targetHeight)
    {
        var spawn = Instantiate(box);
        spawn.transform.localScale = Vector3.one;

        PolygonCollider2D poly = spawn.GetComponent<PolygonCollider2D>();
        RectTransform rect = spawn.GetComponent<RectTransform>();

        if (poly != null && rect != null)
        {
            Vector2[] originalPoints = poly.points;

            Bounds originalBounds = poly.bounds;
            float originalWidth = originalBounds.size.x;
            float originalHeight = originalBounds.size.y;

            float scaleX = targetWidth / originalWidth;
            float scaleY = targetHeight / originalHeight;

            Vector2[] scaledPoints = new Vector2[originalPoints.Length];
            for (int i = 0; i < originalPoints.Length; i++)
            {
                scaledPoints[i] = new Vector2(
                    originalPoints[i].x * scaleX,
                    originalPoints[i].y * scaleY
                );
            }

            poly.points = scaledPoints;
        }

        return spawn;
    }
}
