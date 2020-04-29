using Microsoft.VisualStudio.TestTools.UnitTesting;
using SRTSO;
using System.Threading;
using System.Collections.Generic;
using System;
using System.Linq;

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

        private void WaithToFinish()
        {
            while (scheduler.HasRunningProcess)
                SleepUntilCurrentProcessFinishes();
        }

        private MyProcess WaitForProcessWithExecutionOfToFinish(int executionTime)
        {
            MyProcess process;
            StartWithProcessWithCpuExecutionOf(executionTime);
            process = scheduler.RunningProcess;
            SleepUntilCurrentProcessFinishes();
            return process;
        }

        private void AddAProcessWithCpuExecutionOf(params int[] executionTimes)
        {
            foreach (int executionTime in executionTimes)
                scheduler.AddProcess(executionTime);
        }

        private void StartWithProcessWithCpuExecutionOf(params int[] executionTimes)
        {
            AddAProcessWithCpuExecutionOf(executionTimes);
            scheduler.Start();
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
            StartWithProcessWithCpuExecutionOf(800, 900, 800);
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
            StartWithProcessWithCpuExecutionOf(900, 800, 700);
            SleepUntilCurrentProcessFinishes();
            Assert.AreEqual(800, scheduler.RunningProcess.TOTAL_CPU_EXECUTION_TIME);
        }

        [TestMethod]
        public void ShouldSwapTheRunningProcessWhenANewProcessWithAShorterTimeIsAdded()
        {
            StartWithProcessWithCpuExecutionOf(100 * SchedulerSRT.SLEEP_INTERVAL);
            MyProcess prevProcess = scheduler.RunningProcess;
            AddAProcessWithCpuExecutionOf(70 * SchedulerSRT.SLEEP_INTERVAL);
            Assert.AreNotEqual(prevProcess, scheduler.RunningProcess);
            Assert.IsNotNull(scheduler.RunningProcess);
            prevProcess = scheduler.RunningProcess;
            AddAProcessWithCpuExecutionOf(40 * SchedulerSRT.SLEEP_INTERVAL);
            Assert.AreNotEqual(prevProcess, scheduler.RunningProcess);
            Assert.IsNotNull(scheduler.RunningProcess);
        }

        [TestMethod]
        public void StopwatchShouldNotBeRunninIfTheSchedulerIsNotRunning()
        {
            Assert.IsFalse(scheduler.IsStopwatchRunning);
        }

        [TestMethod]
        public void StopwatchShouldBeRunningIfTheSchedulerIsRunning()
        {
            StartWithRandomProcesses(3);
            Assert.IsTrue(scheduler.IsStopwatchRunning);
        }

        [TestMethod]
        public void StopWatchShouldNotBeRunninWhenTheSchedulerFinish()
        {
            StartWithProcessWithCpuExecutionOf(720);
            Thread.Sleep(1500);
            Assert.IsFalse(scheduler.IsStopwatchRunning);
        }

        [TestMethod]
        public void BussyTimeShouldBeEqualToTheSumOfTheExecutionTimeOfTheProcess()
        {
            StartWithProcessWithCpuExecutionOf(720, 720, 720);
            Thread.Sleep(3000);
            Assert.AreEqual(720 * 3, scheduler.BussyTime);
        }

        [TestMethod]
        public void BussyTimeShoulBeZeroIfSchedulerHasNotStarted()
        {
            Assert.AreEqual(0, scheduler.BussyTime);
        }

        [TestMethod]
        public void ResponseTimeOfFirstProcessShouldBeZero()
        {
            StartWithRandomProcesses(1);
            Assert.AreEqual(0, scheduler.RunningProcess.ResponseTime);
        }

        [TestMethod]
        public void ResponseTimeOfSecondProcessShoudNotBeZero()
        {
            StartWithRandomProcesses(2);
            SleepUntilCurrentProcessFinishes();
            Assert.AreNotEqual(0, scheduler.RunningProcess.ResponseTime);
        }

        [TestMethod]
        public void MinResponseTimeShouldBeZeroWhenStaringWithOneProcess()
        {
            StartWithRandomProcesses(1);
            Assert.AreEqual(0, scheduler.MinResponseTime);
        }

        [TestMethod]
        public void MaxResponseTimeShouldBeZeroWhenStaringWithOneProcess()
        {
            StartWithRandomProcesses(1);
            Assert.AreEqual(0, scheduler.MaxResponseTime);
        }

        [TestMethod]
        public void MaxResponseTimeShouldBeGreaterThanZeroWhenStartingWithTwoProcesses()
        {
            StartWithRandomProcesses(2);
            SleepUntilCurrentProcessFinishes();
            Assert.IsTrue(0 < scheduler.MaxResponseTime);
        }

        [TestMethod]
        public void MaxResponseTimeCanNotDeacrease()
        {
            StartWithProcessWithCpuExecutionOf(720, 1200);
            SleepUntilCurrentProcessFinishes();
            double prevMaxResponse = scheduler.MaxResponseTime;
            AddAProcessWithCpuExecutionOf(720);
            Assert.IsTrue(scheduler.MaxResponseTime >= prevMaxResponse);
        }

        [TestMethod]
        public void ResponseMeanShouldBeZeroWhenStartingWithOneProcess()
        {
            StartWithRandomProcesses(1);
            Assert.AreEqual(0, scheduler.MeanResponseTime);
        }
        
        [TestMethod]
        public void WhenSchedulerFinishTheResponseMeanShouldBeEqualToTheMeanOfAllTheProcess()
        {
            StartWithRandomProcesses(3);
            MyProcess p1 = scheduler.RunningProcess;
            MyProcess p2 = scheduler.NewProcesses[0];
            MyProcess p3 = scheduler.NewProcesses[1];
            for (int i = 0; i < 3; i++)
                SleepUntilCurrentProcessFinishes();

            double mean = (p1.ResponseTime + p2.ResponseTime + p3.ResponseTime) / 3.0;
            Assert.AreEqual(mean, scheduler.MeanResponseTime);
        }

        [TestMethod]
        [ExpectedException(typeof(MyProcess.ProcessAlreadyFinished))]
        public void CanNotDecreaseThePendingExecutionOfAFinishedProcess()
        {
            MyProcess process = WaitForProcessWithExecutionOfToFinish(720);
            process.decreasePenddingExecutionTime(10);
        }

        [TestMethod]
        [ExpectedException(typeof(MyProcess.ProcessHasNotExited))]
        public void CanNotGetTheExitTimeOfAnUnfinishedProcess()
        {
            StartWithProcessWithCpuExecutionOf(720);
            _ = scheduler.RunningProcess.ExitTime;
        }

        [TestMethod]
        public void AJustFinishedProcessShouldHaveAExitCloseToTheTimeOfTheScheduler()
        {
            double delta = 400;
            MyProcess process = WaitForProcessWithExecutionOfToFinish(720);
            Assert.IsTrue(scheduler.CPUTotalTime - delta <= process.ExitTime 
                && process.ExitTime <= scheduler.CPUTotalTime);
        }

        [TestMethod]
        [ExpectedException(typeof(MyProcess.ProcessHasNotExited))]
        public void UnfinishedProcessCanNotHaveTurnAroundTime()
        {
            StartWithRandomProcesses(1);
            _ = scheduler.RunningProcess.TurnaroundTime;
        }

        [TestMethod]
        public void TheFirstProcessShouldHaveEqualTurnaroundAndExitTimeEqual()
        {
            MyProcess process = WaitForProcessWithExecutionOfToFinish(720);
            Assert.AreEqual(process.TurnaroundTime, process.ExitTime);
        }

        [TestMethod]
        public void TheTurnAroundTimeOfANewlyAddedSmallerProcessShouldBeSmaller()
        {
            MyProcess p1 = WaitForProcessWithExecutionOfToFinish(2100);
            MyProcess p2 = WaitForProcessWithExecutionOfToFinish(720);
            Assert.IsTrue(p2.TurnaroundTime < p1.TurnaroundTime);
        }

        [TestMethod]
        public void ItShouldHaveTurnaroundMinEqualToInfiniteAtBegining()
        {
            Assert.AreEqual(double.MaxValue, scheduler.MinTurnaround);
        }

        [TestMethod]
        public void ItShouldHaveTurnaroundMaxEqualToZeroAtBegining()
        {
            Assert.AreEqual(0, scheduler.MaxTurnaround);
        }

        [TestMethod]
        public void ItShouldHaveTheSameMinAndMaxTurnaroundTimeWhenFinishingOneProcess()
        {
            WaitForProcessWithExecutionOfToFinish(720);
            Assert.AreEqual(scheduler.MinTurnaround, scheduler.MaxTurnaround);
        }

        [TestMethod]
        public void ItShouldHaveDifferentMinAndMaxTurnaroundTimeWhenDoingTwoDifferentProcesses()
        {
            WaitForProcessWithExecutionOfToFinish(2100);
            WaitForProcessWithExecutionOfToFinish(720);
            Assert.AreNotEqual(scheduler.MinTurnaround, scheduler.MaxTurnaround);
        }

        [TestMethod]
        public void ShouldHaveA3TurnAroundTimeAfterItFinished()
        {
            StartWithRandomProcesses(3);
            WaithToFinish();
            Assert.AreEqual(3, scheduler.TotalResponseTimes);
        }

        [TestMethod]
        public void ShouldBeCapableOfMeasuringTheMeanOfTurnaround()
        {
            List<double> times = new List<double>();
            for (int i = 0; i < 3; i++)
                times.Add(WaitForProcessWithExecutionOfToFinish(720).TurnaroundTime);

            Assert.AreEqual(times.Sum() / times.Count, scheduler.MeanTurnaround);
        }

    }
}
