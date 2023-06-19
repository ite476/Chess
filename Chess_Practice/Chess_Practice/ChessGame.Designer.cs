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
            board = new Panel();
            SuspendLayout();
            // 
            // board
            // 
            board.BackColor = SystemColors.ActiveCaption;
            board.Dock = DockStyle.Fill;
            board.Location = new Point(0, 0);
            board.Name = "board";
            board.Size = new Size(800, 450);
            board.TabIndex = 0;
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
            ResumeLayout(false);
        }

        #endregion

        private Panel board;
    }
}