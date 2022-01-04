namespace K2Utilities
{
    public abstract class Chore<T>
    {
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger("Chore");

        // Other managed resource this class uses.
        protected T workItem;

        // String to hold log output
        protected string ChoreLog;

        // The class constructor.
        public Chore(T workItem)
        {
            this.workItem = workItem;
        }

        protected abstract bool PreCondition();

        public abstract bool Execute();

        protected abstract bool PostCondition();

        public string Log()
        {
            return ChoreLog;
        }
    }
}
