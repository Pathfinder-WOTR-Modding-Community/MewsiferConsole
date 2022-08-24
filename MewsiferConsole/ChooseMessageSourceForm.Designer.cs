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
            this.catPoint = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.catPoint)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.fromFile, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.fromGame, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.catPoint, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1655, 535);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // fromFile
            // 
            this.fromFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fromFile.Location = new System.Drawing.Point(3, 3);
            this.fromFile.Name = "fromFile";
            this.fromFile.Size = new System.Drawing.Size(545, 529);
            this.fromFile.TabIndex = 0;
            this.fromFile.Text = "Load from file";
            this.fromFile.UseVisualStyleBackColor = true;
            this.fromFile.Click += new System.EventHandler(this.fromFile_Click);
            // 
            // fromGame
            // 
            this.fromGame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fromGame.Location = new System.Drawing.Point(1105, 3);
            this.fromGame.Name = "fromGame";
            this.fromGame.Size = new System.Drawing.Size(547, 529);
            this.fromGame.TabIndex = 1;
            this.fromGame.Text = "Connect to game";
            this.fromGame.UseVisualStyleBackColor = true;
            this.fromGame.Click += new System.EventHandler(this.fromGame_Click);
            // 
            // catPoint
            // 
            this.catPoint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.catPoint.Image = global::MewsiferConsole.Properties.Resources.cat_point;
            this.catPoint.Location = new System.Drawing.Point(554, 3);
            this.catPoint.Name = "catPoint";
            this.catPoint.Size = new System.Drawing.Size(545, 529);
            this.catPoint.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.catPoint.TabIndex = 2;
            this.catPoint.TabStop = false;
            // 
            // ChooseMessageSourceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1655, 535);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ChooseMessageSourceForm";
            this.Text = "ChooseMessageSourceForm";
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.catPoint)).EndInit();
            this.ResumeLayout(false);

    }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Button fromFile;
        private Button fromGame;
        private PictureBox catPoint;
    }
}