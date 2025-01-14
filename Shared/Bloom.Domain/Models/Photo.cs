﻿using System;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a photo.
    /// </summary>
    [Table(Name = "photo")]
    public class Photo
    {
        /// <summary>
        /// Creates a new photo instance.
        /// </summary>
        /// <param name="filePath">The photo file path.</param>
        public static Photo Create(string filePath)
        {
            return new Photo
            {
                Id = Guid.NewGuid(),
                FilePath = filePath
            };
        }

        /// <summary>
        /// Gets or sets the photo identifier.
        /// </summary>
        [Column(Name = "id", IsPrimaryKey = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the photo file path.
        /// </summary>
        [Column(Name = "file_path")]
        public string FilePath { get; set; }

        /// <summary>
        /// Gets or sets the caption.
        /// </summary>
        [Column(Name = "caption")]
        public string Caption { get; set; }

        /// <summary>
        /// Gets or sets the date the photo was taken on.
        /// </summary>
        [Column(Name = "taken_on")]
        public DateTime? TakenOn { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        public override string ToString()
        {
            return FilePath;
        }
    }
}
