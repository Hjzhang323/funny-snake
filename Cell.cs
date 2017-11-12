using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnySnake
{
    public enum CellKind
    {
        Normal,
        Hinder,
        SerpentHead,
        SerpentBody,
        SerpentTail,
        Food
    }

    public enum RelateDirection
    {
        None,
        Center,
        Left,
        Right,
        Top,
        Bottom
    }

    public class Cell
    {
        private int _row;
        private int _col;
        private Color _normalColor = Color.White;
        private Color _hinderColor = Color.Red;
        private Color _sHeadColor = Color.Green;
        private Color _sBodyColor = Color.FromArgb(100, 234, 100);
        private Color _sTailColor = Color.FromArgb(208, 208, 208);
        private Color _foodColor = Color.Yellow;
        private Color _objColor;
        private Color _bdrColor = Color.LightGray;
        private CellKind _kind;

        public int Row
        {
            get
            {
                return _row;
            }

            set
            {
                _row = value;
            }
        }

        public int Col
        {
            get
            {
                return _col;
            }

            set
            {
                _col = value;
            }
        }

        public Color NormalColor
        {
            get
            {
                return _normalColor;
            }

            set
            {
                _normalColor = value;
            }
        }

        public Color HinderColor
        {
            get
            {
                return _hinderColor;
            }

            set
            {
                _hinderColor = value;
            }
        }

        public Color SHeadColor
        {
            get
            {
                return _sHeadColor;
            }

            set
            {
                _sHeadColor = value;
            }
        }

        public Color SBodyColor
        {
            get
            {
                return _sBodyColor;
            }

            set
            {
                _sBodyColor = value;
            }
        }

        public Color STailColor
        {
            get
            {
                return _sTailColor;
            }

            set
            {
                _sTailColor = value;
            }
        }

        public Color FoodColor
        {
            get
            {
                return _foodColor;
            }

            set
            {
                _foodColor = value;
            }
        }

        public Color ObjColor
        {
            get
            {
                return _objColor;
            }
        }

        public CellKind Kind
        {
            get
            {
                return _kind;
            }

            set
            {
                _kind = value;
                UpdateBgColor();
            }
        }

        public Color BdrColor
        {
            get
            {
                return _bdrColor;
            }

            set
            {
                _bdrColor = value;
            }
        }

        public bool IsNormal()
        {
            return Kind == CellKind.Normal;
        }

        public bool IsHinder()
        {
            return Kind == CellKind.Hinder;
        }

        public bool IsSerpentHead()
        {
            return Kind == CellKind.SerpentHead;
        }

        public bool IsSerpentBody()
        {
            return Kind == CellKind.SerpentBody;
        }

        public bool IsSerpentTail()
        {
            return Kind == CellKind.SerpentTail;
        }

        public bool IsFood()
        {
            return Kind == CellKind.Food;
        }

        public bool IsPass()
        {
            return IsNormal() || IsFood();
        }

        public bool IsSamePostion(Cell cell)
        {
            return (this.Row == cell.Row && this.Col == cell.Col);
        }

        private void UpdateBgColor()
        {
            switch (_kind)
            {
                case CellKind.Normal:
                    _objColor = NormalColor;
                    break;
                case CellKind.Hinder:
                    _objColor = HinderColor;
                    break;
                case CellKind.SerpentHead:
                    _objColor = SHeadColor;
                    break;
                case CellKind.SerpentBody:
                    _objColor = SBodyColor;
                    break;
                case CellKind.SerpentTail:
                    _objColor = STailColor;
                    break;
                case CellKind.Food:
                    _objColor = FoodColor;
                    break;
            }

        }

        public Cell(int _row, int _col, CellKind _kind)
        {
            SetData(_row, _col, _kind);
        }

        public void SetData(int _row, int _col, CellKind _kind)
        {
            this.Row = _row;
            this.Col = _col;
            this.Kind = _kind;
        }

        public void SetColor(Color normalColor, Color hinderColor,
            Color sHeadColor, Color sBodyColor, Color sTailColor,
            Color foodColor, Color bdrColor)
        {
            this.NormalColor = normalColor;
            this.HinderColor = hinderColor;
            this.SHeadColor = sHeadColor;
            this.SBodyColor = sBodyColor;
            this.STailColor = sTailColor;
            this.FoodColor = foodColor;
            this.BdrColor = bdrColor;
            UpdateBgColor();
        }

        public Cell(Cell src)
        {
            SetData(src.Row, src.Col, src.Kind);
            SetColor(src.NormalColor, src.HinderColor, src.SHeadColor, src.SBodyColor, src.STailColor, src.FoodColor, src.BdrColor);
        }

        public void Draw2(Graphics g, Rectangle rect)
        {
            Brush brObj = new SolidBrush(ObjColor);
            Rectangle rcObj = rect;
            g.FillRectangle(brObj, rcObj);
            brObj.Dispose();
        }

        private void DrawSimple(Graphics g, Rectangle rect)
        {
            Brush brBack = new SolidBrush(ObjColor);
            g.FillRectangle(brBack, rect);
            Pen pen = new Pen(BdrColor, 1);
            if (Global.DrawCellGridLine)
                g.DrawRectangle(pen, rect);
            brBack.Dispose();
            pen.Dispose();
        }

        private Rectangle GetRelationRect(Rectangle rect, int margin, RelateDirection direction)
        {
            if (!Global.DrawCellMargin)
                return rect;
            Rectangle rcDirection = Rectangle.Empty;
            int x = 0, y = 0, w = 0, h = 0;
            switch (direction)
            {
                case RelateDirection.None:
                    return rcDirection;
                case RelateDirection.Center:
                    x = rect.Left + margin;
                    y = rect.Top + margin;
                    w = rect.Width - margin * 2;
                    h = rect.Height - margin * 2;
                    break;
                case RelateDirection.Left:
                    x = rect.Left;
                    y = rect.Top + margin;
                    w = margin;
                    h = rect.Height - margin * 2;
                    break;
                case RelateDirection.Right:
                    x = rect.Right - margin;
                    y = rect.Top + margin;
                    w = margin;
                    h = rect.Height - margin * 2;
                    break;
                case RelateDirection.Top:
                    x = rect.Left + margin;
                    y = rect.Top;
                    w = rect.Width - margin * 2;
                    h = margin;
                    break;
                case RelateDirection.Bottom:
                    x = rect.Left + margin;
                    y = rect.Bottom - margin;
                    w = rect.Width - margin * 2;
                    h = margin;
                    break;
            }
            rcDirection = new Rectangle(x, y, w, h);
            return rcDirection;
        }

        public void Draw(Graphics g, Rectangle rect, int margin, int idx, RelateDirection before, RelateDirection after)
        {
            if (IsNormal() || IsHinder())
            {
                DrawSimple(g, rect);
                return;
            }

            Brush brObj = new SolidBrush(ObjColor);
            Brush brBack = new SolidBrush(NormalColor);
            Pen pen = new Pen(BdrColor, 1);
            if (!Global.DrawCellMargin)
            {
                g.FillRectangle(brObj, rect);
                if (Global.DrawCellGridLine)
                    g.DrawRectangle(pen, rect);
                if (idx != -1 && Global.DisplaySerpentItemNumber)
                    g.DrawString(idx.ToString(), SystemFonts.DefaultFont, brBack, rect.Left + 2, rect.Top + 2);
            }
            else
            {
                g.FillRectangle(brBack, rect);
                if (Global.DrawCellGridLine)
                    g.DrawRectangle(pen, rect);

                Rectangle rcCenter = new Rectangle(rect.Left + margin, rect.Top + margin, rect.Width - margin * 2, rect.Height - margin * 2);

                g.FillRectangle(brObj, rcCenter);
                if (idx != -1 && Global.DisplaySerpentItemNumber)
                    g.DrawString(idx.ToString(), SystemFonts.DefaultFont, Brushes.Black, rcCenter.Left + 2, rcCenter.Top + 2);

                if (before != RelateDirection.None)
                {
                    Rectangle rcBefore = GetRelationRect(rect, margin, before);
                    g.FillRectangle(brObj, rcBefore);
                }

                if (after != RelateDirection.None)
                {
                    Rectangle rcAfter = GetRelationRect(rect, margin, after);
                    g.FillRectangle(brObj, rcAfter);
                }
            }
            brObj.Dispose();
            brBack.Dispose();
            pen.Dispose();
        }

        public void Draw(Graphics g, Rectangle rect)
        {
            if (IsNormal())
            {
                Brush brBack = new SolidBrush(NormalColor);
                g.FillRectangle(brBack, rect);
                Pen pen = new Pen(BdrColor, 1);
                g.DrawRectangle(pen, rect);
                brBack.Dispose();
                pen.Dispose();
            }
            if (!IsNormal())
            {
                Rectangle rcObj = rect;
                Brush brObj = new SolidBrush(ObjColor);
                g.FillRectangle(brObj, rcObj);
                brObj.Dispose();
            }
        }

        public void Draw3(Graphics g, Rectangle rect)
        {
            if (IsNormal() || IsSerpentHead())
            {
                Brush brBack = new SolidBrush(NormalColor);
                g.FillRectangle(brBack, rect);
                Pen pen = new Pen(BdrColor, 1);
                g.DrawRectangle(pen, rect);
                brBack.Dispose();
                pen.Dispose();
            }
            if (!IsNormal())
            {
                Brush brObj = new SolidBrush(ObjColor);
                Rectangle rcObj = rect;
                if (Kind == CellKind.SerpentHead)
                {
                    GraphicsPath path = GetRoundRect(rcObj, rcObj.Width / 4);
                    //g.SmoothingMode = SmoothingMode.HighQuality;
                    g.FillPath(brObj, path);
                }
                else
                {
                    g.FillRectangle(brObj, rcObj);
                }

                brObj.Dispose();
            }
        }

        private GraphicsPath GetRoundRect(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();

            path.StartFigure();

            // 左上の角丸
            path.AddArc(rect.Left, rect.Top,
                radius * 2, radius * 2,
                180, 90);
            // 上の線
            path.AddLine(rect.Left + radius, rect.Top,
                rect.Right - radius, rect.Top);
            // 右上の角丸
            path.AddArc(rect.Right - radius * 2, rect.Top,
                radius * 2, radius * 2,
                270, 90);
            // 右の線
            path.AddLine(rect.Right, rect.Top + radius,
                rect.Right, rect.Bottom - radius);
            // 右下の角丸
            path.AddArc(rect.Right - radius * 2, rect.Bottom - radius * 2,
                radius * 2, radius * 2,
                0, 90);
            // 下の線
            path.AddLine(rect.Right - radius, rect.Bottom,
                rect.Left + radius, rect.Bottom);
            // 左下の角丸
            path.AddArc(rect.Left, rect.Bottom - radius * 2,
                radius * 2, radius * 2,
                90, 90);
            // 左の線
            path.AddLine(rect.Left, rect.Bottom - radius,
                rect.Left, rect.Top + radius);

            path.CloseFigure();

            return path;
        }
    }
}
