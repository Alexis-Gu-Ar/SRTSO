﻿using System;
using System.Collections.Generic;
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

        public SchedulerSRT()
        {
            newProcesses = new List<MyProcess>();
            runningProcess = null;
            observers = new List<IObserver<SchedulerSRT>>();
        }

        public int TotalProcesses { get => TotalNewProcesses + (HasRunningProcess ? 1 : 0); }
        public bool HasRunningProcess { get => runningProcess != null; }
        public int TotalNewProcesses { get => newProcesses.Count; }
        public MyProcess RunningProcess { get => runningProcess; }
        public List<MyProcess> NewProcesses { get => newProcesses; }

        public void AddRandomProcesses(int total)
        {
            for (int i = 0; i < total; i++)
                newProcesses.Add(new MyProcess(random.Next(30, 300)*SLEEP_INTERVAL));
            Notify();
        }

        public void Start()
        {
            Setup();
            new Thread(Run).Start();
        }

        private void Setup()
        {
            if (!HasRunningProcess)
                runningProcess = PopNewShortestProcess();
            Notify();
        }

        private void Run()
        {
            while (HasRunningProcess)
            {
                Thread.Sleep(SLEEP_INTERVAL);
                runningProcess.decreasePenddingExecutionTime(SLEEP_INTERVAL);
                if (runningProcess.HasFinished)
                    runningProcess = PopNewShortestProcess();
                Notify();
            }
        }

        private MyProcess PopNewShortestProcess()
        {
            if (newProcesses.Count == 0) return null; 
            MyProcess shortest = new MyProcess(int.MaxValue);
            foreach (MyProcess process in newProcesses)
                if (shortest.TOTAL_CPU_EXECUTION_TIME > process.TOTAL_CPU_EXECUTION_TIME)
                    shortest = process;

            newProcesses.Remove(shortest);
            return shortest;
        }

        public void AddProcesses(int executionTime)
        {
            MyProcess process = new MyProcess(executionTime);

            if(HasRunningProcess && process.PendingCpuExecutionTime < runningProcess.PendingCpuExecutionTime)
            {
                newProcesses.Add(runningProcess);
                runningProcess = process;
            }
            else newProcesses.Add(new MyProcess(executionTime));

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

    }
}