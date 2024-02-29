namespace rainflow
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            listBox1 = new ListBox();
            button1 = new Button();
            label1 = new Label();
            button2 = new Button();
            dataGridView1 = new DataGridView();
            button3 = new Button();
            button4 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // listBox1
            // 
            listBox1.BackColor = Color.FromArgb(224, 224, 224);
            listBox1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 21;
            listBox1.Location = new Point(202, 12);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(398, 25);
            listBox1.TabIndex = 0;
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            listBox1.DragDrop += MainForm_DragDrop;
            listBox1.DragEnter += MainForm_DragEnter;
            // 
            // button1
            // 
            button1.Location = new Point(12, 47);
            button1.Name = "button1";
            button1.Size = new Size(170, 50);
            button1.TabIndex = 1;
            button1.Text = "Carregar Rainflow";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F);
            label1.Location = new Point(12, 12);
            label1.Name = "label1";
            label1.Size = new Size(184, 25);
            label1.TabIndex = 2;
            label1.Text = "Arquivo selecionado";
            label1.Click += label1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(188, 47);
            button2.Name = "button2";
            button2.Size = new Size(74, 50);
            button2.TabIndex = 3;
            button2.Text = "Limpar";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 112);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(588, 456);
            dataGridView1.TabIndex = 4;
            // 
            // button3
            // 
            button3.Location = new Point(268, 47);
            button3.Name = "button3";
            button3.Size = new Size(170, 50);
            button3.TabIndex = 5;
            button3.Text = "Calcular Ciclos";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(444, 47);
            button4.Name = "button4";
            button4.Size = new Size(156, 50);
            button4.TabIndex = 6;
            button4.Text = "Gerar Excel";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(618, 580);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(dataGridView1);
            Controls.Add(button2);
            Controls.Add(label1);
            Controls.Add(button1);
            Controls.Add(listBox1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximumSize = new Size(634, 619);
            MinimumSize = new Size(634, 619);
            Name = "Form1";
            Text = "Rainflow";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox listBox1;
        private Button button1;
        private Label label1;
        private Button button2;
        private DataGridView dataGridView1;
        private Button button3;
        private Button button4;
    }
}
