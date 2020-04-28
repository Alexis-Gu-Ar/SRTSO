using System;
using System.Runtime.Serialization;
using System.Windows.Markup;

namespace SRTSO
{
    public class MyProcess
    {
        public readonly int TOTAL_CPU_EXECUTION_TIME;
        private int pendingExecutionTime;
        private readonly double arrivalTime;
        private double? firstTimeInCpu;

        public MyProcess(int cpuExecutionTime, double arrivalTime)
        {
            firstTimeInCpu = null;
            pendingExecutionTime = TOTAL_CPU_EXECUTION_TIME = cpuExecutionTime;
            this.arrivalTime = arrivalTime;
        }

        public void decreasePenddingExecutionTime(int miliseconds)
        {
            pendingExecutionTime -= miliseconds;
        }

        public int PendingCpuExecutionTime { get => pendingExecutionTime; }
        public bool HasFinished { get => pendingExecutionTime <= 0; }
        public int ExecutedTime { get => TOTAL_CPU_EXECUTION_TIME - pendingExecutionTime; }
        public int ResponseTime { get => (int)(firstTimeInCpu - arrivalTime); }
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
    }
}
