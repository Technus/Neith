﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace NeithCore.utility
{
    public interface ITimedModification
    {
        DateTime getTimestamp();
    }

    public static class TimedModificationUtility
    {
        public static T GetNewest<T>(IEnumerable<T> timedModifications) where T: ITimedModification
        {
            return GetNewest(timedModifications.ToArray());
        }

        public static T GetNewest<T>(params T[] timedModifications) where T : ITimedModification
        {
            if (timedModifications != null && timedModifications.Length > 0)
            {
                T newest = timedModifications[0];
                for (int i = 1; i < timedModifications.Length; i++)
                {
                    T currentlyTestedObject = timedModifications[i];
                    if (currentlyTestedObject.getTimestamp().CompareTo(newest.getTimestamp()) > 0)
                    {
                        newest = currentlyTestedObject;
                    }
                }
                return newest;
            }
            return default(T);
        }
    }
}
