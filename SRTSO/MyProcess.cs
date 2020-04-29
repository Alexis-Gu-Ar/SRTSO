using System;
using System.Runtime.Serialization;
using System.Windows.Forms;
using System.Windows.Markup;

namespace SRTSO
{
    public class MyProcess
    {
        public readonly int TOTAL_CPU_EXECUTION_TIME;
        private int pendingExecutionTime;
        private readonly double arrivalTime;
        private double? firstTimeInCpu;
        private double? exitTime;

        public MyProcess(int cpuExecutionTime, double arrivalTime)
        {
            exitTime = null;
            firstTimeInCpu = null;
            pendingExecutionTime = TOTAL_CPU_EXECUTION_TIME = cpuExecutionTime;
            this.arrivalTime = arrivalTime;
        }

        public void decreasePenddingExecutionTime(int miliseconds, double? currentTime = null)
        {
            if (HasFinished) throw new ProcessAlreadyFinished();
            pendingExecutionTime -= miliseconds;
            if (HasFinished)
            {
                if (currentTime == null)
                    throw new ArgumentException("currentTime not provided");
                exitTime = currentTime;
            }
        }

        public int PendingCpuExecutionTime { get => pendingExecutionTime; }
        public bool HasFinished { get => pendingExecutionTime <= 0; }
        public int ExecutedTime { get => TOTAL_CPU_EXECUTION_TIME - pendingExecutionTime; }
        public double ResponseTime { get => FirstTimeInCPU - arrivalTime; }
        public bool HasBeenInCPU { get => firstTimeInCpu != null; }
        public double FirstTimeInCPU
        {
            set 
            {
                if (HasBeenInCPU)
                    throw new FirstTimeInCPUAlreadySet();
                firstTimeInCpu = value;
            }
            get
            {
                if (!HasBeenInCPU) throw new FirstTimeInCPUIsNotSet();
                return firstTimeInCpu.Value;
            }
        }

        public double ExitTime 
        { 
            get {
                if (!HasFinished)
                    throw new ProcessHasNotExited();
                return exitTime.Value;
            } 
        }

        public double TurnaroundTime { 
            get
            {
                if (!HasFinished) throw new ProcessHasNotExited();
                return ExitTime - arrivalTime;
            } 
        }

        [Serializable]
        private class FirstTimeInCPUAlreadySet : Exception
        {
            public FirstTimeInCPUAlreadySet()
            {
            }

            public FirstTimeInCPUAlreadySet(string message) : base(message)
            {
            }

            public FirstTimeInCPUAlreadySet(string message, Exception innerException) : base(message, innerException)
            {
            }

            protected FirstTimeInCPUAlreadySet(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }

        [Serializable]
        private class FirstTimeInCPUIsNotSet : Exception
        {
            public FirstTimeInCPUIsNotSet()
            {
            }

            public FirstTimeInCPUIsNotSet(string message) : base(message)
            {
            }

            public FirstTimeInCPUIsNotSet(string message, Exception innerException) : base(message, innerException)
            {
            }

            protected FirstTimeInCPUIsNotSet(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }

        [Serializable]
        public class ProcessAlreadyFinished : Exception
        {
            public ProcessAlreadyFinished()
            {
            }

            public ProcessAlreadyFinished(string message) : base(message)
            {
            }

            public ProcessAlreadyFinished(string message, Exception innerException) : base(message, innerException)
            {
            }

            protected ProcessAlreadyFinished(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }

        [Serializable]
        public class ProcessHasNotExited : Exception
        {
            public ProcessHasNotExited()
            {
            }

            public ProcessHasNotExited(string message) : base(message)
            {
            }

            public ProcessHasNotExited(string message, Exception innerException) : base(message, innerException)
            {
            }

            protected ProcessHasNotExited(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
    }
}
