namespace ShamanHelper
{
    partial class MainWindow
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.todo_title = new System.Windows.Forms.Label();
            this.todo_1 = new System.Windows.Forms.Label();
            this.todo_2 = new System.Windows.Forms.Label();
            this.todo_3 = new System.Windows.Forms.Label();
            this.todo_4 = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.SuspendLayout();
            // 
            // todo_title
            // 
            this.todo_title.AutoEllipsis = true;
            this.todo_title.AutoSize = true;
            this.todo_title.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.todo_title.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.todo_title.ForeColor = System.Drawing.SystemColors.ControlText;
            this.todo_title.Location = new System.Drawing.Point(35, 9);
            this.todo_title.MaximumSize = new System.Drawing.Size(360, 0);
            this.todo_title.MinimumSize = new System.Drawing.Size(300, 0);
            this.todo_title.Name = "todo_title";
            this.todo_title.Size = new System.Drawing.Size(354, 16);
            this.todo_title.TabIndex = 0;
            this.todo_title.Text = "ДЛЯ ТОГО, ЧТОБЫ ЗАШАМАНИТЬ ЗАКЛИНАНИЯ";
            this.todo_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // todo_1
            // 
            this.todo_1.AutoEllipsis = true;
            this.todo_1.AutoSize = true;
            this.todo_1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.todo_1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.todo_1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.todo_1.Location = new System.Drawing.Point(12, 44);
            this.todo_1.MaximumSize = new System.Drawing.Size(500, 0);
            this.todo_1.MinimumSize = new System.Drawing.Size(300, 0);
            this.todo_1.Name = "todo_1";
            this.todo_1.Size = new System.Drawing.Size(300, 16);
            this.todo_1.TabIndex = 1;
            this.todo_1.Text = "1. Сверните это окно";
            this.todo_1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // todo_2
            // 
            this.todo_2.AutoEllipsis = true;
            this.todo_2.AutoSize = true;
            this.todo_2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.todo_2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.todo_2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.todo_2.Location = new System.Drawing.Point(12, 60);
            this.todo_2.MaximumSize = new System.Drawing.Size(500, 0);
            this.todo_2.MinimumSize = new System.Drawing.Size(300, 0);
            this.todo_2.Name = "todo_2";
            this.todo_2.Size = new System.Drawing.Size(300, 16);
            this.todo_2.TabIndex = 2;
            this.todo_2.Text = "2. Откройте нужное окно с игрой";
            this.todo_2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // todo_3
            // 
            this.todo_3.AutoEllipsis = true;
            this.todo_3.AutoSize = true;
            this.todo_3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.todo_3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.todo_3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.todo_3.Location = new System.Drawing.Point(12, 76);
            this.todo_3.MaximumSize = new System.Drawing.Size(500, 0);
            this.todo_3.MinimumSize = new System.Drawing.Size(300, 0);
            this.todo_3.Name = "todo_3";
            this.todo_3.Size = new System.Drawing.Size(400, 16);
            this.todo_3.TabIndex = 3;
            this.todo_3.Text = "3. Войдите в ритуал активировав [Камень Силы] на себя";
            this.todo_3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // todo_4
            // 
            this.todo_4.AutoEllipsis = true;
            this.todo_4.AutoSize = true;
            this.todo_4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.todo_4.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.todo_4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.todo_4.Location = new System.Drawing.Point(12, 92);
            this.todo_4.MaximumSize = new System.Drawing.Size(500, 0);
            this.todo_4.MinimumSize = new System.Drawing.Size(300, 0);
            this.todo_4.Name = "todo_4";
            this.todo_4.Size = new System.Drawing.Size(371, 16);
            this.todo_4.TabIndex = 4;
            this.todo_4.Text = "4. Начните ритуал использовав [Камень Cилы] в пол";
            this.todo_4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Siala Shaman Helper";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.Click += new System.EventHandler(this.notifyIcon1_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 135);
            this.Controls.Add(this.todo_4);
            this.Controls.Add(this.todo_3);
            this.Controls.Add(this.todo_2);
            this.Controls.Add(this.todo_1);
            this.Controls.Add(this.todo_title);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Siala Shaman Helper v1.1.0";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.Resize += new System.EventHandler(this.MainWindow_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label todo_title;
        private System.Windows.Forms.Label todo_1;
        private System.Windows.Forms.Label todo_2;
        private System.Windows.Forms.Label todo_3;
        private System.Windows.Forms.Label todo_4;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
    }
}

