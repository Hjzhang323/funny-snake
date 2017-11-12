using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FunnySnake
{
    public partial class frmMain : Form
    {
        public enum SerpentState
        {
            Moving,
            Pause,
            Finish,
            Error
        }

        private const int MAP_ROWCOUNT = 6;
        private const int MAP_COLCOUNT = 6;
        private const int SERPENT_MINCELLS = 6;
        private const int SERPENT_MAXCELLS = 15;
        private const int QUICKMOVING_TIME = 50;
        private const int AUTOMOVING_TIME = 50;

        Map map = null;
        Serpent serpent = null;
        Thread Work_Thread = null;
        bool IsWork = false;
        bool autoPlay = false;
        int sleeptime;

        private Stack<Keys> keyBuffer = new Stack<Keys>();

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            txtSerpentCount.Text = SERPENT_MINCELLS.ToString();
            txtMapRowCount.Text = MAP_ROWCOUNT.ToString();
            txtMapColCount.Text = MAP_COLCOUNT.ToString();
            txtAutoPlayTime.Text = AUTOMOVING_TIME.ToString();
            chkSerpentNumber.Checked = Global.DisplaySerpentItemNumber;
            chkGridLine.Checked = Global.DrawCellGridLine;
            chkCellMargin.Checked = Global.DrawCellMargin;
            CreateMap();
            btnReset.Enabled = true;
            btnStart.Enabled = true;
            btnPause.Enabled = false;
            btnStop.Enabled = false;
        }

        private bool CheckInputNumber(TextBox txt, string name, int minvalue, int maxvalue)
        {
            if (string.IsNullOrEmpty(txt.Text))
            {
                FSCommon.ShowMessageWarn(this, string.Format("{0}を設定してください", name));
                return false;
            }
            if (!FSCommon.IsNumber(txt.Text))
            {
                FSCommon.ShowMessageWarn(this, string.Format("{0}に数値を入力してください", name));
                return false;
            }
            int value = int.Parse(txt.Text);
            if (value < minvalue || value > maxvalue)
            {
                FSCommon.ShowMessageWarn(this, string.Format("{0}に数値({1}～{2})を入力してください", name, minvalue, maxvalue));
                return false;
            }
            return true;
        }

        private bool CreateMap()
        {
            if (!CheckInputNumber(txtMapRowCount, "地図の行数", 4, 50))
                return false;

            if (!CheckInputNumber(txtMapColCount, "地図の列数", 4, 50))
                return false;

            int rowCount = int.Parse(txtMapRowCount.Text);
            int colCount = int.Parse(txtMapColCount.Text);
            map = new Map(rowCount, colCount, pnlGame);
            
            pnlGame.map = map;
            map.RedrawMap();

            return true;
        }

        private bool CreateSerpent()
        {
            int maxcount = map.CellCount / 2;
            if (!CheckInputNumber(txtSerpentCount, "蛇の長さ", 4, maxcount))
                return false;
            int count = int.Parse(txtSerpentCount.Text);
            serpent = new Serpent(count, maxcount, pnlGame, map);
            pnlGame.serpent = serpent;
            map.DrawSerpentCells(serpent);
            return true;
        }

        private void SetMap()
        {
            map.ResetMap(true);
            SetSerpent();
        }

        private void SetSerpent()
        {
            int cnt = int.Parse(txtSerpentCount.Text);
            serpent = new Serpent(cnt, map.CellCount, this.pnlGame, map);
            pnlGame.serpent = serpent;
            map.DrawSerpentCells(serpent);
        }

        private void frmMain_ResizeEnd(object sender, EventArgs e)
        {
            if (map != null)
            {
                map.ResizeMap(pnlGame);
                if (serpent != null)
                    map.DrawSerpentCells(serpent);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            IsWork = false;
            btnStop.Enabled = false;
            btnPause.Enabled = false;
            btnStart.Enabled = true;
            if (CreateMap())
                CreateSerpent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            IsWork = false;
            //map.ResetMap();
            if (!CreateMap())
                return;
            if (!CreateSerpent())
                return;
            if (!CheckInputNumber(txtAutoPlayTime, "自動プレイの速度", 1, 5000))
                return;

            this.btnReset.Enabled = false;
            this.btnStart.Enabled = false;
            this.btnStop.Enabled = true;
            this.btnPause.Enabled = true;

            keyBuffer.Clear();

            autoPlay = chkAutoPlay.Checked;
            sleeptime = int.Parse(txtAutoPlayTime.Text);
            this.pnlGame.Focus();
            IsWork = true;
            Work_Thread = new Thread(new ThreadStart(Work));
            Work_Thread.IsBackground = true;
            Work_Thread.Start();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (!IsWork)
            {
                this.btnPause.Text = "一時停止";
                IsWork = true;
                Work_Thread = new Thread(new ThreadStart(Work));
                //Work_Thread.IsBackground = true;
                Work_Thread.Start();
            }
            else
            {
                this.btnPause.Text = "再開";
                IsWork = false;
            }

        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            btnReset.Enabled = true;
            btnStart.Enabled = true;
            btnPause.Enabled = false;
            btnStop.Enabled = false;
            IsWork = false;
            Work_Thread.Abort();
            Work_Thread.Join();
            SetMap();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            IsWork = false;
            if (Work_Thread != null)
            {
                Work_Thread.Abort();
                Work_Thread.Join();
            }
        }


        private void Work()
        {
            map.SetFood(serpent);
            while (IsWork)
            {
                if (autoPlay)
                {
                    SerpentState state = SerpentMove();
                    if (state == SerpentState.Moving)
                    {

                        Thread.Sleep(sleeptime);
                    }
                    else
                    {
                        IsWork = false;
                    }
                }
                else
                {
                    if (keyBuffer.Count > 0)
                    {
                        Keys key = keyBuffer.Pop();
                        SerpentState state = DoKeydown(key);
                        keyBuffer.Clear();
                        if (state == SerpentState.Moving)
                        {
                            Thread.Sleep(QUICKMOVING_TIME);
                        }
                        else
                        {
                            IsWork = false;
                        }
                    }
                }
            }
            map.ResetMap();
        }

        private SerpentState SerpentMove()
        {
            Cell celNext = serpent.AStarMove2Goal(map.CellFood);
            SerpentState index_move = CheckSerpentMove(celNext);
            if (index_move == SerpentState.Error)
            {
                GameError();
            }
            else if (index_move == SerpentState.Finish)
            {
                GameFinish();
            }
            return index_move;

        }

        private void GameError()
        {
            IsWork = false;
            this.BeginInvoke(new MethodInvoker(delegate ()
            {
                FSCommon.ShowMessageWarn(this, "ゲームオーバー！");
            }));

            txtSerpentCount.BeginInvoke(new MethodInvoker(delegate ()
            {
                btnReset.Enabled = true;
                btnStart.Enabled = true;
                btnPause.Enabled = false;
                btnStop.Enabled = false;
            }));

        }

        private void GameFinish()
        {
            IsWork = false;
            this.BeginInvoke(new MethodInvoker(delegate ()
            {
                FSCommon.ShowMessageInfo(this, "ゲーム成功！！！");
            }));

            txtSerpentCount.BeginInvoke(new MethodInvoker(delegate ()
            {
                btnReset.Enabled = true;
                btnStart.Enabled = true;
                btnPause.Enabled = false;
                btnStop.Enabled = false;
            }));

        }

        private bool CannotMoveByKeyboard(Cell cell)
        {
            if (cell == null)
                return true;
            Cell neck = serpent.GetCell(1);
            if (cell.IsSamePostion(neck))
                return true;
            return false;
        }
        private SerpentState DoKeydown(Keys key)
        {
            Cell head = serpent.GetHead();
            Cell cell = null;
            switch (key)
            {
                case Keys.Left:
                    cell = map.GetCell(head.Row, head.Col - 1);
                    if (CannotMoveByKeyboard(cell))
                        return SerpentState.Moving;
                    break;
                case Keys.Right:
                    cell = map.GetCell(head.Row, head.Col + 1);
                    if (CannotMoveByKeyboard(cell))
                        return SerpentState.Moving;
                    break;
                case Keys.Up:
                    cell = map.GetCell(head.Row - 1, head.Col);
                    if (CannotMoveByKeyboard(cell))
                        return SerpentState.Moving;
                    break;
                case Keys.Down:
                    cell = map.GetCell(head.Row + 1, head.Col);
                    if (CannotMoveByKeyboard(cell))
                        return SerpentState.Moving;
                    break;
                case Keys.F1:
                    cell = serpent.AStarMove2Goal(map.CellFood);
                    break;
                case Keys.Escape:
                    if (IsWork)
                    {
                        IsWork = false;
                        this.BeginInvoke(new MethodInvoker(delegate ()
                        {
                            btnPause.Text = "再開";
                        }));
                    }
                    return SerpentState.Pause;
            }
            SerpentState index_move = CheckSerpentMove(cell);
            if (index_move == SerpentState.Error)
            {
                GameError();
            }
            else if (index_move == SerpentState.Finish)
            {
                GameFinish();
            }
            return index_move;

        }

        private SerpentState CheckSerpentMove(Cell cell)
        {
            if (!serpent.IsMovedCellValid(cell))
            {
                return SerpentState.Error;
            }

            //serpent.PrintAllCells();

            serpent.InsertCell(cell);
            if (Global.DisplaySerpentItemNumber)
            {
                map.DrawSerpentCells(serpent);
            }
            else
            {
                map.DrawSerpentCell(serpent, 0);
                map.DrawSerpentCell(serpent, 1);
                //map.DrawSerpentCell(serpent, 2);
            }
            if (!cell.IsFood())
            {
                Cell tail = serpent.GetTail();
                if (!tail.IsSamePostion(cell))
                {
                    map.RedrawMapCell(tail.Row, tail.Col);
                }
                else
                {
                    map.DrawSerpentCell(serpent, 0);
                }
                serpent.RemoveTail();
                map.DrawSerpentCell(serpent, serpent.Body.Count-1);
                //map.DrawSerpentCells(serpent.Body);
            }
            else
            {
                lblSerpentCount.BeginInvoke(new MethodInvoker(delegate ()
                {
                    this.lblSerpentCount.Text = (Convert.ToInt32(this.lblSerpentCount.Text.Trim()) + 1).ToString();
                }));
                cell.Kind = CellKind.Normal;
                //Console.WriteLine("When eat food");
                //serpent.PrintAllCells();
                if (!map.SetFood(serpent))
                {
                    return SerpentState.Finish;
                }
                //Console.WriteLine("\nafter set food");
                //serpent.PrintAllCells();
            }

            return SerpentState.Moving;
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            Keys[] inputKey = { Keys.Left, Keys.Right, Keys.Up, Keys.Down, Keys.F1, Keys.Escape };
            if (IsWork)
            {
                if (inputKey.Contains(e.KeyCode))
                    keyBuffer.Push(e.KeyCode);
            }
        }

        private void frmMain_KeyUp(object sender, KeyEventArgs e)
        {
            keyBuffer.Clear();

        }

        private void chkSerpentNumber_CheckedChanged(object sender, EventArgs e)
        {
            Global.DisplaySerpentItemNumber = chkSerpentNumber.Checked;
        }

        private void chkGridLine_CheckedChanged(object sender, EventArgs e)
        {
            Global.DrawCellGridLine = chkGridLine.Checked;
        }

        private void chkCellMargin_CheckedChanged(object sender, EventArgs e)
        {
            Global.DrawCellMargin = chkCellMargin.Checked;
        }
    }
}
