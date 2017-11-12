namespace FunnySnake
{
    partial class frmMain
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlSide = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtAutoPlayTime = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.chkAutoPlay = new System.Windows.Forms.CheckBox();
            this.lblSerpentCount = new System.Windows.Forms.Label();
            this.txtSerpentCount = new System.Windows.Forms.TextBox();
            this.txtMapColCount = new System.Windows.Forms.TextBox();
            this.txtMapRowCount = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlGame = new FunnySnake.SnakePanel();
            this.chkSerpentNumber = new System.Windows.Forms.CheckBox();
            this.chkGridLine = new System.Windows.Forms.CheckBox();
            this.chkCellMargin = new System.Windows.Forms.CheckBox();
            this.pnlSide.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSide
            // 
            this.pnlSide.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSide.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSide.Controls.Add(this.chkCellMargin);
            this.pnlSide.Controls.Add(this.chkGridLine);
            this.pnlSide.Controls.Add(this.chkSerpentNumber);
            this.pnlSide.Controls.Add(this.groupBox1);
            this.pnlSide.Controls.Add(this.lblSerpentCount);
            this.pnlSide.Controls.Add(this.txtSerpentCount);
            this.pnlSide.Controls.Add(this.txtMapColCount);
            this.pnlSide.Controls.Add(this.txtMapRowCount);
            this.pnlSide.Controls.Add(this.label6);
            this.pnlSide.Controls.Add(this.label5);
            this.pnlSide.Controls.Add(this.btnReset);
            this.pnlSide.Controls.Add(this.btnClose);
            this.pnlSide.Controls.Add(this.btnStop);
            this.pnlSide.Controls.Add(this.btnPause);
            this.pnlSide.Controls.Add(this.btnStart);
            this.pnlSide.Controls.Add(this.label1);
            this.pnlSide.Controls.Add(this.label2);
            this.pnlSide.Controls.Add(this.label4);
            this.pnlSide.Location = new System.Drawing.Point(617, 11);
            this.pnlSide.Name = "pnlSide";
            this.pnlSide.Size = new System.Drawing.Size(191, 599);
            this.pnlSide.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtAutoPlayTime);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.chkAutoPlay);
            this.groupBox1.Location = new System.Drawing.Point(12, 181);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(163, 62);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            // 
            // txtAutoPlayTime
            // 
            this.txtAutoPlayTime.Location = new System.Drawing.Point(55, 34);
            this.txtAutoPlayTime.Name = "txtAutoPlayTime";
            this.txtAutoPlayTime.Size = new System.Drawing.Size(47, 19);
            this.txtAutoPlayTime.TabIndex = 12;
            this.txtAutoPlayTime.Text = "5";
            this.txtAutoPlayTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label7.Location = new System.Drawing.Point(108, 37);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(20, 12);
            this.label7.TabIndex = 11;
            this.label7.Text = "ms";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(13, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "速度：";
            // 
            // chkAutoPlay
            // 
            this.chkAutoPlay.AutoSize = true;
            this.chkAutoPlay.Location = new System.Drawing.Point(15, 15);
            this.chkAutoPlay.Name = "chkAutoPlay";
            this.chkAutoPlay.Size = new System.Drawing.Size(75, 16);
            this.chkAutoPlay.TabIndex = 0;
            this.chkAutoPlay.Text = "自動プレイ";
            this.chkAutoPlay.UseVisualStyleBackColor = true;
            // 
            // lblSerpentCount
            // 
            this.lblSerpentCount.Location = new System.Drawing.Point(110, 14);
            this.lblSerpentCount.Name = "lblSerpentCount";
            this.lblSerpentCount.Size = new System.Drawing.Size(30, 19);
            this.lblSerpentCount.TabIndex = 11;
            this.lblSerpentCount.Text = "5";
            this.lblSerpentCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSerpentCount
            // 
            this.txtSerpentCount.Location = new System.Drawing.Point(74, 14);
            this.txtSerpentCount.Name = "txtSerpentCount";
            this.txtSerpentCount.Size = new System.Drawing.Size(30, 19);
            this.txtSerpentCount.TabIndex = 10;
            this.txtSerpentCount.Text = "5";
            // 
            // txtMapColCount
            // 
            this.txtMapColCount.Location = new System.Drawing.Point(65, 81);
            this.txtMapColCount.Name = "txtMapColCount";
            this.txtMapColCount.Size = new System.Drawing.Size(49, 19);
            this.txtMapColCount.TabIndex = 4;
            this.txtMapColCount.Text = "18";
            // 
            // txtMapRowCount
            // 
            this.txtMapRowCount.Location = new System.Drawing.Point(65, 60);
            this.txtMapRowCount.Name = "txtMapRowCount";
            this.txtMapRowCount.Size = new System.Drawing.Size(49, 19);
            this.txtMapRowCount.TabIndex = 3;
            this.txtMapRowCount.Text = "18";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 84);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "列数：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "行数：";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(25, 249);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(138, 30);
            this.btnReset.TabIndex = 5;
            this.btnReset.Text = "地図初期化";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClose.Location = new System.Drawing.Point(25, 554);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(138, 30);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "終了";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(25, 357);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(138, 30);
            this.btnStop.TabIndex = 8;
            this.btnStop.Text = "ストップ";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(25, 321);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(138, 30);
            this.btnPause.TabIndex = 7;
            this.btnPause.Text = "一時停止";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(25, 285);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(138, 30);
            this.btnStart.TabIndex = 6;
            this.btnStart.Text = "スタート！";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(13, 404);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(162, 147);
            this.label1.TabIndex = 5;
            this.label1.Text = "説明：逆方向、地図壁へはできません。F1は１ステップ自動走行。ESC一時停止";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(13, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "蛇の長さ：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.Location = new System.Drawing.Point(13, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "地図のサイズ";
            // 
            // pnlGame
            // 
            this.pnlGame.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlGame.BackColor = System.Drawing.SystemColors.Window;
            this.pnlGame.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlGame.Location = new System.Drawing.Point(11, 11);
            this.pnlGame.map = null;
            this.pnlGame.Name = "pnlGame";
            this.pnlGame.serpent = null;
            this.pnlGame.Size = new System.Drawing.Size(600, 600);
            this.pnlGame.TabIndex = 0;
            // 
            // chkSerpentNumber
            // 
            this.chkSerpentNumber.AutoSize = true;
            this.chkSerpentNumber.Location = new System.Drawing.Point(15, 122);
            this.chkSerpentNumber.Name = "chkSerpentNumber";
            this.chkSerpentNumber.Size = new System.Drawing.Size(122, 16);
            this.chkSerpentNumber.TabIndex = 13;
            this.chkSerpentNumber.Text = "蛇の番号を表示する";
            this.chkSerpentNumber.UseVisualStyleBackColor = true;
            this.chkSerpentNumber.CheckedChanged += new System.EventHandler(this.chkSerpentNumber_CheckedChanged);
            // 
            // chkGridLine
            // 
            this.chkGridLine.AutoSize = true;
            this.chkGridLine.Location = new System.Drawing.Point(15, 143);
            this.chkGridLine.Name = "chkGridLine";
            this.chkGridLine.Size = new System.Drawing.Size(134, 16);
            this.chkGridLine.TabIndex = 13;
            this.chkGridLine.Text = "グリッドラインを表示する";
            this.chkGridLine.UseVisualStyleBackColor = true;
            this.chkGridLine.CheckedChanged += new System.EventHandler(this.chkGridLine_CheckedChanged);
            // 
            // chkCellMargin
            // 
            this.chkCellMargin.AutoSize = true;
            this.chkCellMargin.Location = new System.Drawing.Point(15, 165);
            this.chkCellMargin.Name = "chkCellMargin";
            this.chkCellMargin.Size = new System.Drawing.Size(144, 16);
            this.chkCellMargin.TabIndex = 13;
            this.chkCellMargin.Text = "セルのマージンを設定する";
            this.chkCellMargin.UseVisualStyleBackColor = true;
            this.chkCellMargin.CheckedChanged += new System.EventHandler(this.chkCellMargin_CheckedChanged);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(818, 622);
            this.Controls.Add(this.pnlSide);
            this.Controls.Add(this.pnlGame);
            this.KeyPreview = true;
            this.Name = "frmMain";
            this.Text = "Funny Snake";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResizeEnd += new System.EventHandler(this.frmMain_ResizeEnd);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyUp);
            this.pnlSide.ResumeLayout(false);
            this.pnlSide.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private SnakePanel pnlGame;
        private System.Windows.Forms.Panel pnlSide;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMapColCount;
        private System.Windows.Forms.TextBox txtMapRowCount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSerpentCount;
        private System.Windows.Forms.Label lblSerpentCount;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtAutoPlayTime;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkAutoPlay;
        private System.Windows.Forms.CheckBox chkCellMargin;
        private System.Windows.Forms.CheckBox chkGridLine;
        private System.Windows.Forms.CheckBox chkSerpentNumber;
    }
}

