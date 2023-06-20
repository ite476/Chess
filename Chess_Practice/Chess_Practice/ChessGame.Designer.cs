namespace Chess_Practice
{
    partial class ChessGame
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            board = new Panel();
            info = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            board.SuspendLayout();
            SuspendLayout();
            // 
            // board
            // 
            board.BackColor = SystemColors.ActiveCaption;
            board.Controls.Add(info);
            board.Dock = DockStyle.Fill;
            board.Location = new Point(0, 0);
            board.Name = "board";
            board.Size = new Size(800, 450);
            board.TabIndex = 0;
            // 
            // info
            // 
            info.AutoSize = true;
            info.Location = new Point(12, 9);
            info.Name = "info";
            info.Size = new Size(28, 15);
            info.TabIndex = 0;
            info.Text = "info";
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Tick += timer1_Tick;
            // 
            // ChessGame
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(board);
            Name = "ChessGame";
            Text = "Form1";
            Load += Form1_Load;
            board.ResumeLayout(false);
            board.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel board;
        private Label info;
        private System.Windows.Forms.Timer timer1;
    }
}