namespace MewsiferConsole
{
  partial class ChooseMessageSourceForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
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
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.fromFile = new System.Windows.Forms.Button();
            this.fromGame = new System.Windows.Forms.Button();
            this.fromDpaste = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.fromFile, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.fromGame, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.fromDpaste, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1519, 199);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // fromFile
            // 
            this.fromFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fromFile.Location = new System.Drawing.Point(3, 3);
            this.fromFile.Name = "fromFile";
            this.fromFile.Size = new System.Drawing.Size(500, 193);
            this.fromFile.TabIndex = 0;
            this.fromFile.Text = "Load from file";
            this.fromFile.UseVisualStyleBackColor = true;
            this.fromFile.Click += new System.EventHandler(this.fromFile_Click);
            // 
            // fromGame
            // 
            this.fromGame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fromGame.Location = new System.Drawing.Point(509, 3);
            this.fromGame.Name = "fromGame";
            this.fromGame.Size = new System.Drawing.Size(500, 193);
            this.fromGame.TabIndex = 1;
            this.fromGame.Text = "Connect to game";
            this.fromGame.UseVisualStyleBackColor = true;
            this.fromGame.Click += new System.EventHandler(this.fromGame_Click);
            // 
            // fromDpaste
            // 
            this.fromDpaste.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fromDpaste.Location = new System.Drawing.Point(1015, 3);
            this.fromDpaste.Name = "fromDpaste";
            this.fromDpaste.Size = new System.Drawing.Size(501, 193);
            this.fromDpaste.TabIndex = 2;
            this.fromDpaste.Text = "Load from dpaste.org";
            this.fromDpaste.UseVisualStyleBackColor = true;
            this.fromDpaste.Click += new System.EventHandler(this.fromDpaste_Click);
            // 
            // ChooseMessageSourceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1519, 199);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ChooseMessageSourceForm";
            this.Text = "ChooseMessageSourceForm";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

    }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Button fromFile;
        private Button fromGame;
        private Button fromDpaste;
    }
}