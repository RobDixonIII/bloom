﻿using System;
using System.Collections.Generic;
using Bloom.State.Domain.Models;

namespace Bloom.State.Data.Respositories
{
    /// <summary>
    /// Repository for library connections.
    /// </summary>
    public interface ILibraryConnectionRepository
    {
        /// <summary>
        /// Gets the library connection.
        /// </summary>
        /// <param name="libraryId">The library identifier.</param>
        LibraryConnection GetLibraryConnection(Guid libraryId);

        /// <summary>
        /// Lists the library connections.
        /// </summary>
        List<LibraryConnection> ListLibraryConnections();

        /// <summary>
        /// Determines if a library connection exists.
        /// </summary>
        /// <param name="libraryId">The library identifier.</param>
        bool LibraryConnectionExists(Guid libraryId);

        /// <summary>
        /// Adds the library connection.
        /// </summary>
        /// <param name="libraryConnection">The library connection.</param>
        void AddLibraryConnection(LibraryConnection libraryConnection);

        /// <summary>
        /// Deletes the library connection.
        /// </summary>
        /// <param name="libraryConnection">The library connection.</param>
        void DeleteLibraryConnection(LibraryConnection libraryConnection);
    }
}