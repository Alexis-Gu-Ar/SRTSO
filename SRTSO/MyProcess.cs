namespace SRTSO
{
    public class MyProcess
    {
        public readonly int TOTAL_CPU_EXECUTION_TIME;
        private int pendingExecutionTime;

        public MyProcess(int cpuExecutionTime)
        {
            pendingExecutionTime = TOTAL_CPU_EXECUTION_TIME = cpuExecutionTime;
        }

        public void decreasePenddingExecutionTime(int miliseconds)
        {
            pendingExecutionTime -= miliseconds;
        }

        public int PendingCpuExecutionTime { get => pendingExecutionTime; }
        public bool HasFinished { get => pendingExecutionTime <= 0; }
    }
}
