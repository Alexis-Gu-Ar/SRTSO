﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SRTSO
{
    public class SchedulerSRT : IObservable<SchedulerSRT>
    {
        private static readonly Random random = new Random(DateTime.Now.Millisecond);
        List<MyProcess> newProcesses;
        MyProcess runningProcess;
        private List<IObserver<SchedulerSRT>> observers;
        public const int SLEEP_INTERVAL = 30;
        private double minTurnAround;
        private double maxTurnAround;
        private Stopwatch stopwatch;
        private Thread startThread;
        private int bussyTime;
        private double maxResponseTime;
        private List<double> responseTimes;
        private List<double> turnaroundTimes;

        public SchedulerSRT()
        {
            bussyTime = 0;
            maxTurnAround = 0;
            minTurnAround = double.MaxValue;
            responseTimes = new List<double>();
            turnaroundTimes = new List<double>();
            startThread = new Thread(Run);
            stopwatch = new Stopwatch();
            newProcesses = new List<MyProcess>();
            runningProcess = null;
            observers = new List<IObserver<SchedulerSRT>>();
        }

        public int TotalProcesses { get => TotalNewProcesses + (HasRunningProcess ? 1 : 0); }
        public bool HasRunningProcess { get => runningProcess != null; }
        public int TotalNewProcesses { get => newProcesses.Count; }
        public MyProcess RunningProcess { get => runningProcess; }
        public List<MyProcess> NewProcesses { get => newProcesses; }
        public bool IsStopwatchRunning { get => stopwatch.IsRunning; }

        public double CPUTotalTime { get => stopwatch.ElapsedMilliseconds; }
        public int BussyTime { get => bussyTime; }
        public double IDLETime { get => CPUTotalTime - BussyTime; }
        public bool HaveNewProcesses { get => newProcesses.Count > 0; }
        public int MinResponseTime { get => 0; }
        public double MaxResponseTime { get => maxResponseTime; }
        public double MeanResponseTime { get => responseTimes.Sum() / responseTimes.Count; }


        public double ResponseTimeStandardDeviation
        {
            get
            {
                double standardDeviation = 0;
                double mean = MeanResponseTime;
                foreach (int responseTime in responseTimes)
                    standardDeviation += Math.Pow(responseTime - mean, 2);

                standardDeviation /= responseTimes.Count;
                return Math.Sqrt(standardDeviation);
            }
        }
        public bool HasResponseTimes { get => responseTimes.Count > 0; }
        public double MinTurnaround { get => minTurnAround; }
        public double MaxTurnaround { get => maxTurnAround; }
        public bool HasTurnaroundTimes { get => turnaroundTimes.Count > 0; }
        public int TotalResponseTimes { get => turnaroundTimes.Count; }
        public double MeanTurnaround { get => turnaroundTimes.Sum()/turnaroundTimes.Count; }
        public double TurnAroundStandardDeviation { get => calculateStandardDeviation(turnaroundTimes); }

        public void AddRandomProcesses(int total)
        {
            for (int i = 0; i < total; i++)
                AddProcess(random.Next(30, 300)*SLEEP_INTERVAL);
            Notify();
        }

        public void Start()
        {
            Setup();
            if(!startThread.IsAlive)
                startThread = new Thread(Run);

            stopwatch.Start();
            startThread.Start();
        }

        private void Setup()
        {
            if (!HasRunningProcess)
                SetRunningProcess(PopNewShortestProcess());
            Notify();
        }

        private void Run()
        {
            while (HasRunningProcess)
            {
                Thread.Sleep(SLEEP_INTERVAL);
                runningProcess.decreasePenddingExecutionTime(SLEEP_INTERVAL, stopwatch.ElapsedMilliseconds);
                bussyTime += SLEEP_INTERVAL;
                if (runningProcess.HasFinished)
                {
                    turnaroundTimes.Add(runningProcess.TurnaroundTime);
                    SetRunningProcess(PopNewShortestProcess());
                }

                Notify();
            }
            stopwatch.Stop();
        }

        private void SetRunningProcess(MyProcess process)
        {
            UpdateTurnaround();
            runningProcess = process;
            if (runningProcess != null && !runningProcess.HasBeenInCPU)
            {
                runningProcess.FirstTimeInCPU = stopwatch.ElapsedMilliseconds;
                responseTimes.Add(runningProcess.ResponseTime);
            }
            UpdateMaxResponseTime();
        }

        private void UpdateTurnaround()
        {
            if(runningProcess != null && runningProcess.HasFinished)
            {
                double time = runningProcess.TurnaroundTime;
                minTurnAround = time < minTurnAround ? time : minTurnAround;
                maxTurnAround = time > maxTurnAround ? time : maxTurnAround;
            }
        }

        private void UpdateMaxResponseTime()
        {
            if (runningProcess != null /*&& !runningProcess.HasBeenInCPU*/)
            {
                maxResponseTime = runningProcess.ResponseTime > maxResponseTime ? runningProcess.ResponseTime : maxResponseTime;
            }
        }

        private MyProcess PopNewShortestProcess()
        {
            if (newProcesses.Count == 0) return null;
            MyProcess shortest = new MyProcess(int.MaxValue, -1);
            foreach (MyProcess process in newProcesses)
                if (shortest.TOTAL_CPU_EXECUTION_TIME > process.TOTAL_CPU_EXECUTION_TIME)
                    shortest = process;

            newProcesses.Remove(shortest);
            return shortest;
        }

        public void AddProcess(int executionTime)
        {
            MyProcess process = new MyProcess(executionTime, stopwatch.ElapsedMilliseconds);

            if(HasRunningProcess && process.PendingCpuExecutionTime < runningProcess.PendingCpuExecutionTime)
            {
                newProcesses.Add(runningProcess);
                SetRunningProcess(process);
            }
            else newProcesses.Add(process);

            Notify();
        }

        public IDisposable Subscribe(IObserver<SchedulerSRT> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);

            return new Unsubscriber(observers, observer);
        }

        private class Unsubscriber : IDisposable
        {
            private List<IObserver<SchedulerSRT>> _observers;
            private IObserver<SchedulerSRT> _observer;

            public Unsubscriber(List<IObserver<SchedulerSRT>> observers, IObserver<SchedulerSRT> observer)
            {
                this._observers = observers;
                this._observer = observer;
            }

            public void Dispose()
            {
                if (!(_observer == null)) _observers.Remove(_observer);
            }
        }

        private void Notify()
        {
            foreach (IObserver<SchedulerSRT> observer in observers)
                observer.OnNext(this);
        }

        private static double calculateStandardDeviation(List<double> values)
        {
            double mean = values.Sum() / values.Count();
            double standardDeviation = 0;
            foreach (double x in values)
                standardDeviation += Math.Pow(x - mean, 2);
            standardDeviation /= values.Count;
            return Math.Sqrt(standardDeviation);
        }
    }
}
