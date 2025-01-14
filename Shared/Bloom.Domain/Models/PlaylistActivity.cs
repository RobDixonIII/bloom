﻿using System;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents an association between a playlist and an activity.
    /// </summary>
    [Table(Name = "playlist_activity")]
    public class PlaylistActivity
    {
        /// <summary>
        /// Creates a new playlist activity instance.
        /// </summary>
        /// <param name="playlist">The playlist.</param>
        /// <param name="activity">The activity.</param>
        public static PlaylistActivity Create(Playlist playlist, Activity activity)
        {
            return new PlaylistActivity
            {
                PlaylistId = playlist.Id,
                ActivityId = activity.Id
            };
        }

        /// <summary>
        /// Gets or sets the playlist identifier.
        /// </summary>
        [Column(Name = "playlist_id", IsPrimaryKey = true)]
        public Guid PlaylistId { get; set; }

        /// <summary>
        /// Gets or sets the activity identifier.
        /// </summary>
        [Column(Name = "activity_id", IsPrimaryKey = true)]
        public Guid ActivityId { get; set; }
    }
}
