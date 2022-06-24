﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using System;
using System.Threading;

namespace osu.Framework.Allocation
{
    public class ObjectUsage<T> : IDisposable
        where T : class
    {
        public T Object;

        public UsageType Usage;

        public readonly int Index;

        public readonly ManualResetEventSlim ResetEvent = new ManualResetEventSlim();

        public bool Consumed;

        internal readonly Action<ObjectUsage<T>, UsageType> Finish;

        public ObjectUsage(int index, Action<ObjectUsage<T>, UsageType> finish)
        {
            Index = index;
            Finish = finish;
        }

        public void Dispose()
        {
            Finish?.Invoke(this, Usage);
        }
    }

    public enum UsageType
    {
        None,
        Read,
        Write
    }
}
