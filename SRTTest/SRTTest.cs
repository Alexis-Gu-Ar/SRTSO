using Microsoft.VisualStudio.TestTools.UnitTesting;
using SRTSO;
using System.Threading;
using System.Collections.Generic;

namespace SRTTest
{
    [TestClass]
    public class SRTTest
    {
        private const int SMALLEST_CPU_EXECUTION_LIMIT = 700;
        private const int TIME_PAD = 60;
        SchedulerSRT scheduler;

        private void AddARandomProcess()
        {
            scheduler.AddRandomProcesses(1);
        }

        private void StartWithRandomProcesses(int total)
        {
            AddRandomProcesses(total);
            scheduler.Start();
        }

        private void SleepUntilCurrentProcessFinishes()
        {
            MyProcess process = scheduler.RunningProcess;
            while (!process.HasFinished)
                Thread.Sleep(process.PendingCpuExecutionTime + TIME_PAD);
        }

        private void AddAProcessWithCpuExecutionOf(params int[] executionTimes)
        {
            foreach (int executionTime in executionTimes)
                scheduler.AddProcesses(executionTime);
        }

        private void AddRandomProcesses(int total)
        {
            for (int i = 0; i < total; i++)
                AddARandomProcess();
        }

        private void AssertAllNewProcessAreGreaterOrEqualThan(int runningProcessExecution)
        {
            foreach (MyProcess process in scheduler.NewProcesses)
                Assert.IsTrue(runningProcessExecution <= process.TOTAL_CPU_EXECUTION_TIME);
        }

        private void AssertSchedulerTotalProcessesToBe(int Expected)
        {
            Assert.AreEqual(Expected, scheduler.TotalProcesses);
        }

        private void AssertSchedulerTotalNewProcessToBe(int total)
        {
            Assert.AreEqual(total, scheduler.TotalNewProcesses);
        }

        [TestInitialize]
        public void Initialize()
        {
            scheduler = new SchedulerSRT();
        }

        [TestMethod]
        public void ShouldNotHaveProcesses()
        {
            AssertSchedulerTotalProcessesToBe(0);
        }


        [TestMethod]
        public void ShouldNotHaveRunningProcess()
        {
            Assert.IsFalse(scheduler.HasRunningProcess);
        }

        [TestMethod]
        public void ShouldHave1ProcessAfterAdding1Process()
        {
            AddARandomProcess();
            AssertSchedulerTotalProcessesToBe(1);
        }

        [TestMethod]
        public void ShouldHave2ProcessAfterAdding1Process2Times()
        {
            AddRandomProcesses(2);
            AssertSchedulerTotalProcessesToBe(2);
        }

        [TestMethod]
        public void ShouldHaveARunningProcessAfterAddingAProcess()
        {
            StartWithRandomProcesses(1);
            Assert.IsTrue(scheduler.HasRunningProcess);
        }

        [TestMethod]
        public void ShouldHave1RunningProcessAnd1NewProcessAfterAdding2Process()
        {
            StartWithRandomProcesses(2);
            Assert.IsTrue(scheduler.HasRunningProcess);
            AssertSchedulerTotalNewProcessToBe(1);
        }


        [TestMethod]
        public void ShouldHave1RunningProcessAnd2NewProcessAfterAdding3Process()
        {
            StartWithRandomProcesses(3);
            Assert.IsTrue(scheduler.HasRunningProcess);
            AssertSchedulerTotalNewProcessToBe(2);
        }

        [TestMethod]
        public void RunningProcessShouldHaveAExecutionTimeBiggerThan700miliseconds()
        {
            AddARandomProcess();
            scheduler.Start();
            Assert.IsTrue(SMALLEST_CPU_EXECUTION_LIMIT < scheduler.RunningProcess.TOTAL_CPU_EXECUTION_TIME);
        }

        [TestMethod]
        public void NewProcessesShouldHaveATotalCpuExecutionTimeBiggerThan700miliseconds()
        {
            AddRandomProcesses(100);
            foreach (MyProcess process in scheduler.NewProcesses)
                Assert.IsTrue(SMALLEST_CPU_EXECUTION_LIMIT < process.TOTAL_CPU_EXECUTION_TIME);
        }

        [TestMethod]
        public void RunningProcessShouldHaveTheSmallestTotalCpuExecutionTime()
        {
            StartWithRandomProcesses(100);
            int runningProcessExecution = scheduler.RunningProcess.TOTAL_CPU_EXECUTION_TIME;
            AssertAllNewProcessAreGreaterOrEqualThan(runningProcessExecution);
        }

        [TestMethod]
        public void WhenRunningANewProcessItShouldHaveTheSmallestCpuExecutionAndOrBeTheLeftMostProcess()
        {
            AddAProcessWithCpuExecutionOf(800, 900, 800);
            scheduler.Start();
            List<MyProcess> newProcesses = scheduler.NewProcesses;
            Assert.AreEqual(900, newProcesses[0].TOTAL_CPU_EXECUTION_TIME);
            Assert.AreEqual(800, newProcesses[1].TOTAL_CPU_EXECUTION_TIME);
        }

        [TestMethod]
        public void WhenAddingANewProcessItShouldHaveTheSameCpuExecutionAndPendingCpuExecution()
        {
            AddRandomProcesses(1);
            MyProcess process = scheduler.NewProcesses[0];
            Assert.AreEqual(process.TOTAL_CPU_EXECUTION_TIME, process.PendingCpuExecutionTime);
        }

        [TestMethod]
        public void PendingExecutionTimeShouldDecreaseOverTime()
        {
            StartWithRandomProcesses(1);
            int pendding = scheduler.RunningProcess.PendingCpuExecutionTime;
            for (int i = 0; i < 3; i++)
            {
                int prevPending = pendding;
                Thread.Sleep(200);
                pendding = scheduler.RunningProcess.PendingCpuExecutionTime;
                Assert.IsTrue(pendding < prevPending);
            }
        }

        [TestMethod]
        public void ExecutingProcessShouldBeNullWhenItFinishWithoutNewProcess()
        {
            StartWithRandomProcesses(1);
            SleepUntilCurrentProcessFinishes();
            Assert.IsNull(scheduler.RunningProcess);
        }

        [TestMethod]
        public void ExecutingProcessShouldBeNewAfterTheCurrentOneFinish()
        {
            StartWithRandomProcesses(2);
            MyProcess prevProcess = scheduler.RunningProcess;
            SleepUntilCurrentProcessFinishes();
            Assert.AreNotEqual(scheduler.RunningProcess, prevProcess);

        }

        [TestMethod]
        public void TheNextExecutingProcessShouldBeTheShortest()
        {
            AddAProcessWithCpuExecutionOf(900, 800, 700);
            scheduler.Start();
            SleepUntilCurrentProcessFinishes();
            Assert.AreEqual(800, scheduler.RunningProcess.TOTAL_CPU_EXECUTION_TIME);
        }

        [TestMethod]
        public void ShouldSwapTheRunningProcessWhenANewProcessWithAShorterTimeIsAdded()
        {
            AddAProcessWithCpuExecutionOf(100 * SchedulerSRT.SLEEP_INTERVAL);
            scheduler.Start();
            MyProcess prevProcess = scheduler.RunningProcess;
            AddAProcessWithCpuExecutionOf(70 * SchedulerSRT.SLEEP_INTERVAL);
            Assert.AreNotEqual(prevProcess, scheduler.RunningProcess);
            Assert.IsNotNull(scheduler.RunningProcess);
            prevProcess = scheduler.RunningProcess;
            AddAProcessWithCpuExecutionOf(40 * SchedulerSRT.SLEEP_INTERVAL);
            Assert.AreNotEqual(prevProcess, scheduler.RunningProcess);
            Assert.IsNotNull(scheduler.RunningProcess);
        }
    }
}
