namespace mko.WinForm
{
    partial class FormWithTabControl
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStripBaseForm = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelBaseForm = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBarBaseForm = new System.Windows.Forms.ToolStripProgressBar();
            this.tabControlBaseFormMain = new System.Windows.Forms.TabControl();
            this.tabPageBaseFormLogs = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.LogDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LogType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.messageDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BindingSourceLogTab = new System.Windows.Forms.BindingSource(this.components);
            this.panelBaseFormtabPageLogs = new System.Windows.Forms.Panel();
            this.btnBaseFormTabPageLogsFibonacci = new System.Windows.Forms.Button();
            this.btnBaseFormTabPageLogsClear = new System.Windows.Forms.Button();
            this.tabPageBaseForm1 = new System.Windows.Forms.TabPage();
            this.numericUpDownBaseFormTabControlLog = new System.Windows.Forms.NumericUpDown();
            this.btnBaseFormTabControlLogFactorization = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.statusStripBaseForm.SuspendLayout();
            this.tabControlBaseFormMain.SuspendLayout();
            this.tabPageBaseFormLogs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BindingSourceLogTab)).BeginInit();
            this.panelBaseFormtabPageLogs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBaseFormTabControlLog)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(845, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // statusStripBaseForm
            // 
            this.statusStripBaseForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelBaseForm,
            this.toolStripProgressBarBaseForm});
            this.statusStripBaseForm.Location = new System.Drawing.Point(0, 438);
            this.statusStripBaseForm.Name = "statusStripBaseForm";
            this.statusStripBaseForm.Size = new System.Drawing.Size(845, 22);
            this.statusStripBaseForm.TabIndex = 1;
            this.statusStripBaseForm.Text = "statusStripBaseForm";
            // 
            // toolStripStatusLabelBaseForm
            // 
            this.toolStripStatusLabelBaseForm.Name = "toolStripStatusLabelBaseForm";
            this.toolStripStatusLabelBaseForm.Size = new System.Drawing.Size(164, 17);
            this.toolStripStatusLabelBaseForm.Text = "toolStripStatusLabelBaseForm";
            // 
            // toolStripProgressBarBaseForm
            // 
            this.toolStripProgressBarBaseForm.Name = "toolStripProgressBarBaseForm";
            this.toolStripProgressBarBaseForm.Size = new System.Drawing.Size(100, 16);
            // 
            // tabControlBaseFormMain
            // 
            this.tabControlBaseFormMain.Controls.Add(this.tabPageBaseFormLogs);
            this.tabControlBaseFormMain.Controls.Add(this.tabPageBaseForm1);
            this.tabControlBaseFormMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlBaseFormMain.Location = new System.Drawing.Point(0, 24);
            this.tabControlBaseFormMain.Name = "tabControlBaseFormMain";
            this.tabControlBaseFormMain.SelectedIndex = 0;
            this.tabControlBaseFormMain.Size = new System.Drawing.Size(845, 414);
            this.tabControlBaseFormMain.TabIndex = 2;
            // 
            // tabPageBaseFormLogs
            // 
            this.tabPageBaseFormLogs.Controls.Add(this.dataGridView1);
            this.tabPageBaseFormLogs.Controls.Add(this.panelBaseFormtabPageLogs);
            this.tabPageBaseFormLogs.Location = new System.Drawing.Point(4, 22);
            this.tabPageBaseFormLogs.Name = "tabPageBaseFormLogs";
            this.tabPageBaseFormLogs.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBaseFormLogs.Size = new System.Drawing.Size(837, 388);
            this.tabPageBaseFormLogs.TabIndex = 0;
            this.tabPageBaseFormLogs.Text = "Log\'s";
            this.tabPageBaseFormLogs.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LogDate,
            this.LogType,
            this.messageDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.BindingSourceLogTab;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 38);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(831, 347);
            this.dataGridView1.TabIndex = 1;
            // 
            // LogDate
            // 
            this.LogDate.DataPropertyName = "LogDate";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.Format = "G";
            dataGridViewCellStyle2.NullValue = null;
            this.LogDate.DefaultCellStyle = dataGridViewCellStyle2;
            this.LogDate.HeaderText = "LogDate";
            this.LogDate.Name = "LogDate";
            this.LogDate.ReadOnly = true;
            this.LogDate.Width = 150;
            // 
            // LogType
            // 
            this.LogType.DataPropertyName = "LogType";
            this.LogType.HeaderText = "LogType";
            this.LogType.Name = "LogType";
            this.LogType.ReadOnly = true;
            // 
            // messageDataGridViewTextBoxColumn
            // 
            this.messageDataGridViewTextBoxColumn.DataPropertyName = "Message";
            this.messageDataGridViewTextBoxColumn.HeaderText = "Message";
            this.messageDataGridViewTextBoxColumn.Name = "messageDataGridViewTextBoxColumn";
            this.messageDataGridViewTextBoxColumn.ReadOnly = true;
            this.messageDataGridViewTextBoxColumn.Width = 400;
            // 
            // BindingSourceLogTab
            // 
            this.BindingSourceLogTab.DataSource = typeof(mko.Log.ILogInfo);
            // 
            // panelBaseFormtabPageLogs
            // 
            this.panelBaseFormtabPageLogs.Controls.Add(this.btnBaseFormTabControlLogFactorization);
            this.panelBaseFormtabPageLogs.Controls.Add(this.numericUpDownBaseFormTabControlLog);
            this.panelBaseFormtabPageLogs.Controls.Add(this.btnBaseFormTabPageLogsFibonacci);
            this.panelBaseFormtabPageLogs.Controls.Add(this.btnBaseFormTabPageLogsClear);
            this.panelBaseFormtabPageLogs.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelBaseFormtabPageLogs.Location = new System.Drawing.Point(3, 3);
            this.panelBaseFormtabPageLogs.Name = "panelBaseFormtabPageLogs";
            this.panelBaseFormtabPageLogs.Size = new System.Drawing.Size(831, 35);
            this.panelBaseFormtabPageLogs.TabIndex = 0;
            // 
            // btnBaseFormTabPageLogsFibonacci
            // 
            this.btnBaseFormTabPageLogsFibonacci.Location = new System.Drawing.Point(99, 4);
            this.btnBaseFormTabPageLogsFibonacci.Name = "btnBaseFormTabPageLogsFibonacci";
            this.btnBaseFormTabPageLogsFibonacci.Size = new System.Drawing.Size(89, 23);
            this.btnBaseFormTabPageLogsFibonacci.TabIndex = 1;
            this.btnBaseFormTabPageLogsFibonacci.Text = "Fibonacci[0]";
            this.btnBaseFormTabPageLogsFibonacci.UseVisualStyleBackColor = true;
            this.btnBaseFormTabPageLogsFibonacci.Click += new System.EventHandler(this.btnBaseFormTabPageLogsFibonacci_Click);
            // 
            // btnBaseFormTabPageLogsClear
            // 
            this.btnBaseFormTabPageLogsClear.Location = new System.Drawing.Point(6, 4);
            this.btnBaseFormTabPageLogsClear.Name = "btnBaseFormTabPageLogsClear";
            this.btnBaseFormTabPageLogsClear.Size = new System.Drawing.Size(75, 23);
            this.btnBaseFormTabPageLogsClear.TabIndex = 0;
            this.btnBaseFormTabPageLogsClear.Text = "Clear";
            this.btnBaseFormTabPageLogsClear.UseVisualStyleBackColor = true;
            this.btnBaseFormTabPageLogsClear.Click += new System.EventHandler(this.btnBaseFormTabPageLogsClear_Click);
            // 
            // tabPageBaseForm1
            // 
            this.tabPageBaseForm1.Location = new System.Drawing.Point(4, 22);
            this.tabPageBaseForm1.Name = "tabPageBaseForm1";
            this.tabPageBaseForm1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBaseForm1.Size = new System.Drawing.Size(837, 388);
            this.tabPageBaseForm1.TabIndex = 1;
            this.tabPageBaseForm1.Text = "1";
            this.tabPageBaseForm1.UseVisualStyleBackColor = true;
            // 
            // numericUpDownBaseFormTabControlLog
            // 
            this.numericUpDownBaseFormTabControlLog.Location = new System.Drawing.Point(222, 7);
            this.numericUpDownBaseFormTabControlLog.Maximum = new decimal(new int[] {
            -727379968,
            232,
            0,
            0});
            this.numericUpDownBaseFormTabControlLog.Name = "numericUpDownBaseFormTabControlLog";
            this.numericUpDownBaseFormTabControlLog.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownBaseFormTabControlLog.TabIndex = 2;
            this.numericUpDownBaseFormTabControlLog.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownBaseFormTabControlLog.Value = new decimal(new int[] {
            354978,
            0,
            0,
            0});
            // 
            // btnBaseFormTabControlLogFactorization
            // 
            this.btnBaseFormTabControlLogFactorization.Location = new System.Drawing.Point(348, 4);
            this.btnBaseFormTabControlLogFactorization.Name = "btnBaseFormTabControlLogFactorization";
            this.btnBaseFormTabControlLogFactorization.Size = new System.Drawing.Size(158, 23);
            this.btnBaseFormTabControlLogFactorization.TabIndex = 3;
            this.btnBaseFormTabControlLogFactorization.Text = "Zerlegen in Primfaktoren";
            this.btnBaseFormTabControlLogFactorization.UseVisualStyleBackColor = true;
            this.btnBaseFormTabControlLogFactorization.Click += new System.EventHandler(this.btnBaseFormTabControlLogFactorization_Click);
            // 
            // FormWithTabControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(845, 460);
            this.Controls.Add(this.tabControlBaseFormMain);
            this.Controls.Add(this.statusStripBaseForm);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormWithTabControl";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStripBaseForm.ResumeLayout(false);
            this.statusStripBaseForm.PerformLayout();
            this.tabControlBaseFormMain.ResumeLayout(false);
            this.tabPageBaseFormLogs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BindingSourceLogTab)).EndInit();
            this.panelBaseFormtabPageLogs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBaseFormTabControlLog)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        protected System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelBaseForm;
        protected System.Windows.Forms.ToolStripProgressBar toolStripProgressBarBaseForm;
        protected System.Windows.Forms.MenuStrip menuStrip1;
        protected System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        protected System.Windows.Forms.TabControl tabControlBaseFormMain;
        protected System.Windows.Forms.TabPage tabPageBaseFormLogs;
        protected System.Windows.Forms.TabPage tabPageBaseForm1;
        protected System.Windows.Forms.Panel panelBaseFormtabPageLogs;
        protected System.Windows.Forms.Button btnBaseFormTabPageLogsClear;
        protected System.Windows.Forms.StatusStrip statusStripBaseForm;
        private System.Windows.Forms.BindingSource BindingSourceLogTab;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnBaseFormTabPageLogsFibonacci;
        private System.Windows.Forms.DataGridViewTextBoxColumn LogDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn LogType;
        private System.Windows.Forms.DataGridViewTextBoxColumn messageDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button btnBaseFormTabControlLogFactorization;
        private System.Windows.Forms.NumericUpDown numericUpDownBaseFormTabControlLog;
    }
}

