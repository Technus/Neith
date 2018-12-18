using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Neith.com.dgs.dapc.neith.mongo
{
    class ThreadLog
    {
        public readonly string name;
        public readonly long threadId;
        public readonly ThreadPriority priority;
        public readonly ThreadState state;
        public readonly bool alive, daemon;

        public ThreadLog(Thread t)
        {
            name = t.Name;
            threadId = t.ManagedThreadId;
            priority = t.Priority;
            state = t.ThreadState;
            alive = t.IsAlive;
            daemon = t.IsBackground;
        }

        [BsonConstructor]
        public ThreadLog(string name, long threadId, ThreadPriority priority, ThreadState state, bool alive, bool daemon)
        {
            this.name = name;
            this.threadId = threadId;
            this.priority = priority;
            this.state = state;
            this.alive = alive;
            this.daemon = daemon;
        }
    }
}
