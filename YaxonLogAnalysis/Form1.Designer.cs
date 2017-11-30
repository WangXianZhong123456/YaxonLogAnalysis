namespace YaxonLogAnalysis
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BeginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EndToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SelectFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectDireToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CloseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.其他ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.JsonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExceptionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(1, 21);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(685, 364);
            this.listBox1.TabIndex = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem,
            this.其他ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(686, 25);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ToolStripMenuItem
            // 
            this.ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BeginToolStripMenuItem,
            this.EndToolStripMenuItem,
            this.SelectFileToolStripMenuItem,
            this.selectDireToolStripMenuItem,
            this.CloseToolStripMenuItem});
            this.ToolStripMenuItem.Name = "ToolStripMenuItem";
            this.ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.ToolStripMenuItem.Text = "选项";
            // 
            // BeginToolStripMenuItem
            // 
            this.BeginToolStripMenuItem.Name = "BeginToolStripMenuItem";
            this.BeginToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.BeginToolStripMenuItem.Text = "开始";
            this.BeginToolStripMenuItem.Click += new System.EventHandler(this.BeginToolStripMenuItem_Click);
            // 
            // EndToolStripMenuItem
            // 
            this.EndToolStripMenuItem.Name = "EndToolStripMenuItem";
            this.EndToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.EndToolStripMenuItem.Text = "结束";
            this.EndToolStripMenuItem.Click += new System.EventHandler(this.EndToolStripMenuItem_Click);
            // 
            // SelectFileToolStripMenuItem
            // 
            this.SelectFileToolStripMenuItem.Name = "SelectFileToolStripMenuItem";
            this.SelectFileToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.SelectFileToolStripMenuItem.Text = "选择文件";
            this.SelectFileToolStripMenuItem.Click += new System.EventHandler(this.SelectFileToolStripMenuItem_Click);
            // 
            // selectDireToolStripMenuItem
            // 
            this.selectDireToolStripMenuItem.Name = "selectDireToolStripMenuItem";
            this.selectDireToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.selectDireToolStripMenuItem.Text = "选择目录";
            this.selectDireToolStripMenuItem.Click += new System.EventHandler(this.selectDireToolStripMenuItem_Click);
            // 
            // CloseToolStripMenuItem
            // 
            this.CloseToolStripMenuItem.Name = "CloseToolStripMenuItem";
            this.CloseToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.CloseToolStripMenuItem.Text = "关闭";
            this.CloseToolStripMenuItem.Click += new System.EventHandler(this.CloseToolStripMenuItem_Click);
            // 
            // 其他ToolStripMenuItem
            // 
            this.其他ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExceptionToolStripMenuItem,
            this.JsonToolStripMenuItem});
            this.其他ToolStripMenuItem.Name = "其他ToolStripMenuItem";
            this.其他ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.其他ToolStripMenuItem.Text = "其他";
            // 
            // JsonToolStripMenuItem
            // 
            this.JsonToolStripMenuItem.Name = "JsonToolStripMenuItem";
            this.JsonToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.JsonToolStripMenuItem.Text = "效验Json";
            this.JsonToolStripMenuItem.Click += new System.EventHandler(this.JsonToolStripMenuItem_Click);
            // 
            // ExceptionToolStripMenuItem
            // 
            this.ExceptionToolStripMenuItem.Name = "ExceptionToolStripMenuItem";
            this.ExceptionToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.ExceptionToolStripMenuItem.Text = "处理异常数据";
            this.ExceptionToolStripMenuItem.Click += new System.EventHandler(this.ExceptionToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 398);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "日志解析上报程序";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SelectFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CloseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectDireToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BeginToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EndToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 其他ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem JsonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExceptionToolStripMenuItem;
    }
}

