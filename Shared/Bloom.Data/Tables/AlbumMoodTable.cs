﻿using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    /// <summary>
    /// Represents the album_mood table.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.ISqlTable" />
    public class AlbumMoodTable : ISqlTable
    {
        /// <summary>
        /// Gets the create album_mood table SQL.
        /// </summary>
        public string CreateSql => "CREATE TABLE album_mood (" +
                                   "album_id BLOB NOT NULL , " +
                                   "mood_id BLOB NOT NULL , " +
                                   "PRIMARY KEY (album_id, mood_id) , " +
                                   "FOREIGN KEY (album_id) REFERENCES album(id) , " +
                                   "FOREIGN KEY (mood_id) REFERENCES mood(id) )";
    }
}