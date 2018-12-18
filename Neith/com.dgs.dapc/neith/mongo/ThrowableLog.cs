using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Neith.com.dgs.dapc.neith.mongo
{
    class ThrowableLog
    {
        public static string currentApplicationName="Neith";

        [BsonId]
        public ObjectId id;

        public readonly string applicationName;

        [BsonIgnore]
        public readonly Exception throwable;
        public readonly string throwableClass;
        public readonly string message;
        public readonly ThrowableLog cause;
        public readonly ReadOnlyCollection<StackTraceElement> stackTrace;
        public readonly ThreadLog threadLog;
        public readonly DateTime time;
        public readonly SystemUser systemUser;

        public ThrowableLog(Exception t)
        {
            time = DateTime.Now;

            threadLog = new ThreadLog(Thread.CurrentThread);

            throwable = t;

            throwableClass = t.GetType().FullName;
            message = t.Message;

            if (t.InnerException != t && t.InnerException != null)
            {
                cause = new ThrowableLog(t.InnerException);
            }
            else
            {
                cause = null;
            }
           

            applicationName = currentApplicationName;
            stackTrace = StackTraceElement.BuildElements(new System.Diagnostics.StackTrace(t));
            systemUser = new SystemUser();
        }

        [BsonConstructor]
        public ThrowableLog(
            ObjectId id, 
            string applicationName, 
            string throwableClass, 
            string message, 
            ThrowableLog cause, 
            List<StackTraceElement> stackTrace, 
            ThreadLog threadLog, 
            DateTime time, 
            SystemUser systemUser)
        {
            this.id = id;
            this.applicationName = applicationName;
            this.throwableClass = throwableClass;
            this.message = message;
            this.cause = cause;
            this.stackTrace = new ReadOnlyCollection<StackTraceElement>(stackTrace);
            this.threadLog = threadLog;
            this.time = time;
            this.systemUser = systemUser;
        }
    }
}
