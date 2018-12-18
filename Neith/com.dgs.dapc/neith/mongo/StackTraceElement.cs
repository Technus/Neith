using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neith.com.dgs.dapc.neith.mongo
{
    class StackTraceElement
    {
        public readonly string declaringClass;
        public readonly string methodName;
        public readonly string fileName;
        public readonly int lineNumber;

        public static ReadOnlyCollection<StackTraceElement> BuildElements(StackTrace stackTrace)
        {
            List<StackTraceElement> list = new List<StackTraceElement>();
            foreach(StackFrame frame in stackTrace.GetFrames())
            {
                list.Add(new StackTraceElement(frame));
            }
            return new ReadOnlyCollection<StackTraceElement>(list);
        }

        public StackTraceElement(StackFrame frame)
        {
            declaringClass = frame.GetType().FullName;
            methodName = frame.GetMethod().Name;
            fileName = frame.GetFileName();
            lineNumber = frame.GetFileLineNumber();
        }

        [BsonConstructor]
        public StackTraceElement(string declaringClass, string methodName, string fileName, int lineNumber)
        {
            this.declaringClass = declaringClass;
            this.methodName = methodName;
            this.fileName = fileName;
            this.lineNumber = lineNumber;
        }
    }
}
