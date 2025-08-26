﻿//----------------------------------------------------------------------- 
// PDS WITSMLstudio Desktop, 2018.1
//
// Copyright 2018 PDS Americas LLC
// 
// Licensed under the PDS Open Source WITSML Product License Agreement (the
// "License"); you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//   
//     http://www.pds.group/WITSMLstudio/OpenSource/ProductLicenseAgreement
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//-----------------------------------------------------------------------

using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Data;

namespace PDS.WITSMLstudio.Desktop.Core.Models
{
    /// <summary>
    /// Manages <see cref="ReaderWriterLockSlim"/> instance associated with an object reference.
    /// </summary>
    public static class ReaderWriterLockManager
    {
        private static readonly ConditionalWeakTable<object, ReaderWriterLockSlim> _managedLocks = new ConditionalWeakTable<object, ReaderWriterLockSlim>();
        private static readonly object _internalLock = new object();

        /// <summary>
        /// Gets the current <see cref="ReaderWriterLockSlim"/> associated with the object instance.
        /// </summary>
        /// <param name="instance">The object instance.</param>
        /// <returns></returns>
        public static ReaderWriterLockSlim GetLock(this object instance)
        {
            lock (_internalLock)
            {
                return _managedLocks.GetValue(instance, k => new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion));
            }
        }

        /// <summary>
        /// Removes the current <see cref="ReaderWriterLockSlim"/> associated with the object instance.
        /// </summary>
        /// <param name="instance">The object instance.</param>
        /// <returns></returns>
        public static bool RemoveLock(this object instance)
        {
            lock (_internalLock)
            {
                ReaderWriterLockSlim lockObject;
                if (!_managedLocks.TryGetValue(instance, out lockObject))
                    return false;

                try
                {
                    _managedLocks.Remove(instance);
                }
                finally
                {
                    lockObject?.Dispose();
                }

                return true;
            }
        }

        /// <summary>
        /// Execute a synchronized read action.
        /// </summary>
        /// <typeparam name="TResult">The return type.</typeparam>
        /// <param name="instance">The object instance.</param>
        /// <param name="action">The action to execute.</param>
        /// <returns>The esult of the executeed action.</returns>
        public static TResult ExecuteWithReadLock<TResult>(this object instance, Func<TResult> action)
        {
            var readLock = GetLock(instance);

            readLock.EnterReadLock();

            try
            {
                return action();
            }
            finally
            {
                readLock.ExitReadLock();
            }
        }

        /// <summary>
        /// Execute a synchronized read action.
        /// </summary>
        /// <param name="instance">The object instance.</param>
        /// <param name="action">The action to execute.</param>
        /// <returns></returns>
        public static void ExecuteWithReadLock(this object instance, System.Action action)
        {
            var readLock = GetLock(instance);

            readLock.EnterReadLock();

            try
            {
                action();
            }
            finally
            {
                readLock.ExitReadLock();
            }
        }

        /// <summary>
        /// Attempt to execute a synchronized read action.
        /// </summary>
        /// <typeparam name="TResult">The return type.</typeparam>
        /// <param name="instance">The object instance.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="result">The result.</param>
        /// <param name="action">The action to execute.</param>
        /// <returns>Whether action was executeed.</returns>
        public static bool TryExecuteWithReadLock<TResult>(this object instance, TimeSpan timeout, out TResult result, Func<TResult> action)
        {
            var readLock = GetLock(instance);

            result = default(TResult);

            if (!readLock.TryEnterReadLock(timeout))
                return false;

            try
            {
                result = action();

                return true;
            }
            finally
            {
                readLock.ExitReadLock();
            }
        }

        /// <summary>
        /// Attempt to execute a synchronized read action.
        /// </summary>
        /// <param name="instance">The object instance.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="action">The action to execute.</param>
        /// <returns></returns>
        public static bool TryExecuteWithReadLock(this object instance, TimeSpan timeout, System.Action action)
        {
            var readLock = GetLock(instance);

            if (!readLock.TryEnterReadLock(timeout))
                return false;

            try
            {
                action();

                return true;
            }
            finally
            {
                readLock.ExitReadLock();
            }
        }

        /// <summary>
        /// Execute a synchronized write action.
        /// </summary>
        /// <typeparam name="TResult">The return type.</typeparam>
        /// <param name="instance">The object instance.</param>
        /// <param name="action">The action to execute.</param>
        /// <returns>The esult of the executeed action.</returns>
        public static TResult ExecuteWithWriteLock<TResult>(this object instance, Func<TResult> action)
        {
            var readLock = GetLock(instance);

            readLock.EnterWriteLock();

            try
            {
                return action();
            }
            finally
            {
                readLock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Execute a synchronized write action.
        /// </summary>
        /// <param name="instance">The object instance.</param>
        /// <param name="action">The action to execute.</param>
        /// <returns></returns>
        public static void ExecuteWithWriteLock(this object instance, System.Action action)
        {
            var readLock = GetLock(instance);

            readLock.EnterWriteLock();

            try
            {
                action();
            }
            finally
            {
                readLock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Attempt to execute a synchronized write action.
        /// </summary>
        /// <typeparam name="TResult">The return type.</typeparam>
        /// <param name="instance">The object instance.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="result">The result.</param>
        /// <param name="action">The action to execute.</param>
        /// <returns>Whether action was executeed.</returns>
        public static bool TryExecuteWithWriteLock<TResult>(this object instance, TimeSpan timeout, out TResult result, Func<TResult> action)
        {
            var readLock = GetLock(instance);

            result = default(TResult);

            if (!readLock.TryEnterWriteLock(timeout))
                return false;

            try
            {
                result = action();

                return true;
            }
            finally
            {
                readLock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Attempt to execute a synchronized write action.
        /// </summary>
        /// <param name="instance">The object instance.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="action">The action to execute.</param>
        /// <returns></returns>
        public static bool TryExecuteWithWriteLock(this object instance, TimeSpan timeout, System.Action action)
        {
            var readLock = GetLock(instance);

            if (!readLock.TryEnterWriteLock(timeout))
                return false;

            try
            {
                action();

                return true;
            }
            finally
            {
                readLock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Synchronize read/write access to enumerable collection. <see cref="CollectionSynchronizationCallback" />
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="context">The context object.</param>
        /// <param name="accessMethod">The access method.</param>
        /// <param name="writeAccess">Whether access is read or write.</param>
        public static void SynchronizeReadWriteAccess(IEnumerable collection, object context, System.Action accessMethod, bool writeAccess)
        {
            if (writeAccess)
                collection.ExecuteWithWriteLock(accessMethod);
            else
                collection.ExecuteWithReadLock(accessMethod);
        }
    }
}
