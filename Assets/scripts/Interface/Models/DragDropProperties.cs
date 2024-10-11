using UnityEngine;
public class DragDropProperties
{
    public RectTransform RecetTransform { get; set; }

    public Vector2 StartPos { get; set; } // the starting position, for this position, the item will be returned if it does not get into the form

    public GameObject Form { get; set; }// the general variable to which we will assign the physical location (into which the item is inserted)

    public bool PosNow { get; set; }

    public bool FormIsFull { get; set; }

    public bool DidTheFormSearchWork { get; set; }

    public DragDropProperties()
    {
        DidTheFormSearchWork = false;
    }
}

