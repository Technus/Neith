using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NeithCore.MongoDB.poco
{
    public class ThreadLog
    {
        [BsonIgnoreIfNull]
        public readonly string name;
        [BsonIgnoreIfNull]
        public readonly long threadId;
        [BsonIgnoreIfNull]
        public readonly ThreadPriority priority;
        [BsonIgnoreIfNull]
        public readonly ThreadState state;
        [BsonIgnoreIfNull]
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
