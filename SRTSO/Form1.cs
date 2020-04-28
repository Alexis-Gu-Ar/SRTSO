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

        private void buttonAddProcess_Click(object sender, EventArgs e)
        {
            scheduler.AddProcess(int.Parse(textBoxMiliseconds.Text));
        }
    }
}
