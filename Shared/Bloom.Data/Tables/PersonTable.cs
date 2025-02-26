﻿using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    /// <summary>
    /// Represents the person table.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.ISqlTable" />
    public class PersonTable : ISqlTable
    {
        /// <summary>
        /// Gets the create person table SQL.
        /// </summary>
        public string CreateSql => "CREATE TABLE person (" +
                                   "id BLOB PRIMARY KEY NOT NULL UNIQUE , " +
                                   "name VARCHAR NOT NULL , " +
                                   "born_on DATETIME , " +
                                   "died_on DATETIME , " +
                                   "from_city_id BLOB , " +
                                   "bio VARCHAR , " +
                                   "twitter VARCHAR , " +
                                   "follow BOOL NOT NULL DEFAULT FALSE )";
    }
}
