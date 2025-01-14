﻿using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;

namespace Bloom.Domain.Interfaces
{
    /// <summary>
    /// Interface for a filter.
    /// </summary>
    public interface IFilter
    {
        /// <summary>
        /// Gets the filter identifier.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Gets the filter label.
        /// </summary>
        string Label { get; }

        /// <summary>
        /// Returns a new collection of the specified type which has been filtered using a provided comparison.
        /// </summary>
        /// <typeparam name="T">Domain entity to filter.</typeparam>
        /// <param name="items">The collection to filter.</param>
        /// <param name="comparison">The comparison statement.</param>
        /// <param name="compareAgainst">The value to compare against.</param>
        List<T> Apply<T>(List<T> items, FilterComparison comparison, string compareAgainst);
    }
}
