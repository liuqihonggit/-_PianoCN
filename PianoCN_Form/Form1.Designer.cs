namespace PlotFactory
{
    partial class PiaForm
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BottomPanel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.GroupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.BottomPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupBox2
            // 
            this.GroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox2.Controls.Add(this.button1);
            this.GroupBox2.Controls.Add(this.textBox1);
            this.GroupBox2.Location = new System.Drawing.Point(12, 12);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(623, 56);
            this.GroupBox2.TabIndex = 14;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = "选择文件";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;        
            this.button1.Font = new System.Drawing.Font("宋体", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(592, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(25, 22);
            this.button1.TabIndex = 15;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button3_Click);
            // 
            // textBox1
            // 
            this.textBox1.AllowDrop = true;
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(9, 20);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(576, 21);
            this.textBox1.TabIndex = 24;
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Location = new System.Drawing.Point(9, 20);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox2.Size = new System.Drawing.Size(606, 402);
            this.textBox2.TabIndex = 15;
            this.textBox2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox2_KeyPress);
            // 
            // button3
            //      
            this.button3.Location = new System.Drawing.Point(110, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(95, 24);
            this.button3.TabIndex = 16;
            this.button3.Text = "保存文本";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.SizeChanged += new System.EventHandler(this.Button2_SizeChanged);
            this.button3.Click += new System.EventHandler(this.Button1_Click);
            // 
            // button4
            //            
            this.button4.Location = new System.Drawing.Point(209, 3);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(95, 24);
            this.button4.TabIndex = 17;
            this.button4.Text = "取消";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.SizeChanged += new System.EventHandler(this.Button2_SizeChanged);
            this.button4.Click += new System.EventHandler(this.Button2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.BottomPanel1);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Location = new System.Drawing.Point(12, 74);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(623, 459);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "文件内容:";
            // 
            // BottomPanel1
            // 
            this.BottomPanel1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.BottomPanel1.Controls.Add(this.button2);
            this.BottomPanel1.Controls.Add(this.button3);
            this.BottomPanel1.Controls.Add(this.button4);
            this.BottomPanel1.Location = new System.Drawing.Point(157, 425);
            this.BottomPanel1.Name = "BottomPanel1";
            this.BottomPanel1.Size = new System.Drawing.Size(322, 31);
            this.BottomPanel1.TabIndex = 19;
            // 
            // button2
            //          
            this.button2.Location = new System.Drawing.Point(9, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(95, 24);
            this.button2.TabIndex = 18;
            this.button2.Text = "保存到源文件";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.SizeChanged += new System.EventHandler(this.Button2_SizeChanged);
            this.button2.Click += new System.EventHandler(this.Button4_Click);
            // 
            // PiaForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 541);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.GroupBox2);
            this.HelpButton = true;
            this.ImeMode = System.Windows.Forms.ImeMode.On;
            this.Name = "PiaForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "修改CAD打印机及样式";
            this.Load += new System.EventHandler(this.PiaForm_Load);
            this.GroupBox2.ResumeLayout(false);
            this.GroupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.BottomPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GroupBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel BottomPanel1;
    }
}

