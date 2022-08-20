namespace MewsiferConsole
{
    partial class MewsiferConsole
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Severity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChannelName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Message = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.topPanelTable = new System.Windows.Forms.TableLayoutPanel();
            this.TailToggle = new System.Windows.Forms.CheckBox();
            this.OmniFilter = new System.Windows.Forms.TextBox();
            this.errCount = new System.Windows.Forms.Label();
            this.warnCount = new System.Windows.Forms.Label();
            this.infoCount = new System.Windows.Forms.Label();
            this.shownCount = new System.Windows.Forms.Label();
            this.tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.topPanelTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.topPanelTable, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 14F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(2032, 909);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Severity,
            this.ChannelName,
            this.Message});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(2, 48);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 33;
            this.dataGridView1.Size = new System.Drawing.Size(2028, 845);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            // 
            // Severity
            // 
            this.Severity.DataPropertyName = "Severity";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Severity.DefaultCellStyle = dataGridViewCellStyle3;
            this.Severity.HeaderText = "Level";
            this.Severity.MinimumWidth = 55;
            this.Severity.Name = "Severity";
            this.Severity.ReadOnly = true;
            this.Severity.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Severity.ToolTipText = "Log Severity";
            this.Severity.Width = 55;
            // 
            // ChannelName
            // 
            this.ChannelName.DataPropertyName = "ChannelName";
            this.ChannelName.HeaderText = "Channel";
            this.ChannelName.MinimumWidth = 300;
            this.ChannelName.Name = "ChannelName";
            this.ChannelName.ReadOnly = true;
            this.ChannelName.Width = 300;
            // 
            // Message
            // 
            this.Message.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Message.DataPropertyName = "Message";
            this.Message.FillWeight = 200F;
            this.Message.HeaderText = "Message";
            this.Message.MinimumWidth = 8;
            this.Message.Name = "Message";
            this.Message.ReadOnly = true;
            // 
            // topPanelTable
            // 
            this.topPanelTable.ColumnCount = 6;
            this.topPanelTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.topPanelTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.topPanelTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.topPanelTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.topPanelTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.topPanelTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this.topPanelTable.Controls.Add(this.TailToggle, 5, 0);
            this.topPanelTable.Controls.Add(this.OmniFilter, 0, 0);
            this.topPanelTable.Controls.Add(this.errCount, 2, 0);
            this.topPanelTable.Controls.Add(this.warnCount, 3, 0);
            this.topPanelTable.Controls.Add(this.infoCount, 4, 0);
            this.topPanelTable.Controls.Add(this.shownCount, 1, 0);
            this.topPanelTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topPanelTable.Location = new System.Drawing.Point(2, 2);
            this.topPanelTable.Margin = new System.Windows.Forms.Padding(2);
            this.topPanelTable.Name = "topPanelTable";
            this.topPanelTable.RowCount = 1;
            this.topPanelTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.topPanelTable.Size = new System.Drawing.Size(2028, 42);
            this.topPanelTable.TabIndex = 1;
            // 
            // TailToggle
            // 
            this.TailToggle.Appearance = System.Windows.Forms.Appearance.Button;
            this.TailToggle.AutoSize = true;
            this.TailToggle.Checked = true;
            this.TailToggle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.TailToggle.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TailToggle.Location = new System.Drawing.Point(1958, 2);
            this.TailToggle.Margin = new System.Windows.Forms.Padding(2);
            this.TailToggle.Name = "TailToggle";
            this.TailToggle.Size = new System.Drawing.Size(56, 35);
            this.TailToggle.TabIndex = 0;
            this.TailToggle.Text = "Tail";
            this.TailToggle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.TailToggle.UseVisualStyleBackColor = true;
            // 
            // OmniFilter
            // 
            this.OmniFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OmniFilter.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.OmniFilter.Location = new System.Drawing.Point(2, 2);
            this.OmniFilter.Margin = new System.Windows.Forms.Padding(2);
            this.OmniFilter.Name = "OmniFilter";
            this.OmniFilter.Size = new System.Drawing.Size(1412, 37);
            this.OmniFilter.TabIndex = 1;
            // 
            // errCount
            // 
            this.errCount.AutoSize = true;
            this.errCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.errCount.Location = new System.Drawing.Point(1599, 0);
            this.errCount.Name = "errCount";
            this.errCount.Size = new System.Drawing.Size(114, 42);
            this.errCount.TabIndex = 2;
            this.errCount.Text = "E: 0";
            this.errCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // warnCount
            // 
            this.warnCount.AutoSize = true;
            this.warnCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.warnCount.Location = new System.Drawing.Point(1719, 0);
            this.warnCount.Name = "warnCount";
            this.warnCount.Size = new System.Drawing.Size(114, 42);
            this.warnCount.TabIndex = 3;
            this.warnCount.Text = "W: 0";
            this.warnCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // infoCount
            // 
            this.infoCount.AutoSize = true;
            this.infoCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.infoCount.Location = new System.Drawing.Point(1839, 0);
            this.infoCount.Name = "infoCount";
            this.infoCount.Size = new System.Drawing.Size(114, 42);
            this.infoCount.TabIndex = 4;
            this.infoCount.Text = "I: 0";
            this.infoCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // shownCount
            // 
            this.shownCount.AutoSize = true;
            this.shownCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.shownCount.Location = new System.Drawing.Point(1419, 0);
            this.shownCount.Name = "shownCount";
            this.shownCount.Size = new System.Drawing.Size(174, 42);
            this.shownCount.TabIndex = 5;
            this.shownCount.Text = "0/0";
            this.shownCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tooltip
            // 
            this.tooltip.IsBalloon = true;
            this.tooltip.ShowAlways = true;
            this.tooltip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.tooltip.Popup += new System.Windows.Forms.PopupEventHandler(this.toolTip1_Popup);
            // 
            // MewsiferConsole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2032, 909);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MewsiferConsole";
            this.Text = "MewsiferConsole";
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.topPanelTable.ResumeLayout(false);
            this.topPanelTable.PerformLayout();
            this.ResumeLayout(false);

        }

    #endregion

    private TableLayoutPanel tableLayoutPanel1;
        private DataGridView dataGridView1;
        private TableLayoutPanel topPanelTable;
        private CheckBox TailToggle;
        private TextBox OmniFilter;
        private Label errCount;
        private Label warnCount;
        private Label infoCount;
        private Label shownCount;
        private ToolTip tooltip;
        private DataGridViewTextBoxColumn Severity;
        private DataGridViewTextBoxColumn ChannelName;
        private DataGridViewTextBoxColumn Message;
    }
}