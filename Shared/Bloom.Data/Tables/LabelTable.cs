﻿using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    /// <summary>
    /// Represents the label table.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.ISqlTable" />
    public class LabelTable : ISqlTable
    {
        /// <summary>
        /// Gets the create label table SQL.
        /// </summary>
        public string CreateSql => "CREATE TABLE label (" +
                                   "id BLOB PRIMARY KEY NOT NULL UNIQUE , " +
                                   "name VARCHAR NOT NULL , " +
                                   "bio VARCHAR , " +
                                   "logo_file_path VARCHAR , " +
                                   "founded_on DATETIME , " +
                                   "closed_on DATETIME , " +
                                   "parent_label_id BLOB)";
    }
}
