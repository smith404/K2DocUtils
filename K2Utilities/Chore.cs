using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K2Utilities
{
    public abstract class Chore<T>
    {
        // Other managed resource this class uses.
        protected T workItem;

        // The class constructor.
        public Chore(T workItem)
        {
            this.workItem = workItem;
        }

        public abstract bool Execute();

        public abstract string Log();
    }
}
