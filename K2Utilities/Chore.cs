namespace K2Utilities
{
    public abstract class Chore<T>
    {
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
