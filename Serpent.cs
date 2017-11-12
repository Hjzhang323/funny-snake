using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FunnySnake
{
    public enum Direction
    {
        Left,
        Up,
        Right,
        Down
    }

    public class Serpent
    {
        private List<Cell> _body = new List<Cell>();
        private int _maxCount = 15;
        private int _minCount = 5;
        private Direction _direction = Direction.Right;
        public Map MapPtr { get; set; }
        private Random _rndInt = null;

        public int MaxCount
        {
            get
            {
                return _maxCount;
            }

            set
            {
                _maxCount = value;
            }
        }

        public int MinCount
        {
            get
            {
                return _minCount;
            }

            set
            {
                _minCount = value;
            }
        }

        public List<Cell> Body
        {
            get
            {
                return _body;
            }

            set
            {
                _body = value;
            }
        }

        public Direction Direction
        {
            get
            {
                return _direction;
            }

            set
            {
                _direction = value;
            }
        }

        public Serpent(int mincount, int maxcount, Panel pnl, Map map)
        {
            MinCount = mincount;
            MaxCount = maxcount;
            MapPtr = map;
            InitializeSerpent();
            _rndInt = new Random(DateTime.Now.Millisecond);
        }

        public Serpent(Serpent src)
        {
            MinCount = src.MinCount;
            MaxCount = src.MaxCount;
            MapPtr = src.MapPtr;
            _rndInt = new Random(DateTime.Now.Millisecond);
            foreach (Cell cell in src.Body)
            {
                this.Body.Add(new Cell(cell));
            }
        }

        public void InitializeSerpent()
        {
            Body.Clear();
            for (int i = 0; i < MinCount; i++)
            {
                int row = i / MapPtr.ColCount;
                int col = i % MapPtr.ColCount;
                Cell cell = new Cell(row, col, CellKind.SerpentBody);
                Body.Insert(0, cell);
            }
            GetHead().Kind = CellKind.SerpentHead;
            GetTail().Kind = CellKind.SerpentTail;
        }

        public void InsertCell(Cell cell)
        {
            GetHead().Kind = CellKind.SerpentBody;
            Cell head = new Cell(cell);
            head.Kind = CellKind.SerpentHead;
            Body.Insert(0, head);
        }

        public void RemoveTail()
        {
            Body.RemoveAt(Body.Count - 1);
            GetTail().Kind = CellKind.SerpentTail;
        }

        public RelateDirection GetCellRelation(int idx, int offset)
        {
            RelateDirection direction = RelateDirection.None;

            int r_idx = idx + offset;
            Cell rela = GetCell(r_idx);
            if (rela != null)
            {
                Cell cell = GetCell(idx);
                if (rela.Row > cell.Row)
                    direction = RelateDirection.Bottom;
                else if (rela.Row < cell.Row)
                    direction = RelateDirection.Top;
                else if (rela.Col > cell.Col)
                    direction = RelateDirection.Right;
                else if (rela.Col < cell.Col)
                    direction = RelateDirection.Left;
            }
            return direction;            
        }

        public bool IsMax()
        {
            return Body.Count == MaxCount;
        }

        public Cell GetCell(int idx)
        {
            if (idx < 0 || idx > _body.Count - 1)
                return null;
            return _body[idx];
        }

        public Cell GetHead()
        {
            return Body[0];
        }

        public Cell GetTail()
        {
            return Body[Body.Count - 1];
        }

        public string PrintAllCells()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\r\n");
            for (int row = 0; row < MapPtr.RowCount; row++)
            {
                for (int col = 0; col < MapPtr.ColCount; col++)
                {
                    Cell cell = MapPtr.GetCell(row, col);
                    if (InBody(cell))
                    {
                        if (cell.IsSamePostion(GetHead()))
                            sb.Append(" @");
                        else if (cell.IsSamePostion(GetTail()))
                            sb.Append(" o");
                        else
                            sb.Append(" #");
                    }
                    else if (cell.IsFood())
                        sb.Append(" F");
                    else if (cell.IsHinder())
                        sb.Append(" H");
                    else
                        sb.Append(" _");
                }
                sb.Append("\r\n");
            }
            Console.Write(sb.ToString());
            return sb.ToString();
        }

        public Cell MoveToNext()
        {
            Cell head = GetHead();
            List<Cell> cells = GetWidelyMovableCells(head.Row, head.Col);
            if (cells.Count == 0)
                return null;
            int idx = _rndInt.Next(0, cells.Count);
            return cells[idx];
        }

        public Cell MoveToNext(Cell goal)
        {
            Cell head = GetHead();
            List<Cell> cells = GetGoalMovableCells(head, goal);
            if (cells.Count == 0)
                return null;

            List<int> dists = new List<int>();
            foreach (Cell cell in cells)
            {
                int dis = GetCellsDistance(cell, goal);
                dists.Add(dis);
            }
            int idx = 0;
            for (int i = 0; i < dists.Count; i++)
            {
                if (dists[idx] > dists[i])
                    idx = i;
            }

            return cells[idx];
        }

        public List<Cell> GetMovableCells(int row, int col)
        {
            List<Cell> cells = new List<Cell>();
            // left
            Cell left = MapPtr.GetCell(row, col - 1);
            if (IsMovedCellValid(left))
                cells.Add(left);

            // right
            Cell right = MapPtr.GetCell(row, col + 1);
            if (IsMovedCellValid(right))
                cells.Add(right);

            // up
            Cell up = MapPtr.GetCell(row - 1, col);
            if (IsMovedCellValid(up))
                cells.Add(up);

            // down
            Cell down = MapPtr.GetCell(row + 1, col);
            if (IsMovedCellValid(down))
                cells.Add(down);

            return cells;
        }

        public int GetMovableCellsCountStep(int row, int col, int step)
        {
            int count = 0;
            List<Cell> cells = GetMovableCells(row, col);
            count = cells.Count;
            if (step > 0)
            {
                foreach (Cell cell in cells)
                {
                    int n = GetMovableCellsCountStep(cell.Row, cell.Col, step - 1);
                    count += n;
                }
            }
            return count;
        }

        public List<Cell> GetWidelyMovableCells(int row, int col)
        {
            List<Cell> cells = new List<Cell>();

            Cell left, right, up, down;
            int cntLeft = 0;
            int cntRight = 0;
            int cntUp = 0;
            int cntDown = 0;
            int step = 5;
            // left
            left = MapPtr.GetCell(row, col - 1);
            if (IsMovedCellValid(left))
            {
                cntLeft = GetMovableCellsCountStep(left.Row, left.Col, step);
            }
            // right
            right = MapPtr.GetCell(row, col + 1);
            if (IsMovedCellValid(right))
                cntRight = GetMovableCellsCountStep(right.Row, right.Col, step);

            // up
            up = MapPtr.GetCell(row - 1, col);
            if (IsMovedCellValid(up))
                cntUp = GetMovableCellsCountStep(up.Row, up.Col, step);

            // down
            down = MapPtr.GetCell(row + 1, col);
            if (IsMovedCellValid(down))
                cntDown = GetMovableCellsCountStep(down.Row, down.Col, step);

            int nMax = Math.Max(cntLeft, cntRight);
            nMax = Math.Max(nMax, cntUp);
            nMax = Math.Max(nMax, cntDown);

            if (cntLeft == nMax)
                cells.Add(left);
            if (cntRight == nMax)
                cells.Add(right);
            if (cntUp == nMax)
                cells.Add(up);
            if (cntDown == nMax)
                cells.Add(down);

            return cells;
        }

        public List<Cell> GetGoalMovableCells(Cell cur, Cell goal)
        {
            List<Cell> cells = new List<Cell>();

            Cell left, right, up, down;
            int cntLeft = 0;
            int cntRight = 0;
            int cntUp = 0;
            int cntDown = 0;
            int step = 5;
            int disLeft = int.MaxValue;
            int disRight = int.MaxValue;
            int disUp = int.MaxValue;
            int disDown = int.MaxValue;
            // left
            left = MapPtr.GetCell(cur.Row, cur.Col - 1);
            if (IsMovedCellValid(left))
            {
                cntLeft = GetMovableCellsCountStep(left.Row, left.Col, step);
                disLeft = GetCellsDistance(left, goal);
            }

            // right
            right = MapPtr.GetCell(cur.Row, cur.Col + 1);
            if (IsMovedCellValid(right))
            {
                cntRight = GetMovableCellsCountStep(right.Row, right.Col, step);
                disRight = GetCellsDistance(right, goal);
            }

            // up
            up = MapPtr.GetCell(cur.Row - 1, cur.Col);
            if (IsMovedCellValid(up))
            {
                cntUp = GetMovableCellsCountStep(up.Row, up.Col, step);
                disUp = GetCellsDistance(up, goal);
            }

            // down
            down = MapPtr.GetCell(cur.Row + 1, cur.Col);
            if (IsMovedCellValid(down))
            {
                cntDown = GetMovableCellsCountStep(down.Row, down.Col, step);
                disDown = GetCellsDistance(down, goal);
            }

            int nMax = Math.Max(cntLeft, cntRight);
            nMax = Math.Max(nMax, cntUp);
            nMax = Math.Max(nMax, cntDown);
            if (nMax == 0)
                return cells;

            int nMin = Math.Min(disLeft, disRight);
            nMin = Math.Min(nMin, disUp);
            nMin = Math.Min(nMin, disDown);
            if (disLeft == nMin && cntLeft == nMax)
                cells.Add(left);
            if (disRight == nMin && cntRight == nMax)
                cells.Add(right);
            if (disUp == nMin && cntUp == nMax)
                cells.Add(up);
            if (disDown == nMin && cntDown == nMax)
                cells.Add(down);

            if (cells.Count == 0)
            {
                if (disLeft == nMin)
                    cells.Add(left);
                if (disRight == nMin)
                    cells.Add(right);
                if (disUp == nMin)
                    cells.Add(up);
                if (disDown == nMin)
                    cells.Add(down);
            }

            if (cells.Count == 0)
            {
                if (cntLeft == nMax)
                    cells.Add(left);
                if (cntRight == nMax)
                    cells.Add(right);
                if (cntUp == nMax)
                    cells.Add(up);
                if (cntDown == nMax)
                    cells.Add(down);
            }
            return cells;
        }

        public bool IsMovedCellValid(Cell cell)
        {
            return (cell != null && cell.IsPass() && !InBodyExceptTail(cell));
        }

        public bool IsCellValid(Cell cell)
        {
            return (cell != null && cell.IsPass() && !InBody(cell));
        }

        public bool InBodyExceptTail(Cell cell)
        {
            for (int i = 0; i < Body.Count - 1; i++)
            {
                Cell bd = Body[i];
                if (cell.Row == bd.Row && cell.Col == bd.Col)
                    return true;
            }
            return false;
        }

        public bool InBody(Cell cell)
        {
            foreach (Cell bd in Body)
            {
                if (cell.Row == bd.Row && cell.Col == bd.Col)
                    return true;
            }
            return false;
        }

        ///////////////////// Virtual move function ////////////////////////////
        private bool[,] CreateAstarMap()
        {
            bool[,] mp = new bool[MapPtr.RowCount, MapPtr.ColCount];
            Cell tail = this.GetTail();
            for (int row = 0; row < MapPtr.RowCount; row++)
            {
                for (int col = 0; col < MapPtr.ColCount; col++)
                {
                    Cell cell = MapPtr.GetCell(row, col);
                    // the Tail is can PASSED!
                    mp[row, col] = IsMovedCellValid(cell);
                }
            }
            return mp;
        }

        public void VirtualMoveCell(Cell cell)
        {
            InsertCell(cell);
            if (!cell.IsFood())
            {
                RemoveTail();
            }
        }

        public void VirtualMoveByPath(List<Point> path)
        {
            foreach (Point pt in path)
            {
                Cell cell = Point2MapCell(pt);
                VirtualMoveCell(cell);
            }
        }

        private Point CellPosition(Cell cell)
        {
            return new Point(cell.Row, cell.Col);
        }

        private Cell Point2MapCell(Point pt)
        {
            return MapPtr.GetCell(pt.X, pt.Y);
        }

        private int GetCellsDistance(Cell cell1, Cell cell2)
        {
            return Math.Abs(cell1.Row - cell2.Row) + Math.Abs(cell1.Col - cell2.Col);
        }

        public List<Point> GetPath2Goal(Cell goal)
        {
            SimpleAStar.SearchParameters param = 
                new SimpleAStar.SearchParameters(CellPosition(GetHead()), CellPosition(goal), CreateAstarMap());
            SimpleAStar.PathFinder pf = new SimpleAStar.PathFinder(param);
            return pf.FindPath();
        }

        public Cell AStarMove2Tail(Cell goal)
        {
            Cell head = GetHead();

            // Get the list of movable cells
            List<Cell> nextCells = GetMovableCells(head.Row, head.Col);
            if (nextCells.Count == 0)
                return null;

            List<Cell> tails = new List<Cell>();
            // If moved cell can find path to tail, add to the tail list
            foreach (Cell cell in nextCells)
            {
                Serpent vsp = new Serpent(this);
                vsp.VirtualMoveCell(cell);
                Cell tail = vsp.GetTail();
                if (vsp.GetPath2Goal(tail).Count > 0)
                    tails.Add(cell);
            }

            // If not cell can find tail then check the nextcells
            if (tails.Count == 0)
            {
                tails.AddRange(nextCells);
            }

            // Get farthest cell to goal
            int dis0 = GetCellsDistance(tails[0], goal);
            Cell cellGot = tails[0];
            for (int i = 1; i < tails.Count; i++)
            {
                int disloop = GetCellsDistance(tails[i], goal);
                if (disloop > dis0)
                {
                    dis0 = disloop;
                    cellGot = tails[i];
                }
            }

            return cellGot;

        }

        public Cell AStarMove2Goal(Cell goal)
        {
            List<Point> pathGoal = null;
            List<Point> pathTail = null;

            pathGoal = GetPath2Goal(goal);
            if (pathGoal.Count > 0)
            {
                Serpent vsp = new Serpent(this);
                vsp.VirtualMoveByPath(pathGoal);
                Cell vtail = vsp.GetTail();
                pathTail = vsp.GetPath2Goal(vtail);
                if (pathTail.Count == 0)
                {
                    return AStarMove2Tail(goal);
                }
                else
                    return Point2MapCell(pathGoal[0]);
            }
            else
            {
                return AStarMove2Tail(goal);
            }
        }

    }
}
