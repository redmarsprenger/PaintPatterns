using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaintPatterns
{
    class Actions
    {
        public class ActionsList<T>
        {
            private class Action
            {
                public Action(T t) => (Next, Data) = (null, t);

                public Action Next { get; set; }
                public T Data { get; set; }
            }

            private Action head;

            public void AddHead(T t)
            {
                Action n = new Action(t) { Next = head };
                head = n;
            }
        }
    }
}
