using System;

namespace K2Utilities
{
    public abstract class Chore<T>
    {
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Other managed resource this class uses.
        protected T workItem;

        // String to hold log output
        protected string choreLog;
        public string ChoreLog { get => choreLog; }

        // The class constructor.
        public Chore(T workItem)
        {
            this.workItem = workItem;
        }

        protected abstract bool PreCondition();

        public abstract bool Process();

        protected abstract bool PostCondition();

        protected abstract void CleanUp();

        public bool Start()
        {
            try
            {
                if (PreCondition())
                {
                    if (Process())
                    {
                        return PostCondition();
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                log.Warn(ex);
                return false;
            }
            finally
            {
                CleanUp();
            }
        }
    }
}
