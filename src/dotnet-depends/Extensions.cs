// Copyright 2020 Heath Stewart
// Licensed under the MIT license. See LICENSE.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Text;

namespace Depends
{
    /// <summary>
    /// Extension methods.
    /// </summary>
    internal static class Extensions
    {
        /// <summary>
        /// Formats an enumerable as a delimited string.
        /// </summary>
        /// <typeparam name="T">The enumerated type.</typeparam>
        /// <param name="source">The enumerable to format.</param>
        /// <param name="separator">The separator.</param>
        /// <param name="selector">Optional selector. The default is <see cref="object.ToString"/>.</param>
        /// <returns>A delimited string of any non-null elements.</returns>
        public static string Join<T>(this IEnumerable<T> source, string separator, Func<T, string>? selector = null)
        {
            selector ??= value => value!.ToString();

            var sb = new StringBuilder();
            foreach (var element in source)
            {
                if (element is null)
                {
                    continue;
                }

                if (sb.Length > 0)
                {
                    sb.Append(separator);
                }

                var value = selector(element);
                sb.Append(value);
            }

            return sb.ToString();
        }
    }
}
