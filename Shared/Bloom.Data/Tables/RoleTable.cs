﻿using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    /// <summary>
    /// Represents the role table.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.ISqlTable" />
    public class RoleTable : ISqlTable
    {
        /// <summary>
        /// Gets the create role table SQL.
        /// </summary>
        public string CreateSql => "CREATE TABLE role (" +
                                   "id BLOB PRIMARY KEY NOT NULL UNIQUE , " +
                                   "name VARCHAR NOT NULL )";
    }
}
