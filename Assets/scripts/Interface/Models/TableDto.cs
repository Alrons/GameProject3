
using System.Collections.Generic;


namespace Assets.scripts.Interface.Models
{
    public class TableDto
    {
        public int Id { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public float PosX { get; set; }
        public float PosY { get; set; }
        public float Rotate { get; set; }

        public CellSizeDto CellSize { get; set; }
        public CellSpacingDto CellSpacing { get; set; }

        public List<CellDto> Cells { get; set; } = new();
    }
}
