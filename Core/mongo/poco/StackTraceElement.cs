using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace NeithCore.MongoDB.poco
{
    public class StackTraceElement
    {
        [BsonIgnoreIfNull]
        public readonly string declaringClass,methodName,fileName;
        [BsonIgnoreIfNull]
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
