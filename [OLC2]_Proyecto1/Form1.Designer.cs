
namespace _OLC2__Proyecto1
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
            this.label1 = new System.Windows.Forms.Label();
            this.LinearNumberCompiPascal = new System.Windows.Forms.RichTextBox();
            this.CompiPascal = new System.Windows.Forms.RichTextBox();
            this.LinearNumberPascal = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Pascal = new System.Windows.Forms.RichTextBox();
            this.Consola = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.debuggerConsole = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Comic Sans MS", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(538, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(216, 45);
            this.label1.TabIndex = 0;
            this.label1.Text = "Compi Pascal";
            // 
            // LinearNumberCompiPascal
            // 
            this.LinearNumberCompiPascal.BackColor = System.Drawing.SystemColors.Control;
            this.LinearNumberCompiPascal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LinearNumberCompiPascal.Cursor = System.Windows.Forms.Cursors.PanNE;
            this.LinearNumberCompiPascal.ForeColor = System.Drawing.Color.SteelBlue;
            this.LinearNumberCompiPascal.Location = new System.Drawing.Point(12, 80);
            this.LinearNumberCompiPascal.Name = "LinearNumberCompiPascal";
            this.LinearNumberCompiPascal.ReadOnly = true;
            this.LinearNumberCompiPascal.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.LinearNumberCompiPascal.Size = new System.Drawing.Size(43, 417);
            this.LinearNumberCompiPascal.TabIndex = 1;
            this.LinearNumberCompiPascal.Text = "";
            this.LinearNumberCompiPascal.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LinearNumberCompiPascal_MouseDown);
            // 
            // CompiPascal
            // 
            this.CompiPascal.AcceptsTab = true;
            this.CompiPascal.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CompiPascal.Location = new System.Drawing.Point(61, 80);
            this.CompiPascal.Name = "CompiPascal";
            this.CompiPascal.Size = new System.Drawing.Size(542, 417);
            this.CompiPascal.TabIndex = 2;
            this.CompiPascal.Text = "";
            this.CompiPascal.WordWrap = false;
            this.CompiPascal.SelectionChanged += new System.EventHandler(this.CompiPascal_SelectionChanged);
            this.CompiPascal.VScroll += new System.EventHandler(this.CompiPascal_VScroll);
            this.CompiPascal.TextChanged += new System.EventHandler(this.CompiPascal_TextChanged);
            // 
            // LinearNumberPascal
            // 
            this.LinearNumberPascal.BackColor = System.Drawing.SystemColors.Control;
            this.LinearNumberPascal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LinearNumberPascal.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.LinearNumberPascal.Location = new System.Drawing.Point(700, 75);
            this.LinearNumberPascal.Name = "LinearNumberPascal";
            this.LinearNumberPascal.ReadOnly = true;
            this.LinearNumberPascal.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.LinearNumberPascal.Size = new System.Drawing.Size(54, 417);
            this.LinearNumberPascal.TabIndex = 3;
            this.LinearNumberPascal.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(73, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Compi Pascal:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(760, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Pascal:";
            // 
            // Pascal
            // 
            this.Pascal.AcceptsTab = true;
            this.Pascal.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Pascal.Location = new System.Drawing.Point(760, 75);
            this.Pascal.Name = "Pascal";
            this.Pascal.Size = new System.Drawing.Size(542, 417);
            this.Pascal.TabIndex = 6;
            this.Pascal.Text = "";
            this.Pascal.WordWrap = false;
            this.Pascal.SelectionChanged += new System.EventHandler(this.Pascal_SelectionChanged);
            this.Pascal.VScroll += new System.EventHandler(this.Pascal_VScroll);
            this.Pascal.TextChanged += new System.EventHandler(this.Pascal_TextChanged);
            this.Pascal.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Pascal_MouseDown);
            // 
            // Consola
            // 
            this.Consola.BackColor = System.Drawing.SystemColors.Info;
            this.Consola.Location = new System.Drawing.Point(380, 531);
            this.Consola.Name = "Consola";
            this.Consola.Size = new System.Drawing.Size(922, 276);
            this.Consola.TabIndex = 7;
            this.Consola.Text = "";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(380, 513);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "Consola:";
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Location = new System.Drawing.Point(80, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "Ejecutar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Location = new System.Drawing.Point(9, 9);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 10;
            this.button2.Text = "Traducir";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.Location = new System.Drawing.Point(151, 9);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(147, 23);
            this.button3.TabIndex = 11;
            this.button3.Text = "Reporte Errores";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // debuggerConsole
            // 
            this.debuggerConsole.BackColor = System.Drawing.SystemColors.ControlLight;
            this.debuggerConsole.Location = new System.Drawing.Point(9, 531);
            this.debuggerConsole.Name = "debuggerConsole";
            this.debuggerConsole.ReadOnly = true;
            this.debuggerConsole.Size = new System.Drawing.Size(365, 276);
            this.debuggerConsole.TabIndex = 12;
            this.debuggerConsole.Text = "";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 513);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 15);
            this.label5.TabIndex = 13;
            this.label5.Text = "Debugger";
            // 
            // button4
            // 
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button4.Location = new System.Drawing.Point(295, 9);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(109, 23);
            this.button4.TabIndex = 14;
            this.button4.Text = "Reporte Simbolos";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button5.Location = new System.Drawing.Point(401, 9);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(69, 23);
            this.button5.TabIndex = 15;
            this.button5.Text = "AST";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1327, 819);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.debuggerConsole);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Consola);
            this.Controls.Add(this.Pascal);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.LinearNumberPascal);
            this.Controls.Add(this.CompiPascal);
            this.Controls.Add(this.LinearNumberCompiPascal);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox LinearNumberCompiPascal;
        private System.Windows.Forms.RichTextBox CompiPascal;
        private System.Windows.Forms.RichTextBox LinearNumberPascal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox Pascal;
        private System.Windows.Forms.RichTextBox Consola;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.RichTextBox debuggerConsole;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
    }
}

