using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FunnySnake
{
    public class Map
    {
        private List<List<Cell>> _cells = null;
        private int _cellWidth = 0;
        private int _cellMargin = 4;
        private Cell _cellFood = null;
        private Graphics _graphic = null;
        private Bitmap _bitmap = null;
        private Rectangle _bounds = Rectangle.Empty;
        private Rectangle _mapSide = Rectangle.Empty;

        public int RowCount { get; set; }
        public int ColCount { get; set; }
        public int CellCount { get; set; }
        public Color OutSideColor { get; set; } = Color.DarkGray;
        public Color BorderColor { get; set; } = Color.Black;

        public int CellWidth
        {
            get
            {
                return _cellWidth;
            }

            set
            {
                _cellWidth = value;
            }
        }

        public int CellMargin
        {
            get
            {
                return _cellMargin;
            }

            set
            {
                _cellMargin = value;
            }
        }

        public Cell CellFood
        {
            get
            {
                return _cellFood;
            }

            set
            {
                _cellFood = value;
            }
        }

        private void CalcMapSize(Control control)
        {
            int w = control.ClientSize.Width;
            int h = control.ClientSize.Height;

            int w1 = (w - 2) / ColCount;
            int w2 = (h - 2) / RowCount;
            CellWidth = Math.Min(w1, w2);
            if (CellWidth <= 4)
                throw new Exception("グリッドの幅が小さすぎる！");

            CellMargin = CellWidth / 8;

            _graphic = control.CreateGraphics();
            _bitmap = new Bitmap(w, h);

            int mw = CellWidth * ColCount;
            int mh = CellWidth * RowCount;
            _mapSide = new Rectangle((w - mw) / 2, (h - mh) / 2, mw, mh);
            _bounds = new Rectangle(0, 0, w, h);
        }

        public Map(int rowCount, int colCount, Control control)
        {
            this.RowCount = rowCount;
            this.ColCount = colCount;
            this.CellCount = RowCount * ColCount;
            CalcMapSize(control);

            int cellWidth = _mapSide.Width / ColCount;

            _cells = new List<List<Cell>>();
            for (int row = 0; row < RowCount; row++)
            {
                List<Cell> rowCells = new List<Cell>();
                for (int col = 0; col < ColCount; col++)
                {
                    Cell cell = new Cell(row, col, CellKind.Normal);
                    //cell.SetColor(Color.Black, Color.Red, Color.Red, Color.White, Color.White, Color.Chocolate, Color.Black);
                    rowCells.Add(cell);
                }
                _cells.Add(rowCells);
            }
        }

        private void ResetCells()
        {
            for (int row = 0; row < RowCount; row++)
            {
                for (int col = 0; col < ColCount; col++)
                {
                    Cell cell = _cells[row][col];
                    cell.Kind = CellKind.Normal;
                }
            }

        }

        public void ResetMap(bool redraw = false)
        {
            ResetCells();
            if (redraw)
                RedrawMap();
        }

        public void RedrawMap()
        {
            Graphics g = Graphics.FromImage(_bitmap);
            g.Clear(OutSideColor);

            for (int row = 0; row < RowCount; row++)
            {
                for (int col = 0; col < ColCount; col++)
                {
                    _drawMapCell(g, row, col);
                }
            }
            Pen pen = new Pen(BorderColor);
            Rectangle frame = _mapSide;
            frame.Inflate(2, 2);
            frame.Offset(-1, -1);
            g.DrawRectangle(pen, frame);
            pen.Dispose();
            g.Dispose();
            _graphic.DrawImage(_bitmap, 0, 0);

        }

        public void DrawSerpentCells(Serpent serpent)
        {
            for (int i = 0; i < serpent.Body.Count; i++)
            {
                Cell cell = serpent.Body[i];
                Rectangle rect = GetCellRect(cell.Row, cell.Col);
                RelateDirection before = serpent.GetCellRelation(i, -1);
                RelateDirection after = serpent.GetCellRelation(i, 1);
                cell.Draw(_graphic, rect, CellMargin, i, before, after);
            }
        }

        public void DrawSerpentCells2(Serpent serpent)
        {
            Graphics g = Graphics.FromImage(_bitmap);

            for (int i = 0; i < serpent.Body.Count; i++)
            {
                Cell cell = serpent.Body[i];
                Rectangle rect = GetCellRect(cell.Row, cell.Col);
                RelateDirection before = serpent.GetCellRelation(i, -1);
                RelateDirection after = serpent.GetCellRelation(i, 1);
                cell.Draw(g, rect, CellMargin, i, before, after);
            }
            g.Dispose();

            _graphic.DrawImage(_bitmap, 0, 0);
        }

        public void DrawSerpentCell(Serpent serpent, int idx)
        {
            Cell cell = serpent.GetCell(idx);
            Rectangle rect = GetCellRect(cell.Row, cell.Col);
            RelateDirection before = serpent.GetCellRelation(idx, -1);
            RelateDirection after = serpent.GetCellRelation(idx, 1);
            cell.Draw(_graphic, rect, CellMargin, idx, before, after);
        }

        public void RedrawMapCell(int row, int col)
        {
            _drawMapCell(_graphic, row, col);
        }

        private void _drawMapCell(Graphics g, int row, int col)
        {
            Cell cell = _cells[row][col];
            Rectangle rect = GetCellRect(row, col);
            cell.Draw(g, rect, CellMargin, -1, RelateDirection.None, RelateDirection.None);

        }

        public void ResizeMap(Control control)
        {
            CalcMapSize(control);
            RedrawMap();
        }

        public Rectangle GetCellRect(int row, int col)
        {
            Rectangle rect = Rectangle.Empty;
            if (_cells.Count == 0)
                return rect;
            rect = new Rectangle(_mapSide.Left + CellWidth * col, _mapSide.Top + CellWidth * row, CellWidth, CellWidth);
            return rect;
        }

        public Cell GetCell(int row, int col)
        {
            if (row < 0 || col < 0 || row > RowCount - 1 || col > ColCount - 1)
                return null;
            return _cells[row][col];
        }

        public bool HasNormalCell(Serpent serpent)
        {
            int row, col;
            for (row = 0; row < RowCount; row++)
            {
                for (col = 0; col < ColCount; col++)
                {
                    Cell cell = GetCell(row, col);
                    if (cell.IsNormal() && !serpent.InBody(cell))
                        return true;
                }
            }
            return false;

        }

        public bool SetFood(Serpent serpent)
        {
            if (!HasNormalCell(serpent))
                return false;

            int x, y;
            Random r = new Random();
            while (true)
            {
                x = r.Next(0, RowCount);
                y = r.Next(0, ColCount);
                Cell cell = GetCell(x, y);
                if (cell.IsNormal() && !serpent.InBody(cell))
                {
                    break;
                }
            }
            _cellFood = _cells[x][y];
            _cellFood.Kind = CellKind.Food;
            RedrawMapCell(x, y);
            return true;
        }
    }
}
