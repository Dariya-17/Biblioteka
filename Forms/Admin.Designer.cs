namespace Forms
{
    partial class Admin
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
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button5 = new Button();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            button4 = new Button();
            button6 = new Button();
            button7 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = Color.Khaki;
            button1.Location = new Point(509, 280);
            button1.Margin = new Padding(4, 3, 4, 3);
            button1.Name = "button1";
            button1.Size = new Size(151, 75);
            button1.TabIndex = 0;
            button1.Text = "Manage Books";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.Tan;
            button2.Location = new Point(324, 283);
            button2.Margin = new Padding(4, 3, 4, 3);
            button2.Name = "button2";
            button2.Size = new Size(151, 72);
            button2.TabIndex = 1;
            button2.Text = "Manage Authors";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.BackColor = Color.Tan;
            button3.Location = new Point(505, 179);
            button3.Margin = new Padding(4, 3, 4, 3);
            button3.Name = "button3";
            button3.Size = new Size(151, 74);
            button3.TabIndex = 2;
            button3.Text = "Manage Genre";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // button5
            // 
            button5.BackColor = Color.Khaki;
            button5.Location = new Point(324, 181);
            button5.Margin = new Padding(4, 3, 4, 3);
            button5.Name = "button5";
            button5.Size = new Size(155, 72);
            button5.TabIndex = 4;
            button5.Text = "Close";
            button5.UseVisualStyleBackColor = false;
            button5.Click += button5_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Image = Properties.Resources.man_with_the_inscription_admin_icon_outline_man_with_the_inscription_admin_vector_icon_color_flat_isolated_2H36T8M;
            pictureBox1.Location = new Point(12, 341);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(167, 213);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Yu Gothic", 13.8F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.Location = new Point(404, 109);
            label1.Name = "label1";
            label1.Size = new Size(202, 30);
            label1.TabIndex = 6;
            label1.Text = "Welcome,Admin!";
            // 
            // button4
            // 
            button4.BackColor = Color.Khaki;
            button4.Location = new Point(324, 390);
            button4.Name = "button4";
            button4.Size = new Size(151, 69);
            button4.TabIndex = 7;
            button4.Text = "See all clients";
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click_1;
            // 
            // button6
            // 
            button6.BackColor = Color.Tan;
            button6.Location = new Point(509, 390);
            button6.Name = "button6";
            button6.Size = new Size(151, 69);
            button6.TabIndex = 8;
            button6.Text = "All returned Books";
            button6.UseVisualStyleBackColor = false;
            button6.Click += button6_Click;
            // 
            // button7
            // 
            button7.BackColor = Color.Khaki;
            button7.Location = new Point(693, 179);
            button7.Name = "button7";
            button7.Size = new Size(147, 72);
            button7.TabIndex = 9;
            button7.Text = "Late books for returning";
            button7.UseVisualStyleBackColor = false;
            button7.Click += button7_Click;
            // 
            // Admin
            // 
            AutoScaleDimensions = new SizeF(11F, 22F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(931, 554);
            Controls.Add(button7);
            Controls.Add(button6);
            Controls.Add(button4);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Controls.Add(button5);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Font = new Font("Yu Gothic", 10.2F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            Margin = new Padding(4, 3, 4, 3);
            Name = "Admin";
            Text = "Admin";
            Load += Admin_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Button button2;
        private Button button3;
        private Button button5;
        private PictureBox pictureBox1;
        private Label label1;
        private Button button4;
        private Button button6;
        private Button button7;
    }
}