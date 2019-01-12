namespace SystAnalys_lr1
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.about = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteALLButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.drawEdgeButton = new System.Windows.Forms.Button();
            this.drawVertexButton = new System.Windows.Forms.Button();
            this.sheet1 = new System.Windows.Forms.PictureBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.sheet2 = new System.Windows.Forms.PictureBox();
            this.buttonCombine = new System.Windows.Forms.Button();
            this.sheet3 = new System.Windows.Forms.PictureBox();
            this.selectButton = new System.Windows.Forms.Button();
            this.listBox = new System.Windows.Forms.ListBox();
            this.buttonCycleCombine = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sheet2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sheet3)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.about});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(755, 24);
            this.menuStrip1.TabIndex = 12;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // about
            // 
            this.about.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.about.Name = "about";
            this.about.Size = new System.Drawing.Size(94, 20);
            this.about.Text = "О программе";
            this.about.Click += new System.EventHandler(this.about_Click);
            // 
            // deleteALLButton
            // 
            this.deleteALLButton.Image = global::SystAnalys_lr1.Properties.Resources.deleteAll;
            this.deleteALLButton.Location = new System.Drawing.Point(13, 217);
            this.deleteALLButton.Name = "deleteALLButton";
            this.deleteALLButton.Size = new System.Drawing.Size(45, 45);
            this.deleteALLButton.TabIndex = 5;
            this.deleteALLButton.UseVisualStyleBackColor = true;
            this.deleteALLButton.Click += new System.EventHandler(this.deleteALLButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Image = global::SystAnalys_lr1.Properties.Resources.delete;
            this.deleteButton.Location = new System.Drawing.Point(12, 154);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(45, 45);
            this.deleteButton.TabIndex = 3;
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // drawEdgeButton
            // 
            this.drawEdgeButton.Image = global::SystAnalys_lr1.Properties.Resources.edge;
            this.drawEdgeButton.Location = new System.Drawing.Point(13, 90);
            this.drawEdgeButton.Name = "drawEdgeButton";
            this.drawEdgeButton.Size = new System.Drawing.Size(45, 45);
            this.drawEdgeButton.TabIndex = 2;
            this.drawEdgeButton.UseVisualStyleBackColor = true;
            this.drawEdgeButton.Click += new System.EventHandler(this.drawEdgeButton_Click);
            // 
            // drawVertexButton
            // 
            this.drawVertexButton.Image = global::SystAnalys_lr1.Properties.Resources.vertex;
            this.drawVertexButton.Location = new System.Drawing.Point(13, 27);
            this.drawVertexButton.Name = "drawVertexButton";
            this.drawVertexButton.Size = new System.Drawing.Size(45, 45);
            this.drawVertexButton.TabIndex = 1;
            this.drawVertexButton.UseVisualStyleBackColor = true;
            this.drawVertexButton.Click += new System.EventHandler(this.drawVertexButton_Click);
            // 
            // sheet1
            // 
            this.sheet1.BackColor = System.Drawing.SystemColors.Control;
            this.sheet1.Location = new System.Drawing.Point(80, 27);
            this.sheet1.Name = "sheet1";
            this.sheet1.Size = new System.Drawing.Size(260, 235);
            this.sheet1.TabIndex = 0;
            this.sheet1.TabStop = false;
            this.sheet1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.sheet_MouseClick);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(568, 565);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(96, 23);
            this.saveButton.TabIndex = 13;
            this.saveButton.Text = "Сохранить граф";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // sheet2
            // 
            this.sheet2.Location = new System.Drawing.Point(404, 27);
            this.sheet2.Name = "sheet2";
            this.sheet2.Size = new System.Drawing.Size(260, 235);
            this.sheet2.TabIndex = 14;
            this.sheet2.TabStop = false;
            this.sheet2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.sheet2_MouseClick);
            // 
            // buttonCombine
            // 
            this.buttonCombine.Location = new System.Drawing.Point(263, 268);
            this.buttonCombine.Name = "buttonCombine";
            this.buttonCombine.Size = new System.Drawing.Size(89, 23);
            this.buttonCombine.TabIndex = 15;
            this.buttonCombine.Text = "Объединить";
            this.buttonCombine.UseVisualStyleBackColor = true;
            this.buttonCombine.Click += new System.EventHandler(this.buttonCombine_Click);
            // 
            // sheet3
            // 
            this.sheet3.Location = new System.Drawing.Point(188, 314);
            this.sheet3.Name = "sheet3";
            this.sheet3.Size = new System.Drawing.Size(364, 274);
            this.sheet3.TabIndex = 16;
            this.sheet3.TabStop = false;
            this.sheet3.MouseClick += new System.Windows.Forms.MouseEventHandler(this.sheet3_MouseClick);
            // 
            // selectButton
            // 
            this.selectButton.Image = global::SystAnalys_lr1.Properties.Resources.cursor;
            this.selectButton.Location = new System.Drawing.Point(13, 280);
            this.selectButton.Name = "selectButton";
            this.selectButton.Size = new System.Drawing.Size(45, 47);
            this.selectButton.TabIndex = 17;
            this.selectButton.UseVisualStyleBackColor = true;
            this.selectButton.Click += new System.EventHandler(this.selectButton_Click);
            // 
            // listBox
            // 
            this.listBox.FormattingEnabled = true;
            this.listBox.Location = new System.Drawing.Point(569, 314);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(174, 186);
            this.listBox.TabIndex = 18;
            // 
            // buttonCycleCombine
            // 
            this.buttonCycleCombine.Location = new System.Drawing.Point(382, 268);
            this.buttonCycleCombine.Name = "buttonCycleCombine";
            this.buttonCycleCombine.Size = new System.Drawing.Size(140, 23);
            this.buttonCycleCombine.TabIndex = 19;
            this.buttonCycleCombine.Text = "Круговое объединение";
            this.buttonCycleCombine.UseVisualStyleBackColor = true;
            this.buttonCycleCombine.Click += new System.EventHandler(this.buttonCycleCombine_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(245, 107);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(289, 92);
            this.button1.TabIndex = 21;
            this.button1.Text = "Программа предназначена для создания графов,\r\nс последующим объединением их.\r\nКол" +
    "ичество вершин в результирующем\r\nграфе должно быть не более 14.";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(755, 600);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonCycleCombine);
            this.Controls.Add(this.listBox);
            this.Controls.Add(this.selectButton);
            this.Controls.Add(this.sheet3);
            this.Controls.Add(this.buttonCombine);
            this.Controls.Add(this.sheet2);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.deleteALLButton);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.drawEdgeButton);
            this.Controls.Add(this.drawVertexButton);
            this.Controls.Add(this.sheet1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sheet2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sheet3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox sheet1;
        private System.Windows.Forms.Button drawVertexButton;
        private System.Windows.Forms.Button drawEdgeButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button deleteALLButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem about;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.PictureBox sheet2;
        private System.Windows.Forms.Button buttonCombine;
        private System.Windows.Forms.PictureBox sheet3;
        private System.Windows.Forms.Button selectButton;
        private System.Windows.Forms.ListBox listBox;
        private System.Windows.Forms.Button buttonCycleCombine;
        private System.Windows.Forms.Button button1;
    }
}

