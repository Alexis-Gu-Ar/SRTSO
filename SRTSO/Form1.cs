using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SRTSO
{
    public partial class Form1 : Form, IObserver<SchedulerSRT>
    {
        private SchedulerSRT scheduler;
        private Series seriesPenddingExecution,
            seriesExecuted;
        public Form1()
        {
            InitializeComponent();

            chart1.Series.Clear();

            textBoxMiliseconds.Text = "720";
            textBoxTotalNewProcess.Text = "5";

            seriesPenddingExecution = new Series();
            seriesPenddingExecution.Name = "Ejecución pendiente";
            seriesPenddingExecution.Color = Color.BlueViolet;
            seriesPenddingExecution.ChartType = SeriesChartType.StackedColumn;

            seriesExecuted = new Series();
            seriesExecuted.Name = "Ejecutado";
            seriesExecuted.Color = Color.LawnGreen;
            seriesExecuted.ChartType = SeriesChartType.StackedColumn;

            chart1.Series.Add(seriesPenddingExecution);
            chart1.Series.Add(seriesExecuted);
            scheduler = new SchedulerSRT();
            scheduler.Subscribe(this);
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        private delegate void SafeUpdateChart(List<int> yValues, Series series);

        public void OnNext(SchedulerSRT value)
        {
            List<int> yPendingValues = new List<int>();
            List<int> yExecutedValues = new List<int>();
            for (int i = 0; i < scheduler.NewProcesses.Count; i++)
            {
                MyProcess process = scheduler.NewProcesses[i];
                yPendingValues.Add(process.PendingCpuExecutionTime);
                yExecutedValues.Add(process.ExecutedTime);
            }
            if (scheduler.HasRunningProcess)
            {
                yPendingValues.Add(scheduler.RunningProcess.PendingCpuExecutionTime);
                yExecutedValues.Add(scheduler.RunningProcess.ExecutedTime);
            }
            UpdatePendingExecutionSeries(yPendingValues, seriesPenddingExecution);
            UpdatePendingExecutionSeries(yExecutedValues, seriesExecuted);
            UpdateLabel(labelCPUTime, scheduler.CPUTotalTime.ToString());
            UpdateLabel(labelCPUBusy, scheduler.BussyTime.ToString());
            UpdateLabel(labelCPUIDLE, scheduler.IDLETime.ToString());
            UpdateLabel(labelResponseMin, scheduler.MinResponseTime.ToString());
            UpdateLabel(labelResponseMax, scheduler.MaxResponseTime.ToString());
            if (scheduler.HasResponseTimes)
            {
                UpdateLabel(labelResponseMean, scheduler.MeanResponseTime.ToString());
                UpdateLabel(labelResponseEstandardDeviation, scheduler.StandardDeviation.ToString());
            }
            
        }
        private delegate void SafeUpdateLabel(Label label, String text);
        private void UpdateLabel(Label label, string text)
        {
            if (label.InvokeRequired)
            {
                var d = new SafeUpdateLabel(UpdateLabel);
                label.Invoke(d, new object[] { label, text });
            }
            else label.Text = text;
        }

        private void UpdatePendingExecutionSeries(List<int> yValues, Series series)
        {
            if (chart1.InvokeRequired)
            {
                var d = new SafeUpdateChart(UpdatePendingExecutionSeries);
                chart1.Invoke(d, new object[] { yValues, series });
            }
            else series.Points.DataBindY(yValues.ToArray());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(!scheduler.HasRunningProcess)
                scheduler.Start();
        }

        private void buttonAddProcesses_Click(object sender, EventArgs e)
        {
            scheduler.AddRandomProcesses(int.Parse(textBoxTotalNewProcess.Text));
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonAddProcess_Click(object sender, EventArgs e)
        {
            scheduler.AddProcess(int.Parse(textBoxMiliseconds.Text));
        }
    }
}
