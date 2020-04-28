namespace SRTSO
{
    partial class Form1
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.labelCPUBusy = new System.Windows.Forms.Label();
            this.labelCPUIDLE = new System.Windows.Forms.Label();
            this.labelCPUTime = new System.Windows.Forms.Label();
            this.labelResponseEstandardDeviation = new System.Windows.Forms.Label();
            this.labelResponseMax = new System.Windows.Forms.Label();
            this.labelResponseMean = new System.Windows.Forms.Label();
            this.labelResponseMin = new System.Windows.Forms.Label();
            this.labelTurnaroundStandardDeviation = new System.Windows.Forms.Label();
            this.labelTurnaroundMax = new System.Windows.Forms.Label();
            this.labelTurnaroundMean = new System.Windows.Forms.Label();
            this.labelTurnaroundMin = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.buttonAddProcess = new System.Windows.Forms.Button();
            this.textBoxMiliseconds = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxTotalNewProcess = new System.Windows.Forms.TextBox();
            this.buttonAddProcesses = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            chartArea4.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.chart1.Legends.Add(legend4);
            this.chart1.Location = new System.Drawing.Point(12, 12);
            this.chart1.Name = "chart1";
            series4.ChartArea = "ChartArea1";
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            this.chart1.Series.Add(series4);
            this.chart1.Size = new System.Drawing.Size(908, 391);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(824, 495);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 30);
            this.button1.TabIndex = 1;
            this.button1.Text = "Iniciar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 418);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "CPU";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 445);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Tiempo";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(38, 458);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "IDLE";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(38, 471);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Ocupado";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(269, 471);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Max";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(269, 458);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Media";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(269, 445);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(24, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Min";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(269, 418);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(106, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Tiempo de respuesta";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(269, 484);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(104, 13);
            this.label9.TabIndex = 10;
            this.label9.Text = "Desviación estandar";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(487, 418);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(96, 13);
            this.label14.TabIndex = 11;
            this.label14.Text = "Tiempo turnaround";
            // 
            // labelCPUBusy
            // 
            this.labelCPUBusy.AutoSize = true;
            this.labelCPUBusy.Location = new System.Drawing.Point(112, 471);
            this.labelCPUBusy.Name = "labelCPUBusy";
            this.labelCPUBusy.Size = new System.Drawing.Size(41, 13);
            this.labelCPUBusy.TabIndex = 18;
            this.labelCPUBusy.Text = "label15";
            // 
            // labelCPUIDLE
            // 
            this.labelCPUIDLE.AutoSize = true;
            this.labelCPUIDLE.Location = new System.Drawing.Point(112, 458);
            this.labelCPUIDLE.Name = "labelCPUIDLE";
            this.labelCPUIDLE.Size = new System.Drawing.Size(41, 13);
            this.labelCPUIDLE.TabIndex = 17;
            this.labelCPUIDLE.Text = "label16";
            // 
            // labelCPUTime
            // 
            this.labelCPUTime.AutoSize = true;
            this.labelCPUTime.Location = new System.Drawing.Point(112, 445);
            this.labelCPUTime.Name = "labelCPUTime";
            this.labelCPUTime.Size = new System.Drawing.Size(74, 13);
            this.labelCPUTime.TabIndex = 16;
            this.labelCPUTime.Text = "labelCPUTime";
            // 
            // labelResponseEstandardDeviation
            // 
            this.labelResponseEstandardDeviation.AutoSize = true;
            this.labelResponseEstandardDeviation.Location = new System.Drawing.Point(379, 484);
            this.labelResponseEstandardDeviation.Name = "labelResponseEstandardDeviation";
            this.labelResponseEstandardDeviation.Size = new System.Drawing.Size(0, 13);
            this.labelResponseEstandardDeviation.TabIndex = 22;
            // 
            // labelResponseMax
            // 
            this.labelResponseMax.AutoSize = true;
            this.labelResponseMax.Location = new System.Drawing.Point(379, 471);
            this.labelResponseMax.Name = "labelResponseMax";
            this.labelResponseMax.Size = new System.Drawing.Size(0, 13);
            this.labelResponseMax.TabIndex = 21;
            // 
            // labelResponseMean
            // 
            this.labelResponseMean.AutoSize = true;
            this.labelResponseMean.Location = new System.Drawing.Point(379, 458);
            this.labelResponseMean.Name = "labelResponseMean";
            this.labelResponseMean.Size = new System.Drawing.Size(0, 13);
            this.labelResponseMean.TabIndex = 20;
            // 
            // labelResponseMin
            // 
            this.labelResponseMin.AutoSize = true;
            this.labelResponseMin.Location = new System.Drawing.Point(379, 445);
            this.labelResponseMin.Name = "labelResponseMin";
            this.labelResponseMin.Size = new System.Drawing.Size(0, 13);
            this.labelResponseMin.TabIndex = 19;
            // 
            // labelTurnaroundStandardDeviation
            // 
            this.labelTurnaroundStandardDeviation.AutoSize = true;
            this.labelTurnaroundStandardDeviation.Location = new System.Drawing.Point(597, 480);
            this.labelTurnaroundStandardDeviation.Name = "labelTurnaroundStandardDeviation";
            this.labelTurnaroundStandardDeviation.Size = new System.Drawing.Size(0, 13);
            this.labelTurnaroundStandardDeviation.TabIndex = 30;
            // 
            // labelTurnaroundMax
            // 
            this.labelTurnaroundMax.AutoSize = true;
            this.labelTurnaroundMax.Location = new System.Drawing.Point(597, 467);
            this.labelTurnaroundMax.Name = "labelTurnaroundMax";
            this.labelTurnaroundMax.Size = new System.Drawing.Size(0, 13);
            this.labelTurnaroundMax.TabIndex = 29;
            // 
            // labelTurnaroundMean
            // 
            this.labelTurnaroundMean.AutoSize = true;
            this.labelTurnaroundMean.Location = new System.Drawing.Point(597, 454);
            this.labelTurnaroundMean.Name = "labelTurnaroundMean";
            this.labelTurnaroundMean.Size = new System.Drawing.Size(0, 13);
            this.labelTurnaroundMean.TabIndex = 28;
            // 
            // labelTurnaroundMin
            // 
            this.labelTurnaroundMin.AutoSize = true;
            this.labelTurnaroundMin.Location = new System.Drawing.Point(597, 441);
            this.labelTurnaroundMin.Name = "labelTurnaroundMin";
            this.labelTurnaroundMin.Size = new System.Drawing.Size(0, 13);
            this.labelTurnaroundMin.TabIndex = 27;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(487, 480);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(104, 13);
            this.label22.TabIndex = 26;
            this.label22.Text = "Desviación estandar";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(487, 467);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(27, 13);
            this.label23.TabIndex = 25;
            this.label23.Text = "Max";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(487, 454);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(36, 13);
            this.label24.TabIndex = 24;
            this.label24.Text = "Media";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(487, 441);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(24, 13);
            this.label25.TabIndex = 23;
            this.label25.Text = "Min";
            // 
            // buttonAddProcess
            // 
            this.buttonAddProcess.Location = new System.Drawing.Point(824, 458);
            this.buttonAddProcess.Name = "buttonAddProcess";
            this.buttonAddProcess.Size = new System.Drawing.Size(98, 30);
            this.buttonAddProcess.TabIndex = 31;
            this.buttonAddProcess.Text = "Añadir proceso";
            this.buttonAddProcess.UseVisualStyleBackColor = true;
            this.buttonAddProcess.Click += new System.EventHandler(this.buttonAddProcess_Click);
            // 
            // textBoxMiliseconds
            // 
            this.textBoxMiliseconds.Location = new System.Drawing.Point(730, 468);
            this.textBoxMiliseconds.Name = "textBoxMiliseconds";
            this.textBoxMiliseconds.Size = new System.Drawing.Size(88, 20);
            this.textBoxMiliseconds.TabIndex = 32;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(730, 449);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(68, 13);
            this.label10.TabIndex = 33;
            this.label10.Text = "Milisegundos";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(730, 408);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(33, 9);
            this.label11.TabIndex = 36;
            this.label11.Text = "Cantidad";
            // 
            // textBoxTotalNewProcess
            // 
            this.textBoxTotalNewProcess.Location = new System.Drawing.Point(730, 427);
            this.textBoxTotalNewProcess.Name = "textBoxTotalNewProcess";
            this.textBoxTotalNewProcess.Size = new System.Drawing.Size(88, 20);
            this.textBoxTotalNewProcess.TabIndex = 35;
            // 
            // buttonAddProcesses
            // 
            this.buttonAddProcesses.Location = new System.Drawing.Point(824, 417);
            this.buttonAddProcesses.Name = "buttonAddProcesses";
            this.buttonAddProcesses.Size = new System.Drawing.Size(98, 30);
            this.buttonAddProcesses.TabIndex = 34;
            this.buttonAddProcesses.Text = "Añadir procesos";
            this.buttonAddProcesses.UseVisualStyleBackColor = true;
            this.buttonAddProcesses.Click += new System.EventHandler(this.buttonAddProcesses_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 527);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.textBoxTotalNewProcess);
            this.Controls.Add(this.buttonAddProcesses);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.textBoxMiliseconds);
            this.Controls.Add(this.buttonAddProcess);
            this.Controls.Add(this.labelTurnaroundStandardDeviation);
            this.Controls.Add(this.labelTurnaroundMax);
            this.Controls.Add(this.labelTurnaroundMean);
            this.Controls.Add(this.labelTurnaroundMin);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.labelResponseEstandardDeviation);
            this.Controls.Add(this.labelResponseMax);
            this.Controls.Add(this.labelResponseMean);
            this.Controls.Add(this.labelResponseMin);
            this.Controls.Add(this.labelCPUBusy);
            this.Controls.Add(this.labelCPUIDLE);
            this.Controls.Add(this.labelCPUTime);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.chart1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label labelCPUBusy;
        private System.Windows.Forms.Label labelCPUIDLE;
        private System.Windows.Forms.Label labelCPUTime;
        private System.Windows.Forms.Label labelResponseEstandardDeviation;
        private System.Windows.Forms.Label labelResponseMax;
        private System.Windows.Forms.Label labelResponseMean;
        private System.Windows.Forms.Label labelResponseMin;
        private System.Windows.Forms.Label labelTurnaroundStandardDeviation;
        private System.Windows.Forms.Label labelTurnaroundMax;
        private System.Windows.Forms.Label labelTurnaroundMean;
        private System.Windows.Forms.Label labelTurnaroundMin;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Button buttonAddProcess;
        private System.Windows.Forms.TextBox textBoxMiliseconds;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxTotalNewProcess;
        private System.Windows.Forms.Button buttonAddProcesses;
    }
}

