using System;
using System.Collections.Generic;
using System.Linq;

namespace NeithCore.utility
{
    public interface ITimedModification
    {
        DateTime getTimestamp();
    }

    public static class TimedModificationExtensions
    {
        public static T GetNewest<T>(this T[] timedModifications) where T : ITimedModification
        {
            return ((IEnumerable<T>)timedModifications).GetNewest();
        }

        public static T GetNewest<T>(this IEnumerable<T> timedModifications) where T: ITimedModification
        {
            return timedModifications.GetNewest();
        }

        public static T GetNewest<T>(this IEnumerator<T> enumerator) where T : ITimedModification
        {
            enumerator.Reset();

            T newest = enumerator.Current;
            try
            {
                if (!enumerator.MoveNext())
                {
                    return newest;
                }
            }
            catch(InvalidOperationException e)
            {
                return GetNewest(enumerator);
            }

            T currentlyTestedObject;
        loop:
            currentlyTestedObject = enumerator.Current;
            if (currentlyTestedObject.getTimestamp().CompareTo(newest.getTimestamp()) > 0)
            {
                newest = currentlyTestedObject;
            }
            
            try
            {
                if (enumerator.MoveNext())
                {
                    goto loop;
                }
            }
            catch (InvalidOperationException e)
            {
                return GetNewest(enumerator);
            }
            return newest;
        }
    }
}
