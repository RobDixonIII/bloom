﻿using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    /// <summary>
    /// Represents the album_release table.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.ISqlTable" />
    public class AlbumReleaseTable : ISqlTable
    {
        /// <summary>
        /// Gets the create album_release table SQL.
        /// </summary>
        public string CreateSql => "CREATE TABLE album_release (" +
                                   "id BLOB PRIMARY KEY NOT NULL UNIQUE , " +
                                   "album_id BLOB NOT NULL , " +
                                   "media_types INTEGER NOT NULL DEFAULT 0 , " +
                                   "digital_formats INTEGER , " +
                                   "release_date DATETIME , " +
                                   "label_id BLOB ," +
                                   "catalog_number VARCHAR , " +
                                   "FOREIGN KEY (album_id) REFERENCES album(id) , " +
                                   "FOREIGN KEY (label_id) REFERENCES label(id) )";
    }
}