﻿using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using Bloom.Domain.Enums;
using Prism.Mvvm;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a song.
    /// </summary>
    /// <seealso cref="BindableBase" />
    [Table(Name = "song")]
    public class Song : BindableBase
    {
        /// <summary>
        /// Creates a new song instance.
        /// </summary>
        /// <param name="name">The song name.</param>
        /// <param name="artist">The song artist.</param>
        public static Song Create(string name, Artist artist)
        {
            return new Song
            {
                Id = Guid.NewGuid(),
                Name = name,
                Artist = artist
            };
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [Column(Name = "id", IsPrimaryKey = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the song's artist identifier.
        /// </summary>
        [Column(Name = "artist_id")]
        public Guid ArtistId { get; set; }

        /// <summary>
        /// Gets or sets the song's artist.
        /// </summary>
        public Artist Artist
        {
            get { return _artist; }
            set
            {
                _artist = value;
                ArtistId = _artist?.Id ?? Guid.Empty;
            }
        }
        private Artist _artist;

        /// <summary>
        /// Gets or sets the song name.
        /// </summary>
        [Column(Name = "name")]
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        private string _name;

        /// <summary>
        /// Gets or sets the song version.
        /// </summary>
        [Column(Name = "version")]
        public string Version
        {
            get { return _version; }
            set { SetProperty(ref _version, value); }
        }
        private string _version;

        /// <summary>
        /// Gets or sets the song length in milliseconds.
        /// </summary>
        [Column(Name = "length")]
        public int Length
        {
            get { return _length; }
            set { SetProperty(ref _length, value); }
        }
        private int _length;

        /// <summary>
        /// Gets or sets the song genre identifier.
        /// </summary>
        [Column(Name = "genre_id")]
        public Guid? GenreId { get; set; }

        /// <summary>
        /// Gets or sets the song genre.
        /// </summary>
        public Genre Genre 
        {
            get { return _genre; }
            set
            {
                _genre = value;
                GenreId = _genre?.Id;
            }
        }
        private Genre _genre;

        /// <summary>
        /// Gets or sets the song's beats per minute.
        /// </summary>
        [Column(Name = "bpm")]
        public double? Bpm { get; set; }

        /// <summary>
        /// Gets or sets the song's musical key.
        /// </summary>
        [Column(Name = "key")]
        public MusicalKeys? Key { get; set; }

        /// <summary>
        /// Gets or sets the time signature identifier.
        /// </summary>
        [Column(Name = "time_signature_id")]
        public Guid? TimeSignatureId { get; set; }

        /// <summary>
        /// Gets or sets the song's time signature.
        /// </summary>
        public TimeSignature TimeSignature
        {
            get { return _timeSignature; }
            set
            {
                _timeSignature = value;
                TimeSignatureId = _timeSignature?.Id;
            }
        }
        private TimeSignature _timeSignature;

        /// <summary>
        /// Gets or sets song description.
        /// </summary>
        [Column(Name = "description")]
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }
        private string _description;

        /// <summary>
        /// Gets or sets the song's lyrics.
        /// </summary>
        [Column(Name = "lyrics")]
        public string Lyrics
        {
            get { return _lyrics; }
            set { SetProperty(ref _lyrics, value); }
        }
        private string _lyrics;

        /// <summary>
        /// Gets or sets whether this song is live.
        /// </summary>
        [Column(Name = "is_live")]
        public bool IsLive { get; set; }

        /// <summary>
        /// Gets or sets whether this song is a cover.
        /// </summary>
        [Column(Name = "is_cover")]
        public bool IsCover { get; set; }

        /// <summary>
        /// Gets or sets whether this song is a remix.
        /// </summary>
        [Column(Name = "is_remix")]
        public bool IsRemix { get; set; }

        /// <summary>
        /// Gets or sets the original song identifier.
        /// </summary>
        [Column(Name = "original_song_id")]
        public Guid? OriginalSongId { get; set; }

        /// <summary>
        /// Gets or sets whether this song is for a holiday.
        /// </summary>
        [Column(Name = "is_holiday")]
        public bool IsHoliday { get; set; }

        /// <summary>
        /// Gets or sets the holiday identifier.
        /// </summary>
        [Column(Name = "holiday_id")]
        public Guid? HolidayId { get; set; }

        /// <summary>
        /// Gets or sets the holiday.
        /// </summary>s
        public Holiday Holiday
        {
            get { return _holiday; }
            set
            {
                _holiday = value;
                HolidayId = _holiday?.Id;
                IsHoliday = _holiday != null;
            }
        }
        private Holiday _holiday;

        /// <summary>
        /// Gets or sets whether this song has explicit content.
        /// </summary>
        [Column(Name = "has_explicit_content")]
        public bool HasExplicitContent { get; set; }

        /// <summary>
        /// Gets or sets the day of week this song is about.
        /// </summary>
        [Column(Name = "about_day_of_week")]
        public DayOfWeek? AboutDayOfWeek { get; set; }

        /// <summary>
        /// Gets or sets the time of year this song is about.
        /// </summary>
        [Column(Name = "about_time_of_year")]
        public TimeOfYear? AboutTimeOfYear { get; set; }

        /// <summary>
        /// Gets or sets the starting time this song is best played at, measured in seconds past midnight.
        /// </summary>
        [Column(Name = "best_played_at_start")]
        public int? BestPlayedAtStart { get; set; }

        /// <summary>
        /// Gets or sets the stopping time this song is best played at, measured in seconds past midnight.
        /// </summary>
        [Column(Name = "best_played_at_stop")]
        public int? BestPlayedAtStop { get; set; }

        /// <summary>
        /// Gets or sets the song rating.
        /// </summary>
        [Column(Name = "rating")]
        public int? Rating 
        { 
            get { return _rating; }
            set
            {
                _rating = value;
                RatedOn = DateTime.Now;
            } 
        }
        private int? _rating;

        /// <summary>
        /// Gets or sets notes on this song.
        /// </summary>
        [Column(Name = "notes")]
        public string Notes { get; set; }

        /// <summary>
        /// Gets or sets the song's play count.
        /// </summary>
        [Column(Name = "play_count")]
        public int PlayCount { get; set; }

        /// <summary>
        /// Gets or sets the song's skip count.
        /// </summary>
        [Column(Name = "skip_count")]
        public int SkipCount { get; set; }

        /// <summary>
        /// Gets or sets the song's remove count.
        /// </summary>
        [Column(Name = "remove_count")]
        public int RemoveCount { get; set; }

        /// <summary>
        /// Gets or sets the date this song was added on.
        /// </summary>
        [Column(Name = "added_on")]
        public DateTime AddedOn { get; set; }

        /// <summary>
        /// Gets or sets the date this song was rated on.
        /// </summary>
        [Column(Name = "rated_on")]
        public DateTime? RatedOn { get; set; }

        /// <summary>
        /// Gets or sets the date this song was last played.
        /// </summary>
        [Column(Name = "last_played")]
        public DateTime? LastPlayed { get; set; }

        /// <summary>
        /// Gets or sets the song media.
        /// </summary>
        public List<SongMedia> Media { get; set; }

        /// <summary>
        /// Gets or sets the song's segments.
        /// </summary>
        public List<SongSegment> Segments { get; set; }

        /// <summary>
        /// Gets or sets the collaborators.
        /// </summary>
        public List<SongCollaborator> Collaborators { get; set; }

        /// <summary>
        /// Gets or sets the song's credits.
        /// </summary>
        public List<SongCredit> Credits { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        public override string ToString()
        {
            return Artist == null ? Name : Artist.Name + ": " + Name;
        }
    }
}
