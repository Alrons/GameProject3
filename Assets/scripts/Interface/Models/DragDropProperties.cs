using UnityEngine;
using System.Collections.Generic;
public class DragDropProperties
{
    public RectTransform RecetTransform { get; set; }

    public Vector2 StartPos { get; set; } // the starting position, for this position, the item will be returned if it does not get into the form

    public List<CellNumberModel> Form { get; set; }

    public bool PosNow { get; set; }

    public bool FormIsFull { get; set; }

    public bool DidTheFormSearchWork { get; set; }

    public bool IsOnEndDrag { get; set; }

    public DragDropProperties()
    {
        Form = new List<CellNumberModel>();
        DidTheFormSearchWork = false;
        IsOnEndDrag = false;
    }
}

