﻿using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    /// <summary>
    /// Access methods for album data.
    /// </summary>
    public class AlbumRepository : IAlbumRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumRepository"/> class.
        /// </summary>
        /// <param name="roleRepository">The role repository.</param>
        /// <param name="personRepository">The person repository.</param>
        public AlbumRepository(IRoleRepository roleRepository, IPersonRepository personRepository)
        {
            _roleRepository = roleRepository;
            _personRepository = personRepository;
        }
        private readonly IRoleRepository _roleRepository;
        private readonly IPersonRepository _personRepository;

        /// <summary>
        /// Gets the album.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="albumId">The album identifier.</param>
        public Album GetAlbum(IDataSource dataSource, Guid albumId)
        {
            if (!dataSource.IsConnected())
                return null;

            var holidayTable = HolidayTable(dataSource);
            var artistTable = ArtistTable(dataSource);
            var albumTable = AlbumTable(dataSource);
            if (albumTable == null)
                return null;

            var albumQuery =
                from a in albumTable
                from artist in artistTable.Where(r => a.ArtistId == r.Id).DefaultIfEmpty()
                from holiday in holidayTable.Where(h => a.HolidayId == h.Id).DefaultIfEmpty()
                where a.Id == albumId
                select new
                {
                    Album = a,
                    Artist = artist,
                    Holiday = holiday
                };

            var result = albumQuery.SingleOrDefault();

            if (result == null)
                return null;

            var album = result.Album;
            if (album == null)
                return null;

            album.Artist = result.Artist;
            album.Holiday = result.Holiday;

            var tracksTable = AlbumTrackTable(dataSource);
            var songTable = SongTable(dataSource);
            var genreTable = GenreTable(dataSource);
            var timeSignatureTable = TimeSignatureTable(dataSource);
            var tracksQuery =
                from track in tracksTable
                join song in songTable on track.SongId equals song.Id
                from genre in genreTable.Where(g => song.GenreId == g.Id).DefaultIfEmpty()
                from artist in artistTable.Where(a => song.ArtistId == a.Id).DefaultIfEmpty()
                from holiday in holidayTable.Where(h => song.HolidayId == h.Id).DefaultIfEmpty()
                from timeSignature in timeSignatureTable.Where(t => song.TimeSignatureId == t.Id).DefaultIfEmpty()
                where track.AlbumId == albumId
                orderby track.DiscNumber, track.TrackNumber
                select new 
                {
                    Track = track,
                    Song = song,
                    Genre = genre,
                    Artist = artist,
                    Holiday = holiday,
                    TimeSignature = timeSignature
                };

            var results = tracksQuery.ToList();

            album.Tracks = null;
            if (results.Any())
            {
                album.Tracks = new List<AlbumTrack>();
                foreach (var trackResult in results)
                {
                    var track = trackResult.Track;
                    track.Song = trackResult.Song;
                    track.Song.Artist = trackResult.Artist;
                    track.Song.Genre = trackResult.Genre;
                    track.Song.Holiday = trackResult.Holiday;
                    track.Song.TimeSignature = trackResult.TimeSignature;
                    album.Tracks.Add(track);
                }
            }

            var albumMediaTable = AlbumMediaTable(dataSource);
            var albumReleaseTable = AlbumReleaseTable(dataSource);
            var mediaQuery =
                from media in albumMediaTable
                from release in albumReleaseTable.Where(r => media.ReleaseId == r.Id).DefaultIfEmpty()
                where media.AlbumId == albumId
                orderby release.ReleaseDate
                select new
                {
                    Media = media,
                    Release = release
                };

            album.Media = null;
            var mediaResults = mediaQuery.ToList();
            if (mediaResults.Any())
            {
                album.Media = new List<AlbumMedia>();
                foreach (var mediaResult in mediaResults)
                {
                    var albumMedia = mediaResult.Media;
                    albumMedia.Release = mediaResult.Release;
                    album.Media.Add(albumMedia);
                }
            }
            
            var albumArtworkTable = AlbumArtworkTable(dataSource);
            var artworkQuery =
                from artwork in albumArtworkTable
                where artwork.AlbumId == albumId
                select artwork;

            album.Artwork = artworkQuery.ToList();

            var collaboratorsTable = AlbumCollaboratorTable(dataSource);
            var collaboratorsQuery =
                from collaborator in collaboratorsTable
                join artist in artistTable on collaborator.ArtistId equals artist.Id
                where collaborator.AlbumId == albumId
                orderby collaborator.IsFeatured descending, artist.Name
                select new
                {
                    Collaborator = collaborator,
                    Artist = artist
                };

            album.Collaborators = null;
            var collaboratorsResults = collaboratorsQuery.ToList();
            if (collaboratorsResults.Any())
            {
                album.Collaborators = new List<AlbumCollaborator>();
                foreach (var collaboratorResult in collaboratorsResults)
                {
                    var albumCollaborator = collaboratorResult.Collaborator;
                    albumCollaborator.Artist = collaboratorResult.Artist;
                    album.Collaborators.Add(albumCollaborator);
                }
            }

            var personTable = PersonTable(dataSource);
            var creditsTable = AlbumCreditTable(dataSource);
            var creditsQuery =
                from albumCredit in creditsTable
                join person in personTable on albumCredit.PersonId equals person.Id
                where albumCredit.AlbumId == albumId
                orderby person.Name
                select new
                {
                    Credit = albumCredit,
                    Person = person
                };

            album.Credits = null;
            var creditsResults = creditsQuery.ToList();
            if (creditsResults.Any())
            {
                album.Credits = new List<AlbumCredit>();
                foreach (var creditResult in creditsResults)
                {
                    var albumCredit = creditResult.Credit;
                    albumCredit.Person = creditResult.Person;
                    album.Credits.Add(albumCredit);
                }
            }

            if (album.Credits == null)
                return album;

            var albumCreditRoleTable = AlbumCreditRoleTable(dataSource);
            var roleTable = RoleTable(dataSource);
            foreach (var credit in album.Credits)
            {
                var c = credit;
                var rolesQuery =
                    from acr in albumCreditRoleTable
                    join role in roleTable on acr.RoleId equals role.Id
                    where acr.AlbumCreditId == c.Id
                    orderby role.Name
                    select role;

                credit.Roles = rolesQuery.ToList();
            }

            return album;
        }

        /// <summary>
        /// Gets the album release.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="albumReleaseId">The album release identifier.</param>
        public AlbumRelease GetAlbumRelease(IDataSource dataSource, Guid albumReleaseId)
        {
            if (!dataSource.IsConnected())
                return null;

            var albumReleaseTable = AlbumReleaseTable(dataSource);
            if (albumReleaseTable == null)
                return null;

            var albumReleaseQuery =
                from albumRelease in albumReleaseTable
                where albumRelease.Id == albumReleaseId
                select albumRelease;

            return albumReleaseQuery.SingleOrDefault();
        }

        /// <summary>
        /// Lists the albums.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        public List<Album> ListAlbums(IDataSource dataSource)
        {
            if (!dataSource.IsConnected())
                return null;

            var holidayTable = HolidayTable(dataSource);
            var artistTable = ArtistTable(dataSource);
            var albumTable = AlbumTable(dataSource);
            if (albumTable == null)
                return null;

            var albumQuery =
                from album in albumTable
                from artist in artistTable.Where(r => album.ArtistId == r.Id).DefaultIfEmpty()
                from holiday in holidayTable.Where(h => album.HolidayId == h.Id).DefaultIfEmpty()
                where album.ArtistId != null
                orderby artist.Name, album.FirstReleasedOn, album.Name
                select new
                {
                    Album = album,
                    Artist = artist,
                    Holiday = holiday
                };

            var noArtistAlbumQuery =
                from album in albumTable
                from artist in artistTable.Where(r => album.ArtistId == r.Id).DefaultIfEmpty()
                from holiday in holidayTable.Where(h => album.HolidayId == h.Id).DefaultIfEmpty()
                where album.ArtistId == null
                orderby album.FirstReleasedOn, album.Name
                select new
                {
                    Album = album,
                    Artist = artist,
                    Holiday = holiday
                };

            var results = albumQuery.ToList();
            results.AddRange(noArtistAlbumQuery.ToList());

            if (!results.Any()) 
                return null;

            var albums = new List<Album>();
            foreach (var result in results)
            {
                var album = result.Album;
                album.Artist = result.Artist;
                album.Holiday = result.Holiday;
                albums.Add(album);
            }

            return albums;
        }

        /// <summary>
        /// Lists the albums for a given artist.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="artistId">The artist identifier.</param>
        public List<Album> ListArtistAlbums(IDataSource dataSource, Guid artistId)
        {
            if (!dataSource.IsConnected())
                return null;

            var holidayTable = HolidayTable(dataSource);
            var artistTable = ArtistTable(dataSource);
            var albumTable = AlbumTable(dataSource);
            if (albumTable == null)
                return null;

            var albumQuery =
                from album in albumTable
                from artist in artistTable.Where(r => album.ArtistId == r.Id).DefaultIfEmpty()
                from holiday in holidayTable.Where(h => album.HolidayId == h.Id).DefaultIfEmpty()
                where artist.Id == artistId
                orderby album.FirstReleasedOn, album.Name
                select new
                {
                    Album = album,
                    Artist = artist,
                    Holiday = holiday
                };

            var results = albumQuery.ToList();

            if (!results.Any())
                return null;

            var albums = new List<Album>();
            foreach (var result in results)
            {
                var album = result.Album;
                album.Artist = result.Artist;
                album.Holiday = result.Holiday;
                albums.Add(album);
            }

            return albums;
        }

        /// <summary>
        /// Lists the album releases.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="albumId">The album identifier.</param>
        public List<AlbumRelease> ListAlbumReleases(IDataSource dataSource, Guid albumId)
        {
            if (!dataSource.IsConnected())
                return null;

            var albumReleaseTable = AlbumReleaseTable(dataSource);
            if (albumReleaseTable == null)
                return null;

            var albumReleaseQuery =
                from albumRelease in albumReleaseTable
                where albumRelease.AlbumId == albumId
                orderby albumRelease.ReleaseDate
                select albumRelease;

            return albumReleaseQuery.ToList();
        }

        /// <summary>
        /// Adds the album.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="album">The album.</param>
        public void AddAlbum(IDataSource dataSource, Album album)
        {
            if (!dataSource.IsConnected())
                return;

            var albumTable = AlbumTable(dataSource);
            if (albumTable == null)
                return;

            albumTable.InsertOnSubmit(album);
            dataSource.Save();
        }

        /// <summary>
        /// Adds a track to the album.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="albumTrack">The album track.</param>
        public void AddAlbumTrack(IDataSource dataSource, AlbumTrack albumTrack)
        {
            if (!dataSource.IsConnected())
                return;

            var albumTrackTable = AlbumTrackTable(dataSource);
            if (albumTrackTable == null)
                return;

            albumTrackTable.InsertOnSubmit(albumTrack);
            dataSource.Save();
        }

        /// <summary>
        /// Deletes an album track.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="albumTrack">The album track.</param>
        public void DeleteAlbumTrack(IDataSource dataSource, AlbumTrack albumTrack)
        {
            if (!dataSource.IsConnected())
                return;

            var albumTrackTable = AlbumTrackTable(dataSource);
            if (albumTrackTable == null)
                return;

            albumTrackTable.DeleteOnSubmit(albumTrack);
            dataSource.Save();
        }

        /// <summary>
        /// Adds the album media.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="albumMedia">The album media.</param>
        public void AddAlbumMedia(IDataSource dataSource, AlbumMedia albumMedia)
        {
            if (!dataSource.IsConnected())
                return;

            var albumMediaTable = AlbumMediaTable(dataSource);
            if (albumMediaTable == null)
                return;

            albumMediaTable.InsertOnSubmit(albumMedia);
            dataSource.Save();
        }

        /// <summary>
        /// Deletes the album media.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="albumMedia">The album media.</param>
        public void DeleteAlbumMedia(IDataSource dataSource, AlbumMedia albumMedia)
        {
            if (!dataSource.IsConnected())
                return;

            var albumMediaTable = AlbumMediaTable(dataSource);
            if (albumMediaTable == null)
                return;

            albumMediaTable.DeleteOnSubmit(albumMedia);
            dataSource.Save();
        }

        /// <summary>
        /// Adds album artwork.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="albumArtwork">The album artwork.</param>
        public void AddAlbumArtwork(IDataSource dataSource, AlbumArtwork albumArtwork)
        {
            if (!dataSource.IsConnected())
                return;

            var albumArtworkTable = AlbumArtworkTable(dataSource);
            if (albumArtworkTable == null)
                return;

            albumArtworkTable.InsertOnSubmit(albumArtwork);
            dataSource.Save();
        }

        /// <summary>
        /// Deletes album artwork.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="albumArtwork">The album artwork.</param>
        public void DeleteAlbumArtwork(IDataSource dataSource, AlbumArtwork albumArtwork)
        {
            if (!dataSource.IsConnected())
                return;

            var albumArtworkTable = AlbumArtworkTable(dataSource);
            if (albumArtworkTable == null)
                return;

            albumArtworkTable.DeleteOnSubmit(albumArtwork);
            dataSource.Save();
        }

        /// <summary>
        /// Adds an album release.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="albumRelease">The album release.</param>
        public void AddAlbumRelease(IDataSource dataSource, AlbumRelease albumRelease)
        {
            if (!dataSource.IsConnected())
                return;

            var albumReleaseTable = AlbumReleaseTable(dataSource);
            if (albumReleaseTable == null)
                return;

            albumReleaseTable.InsertOnSubmit(albumRelease);
            dataSource.Save();
        }

        /// <summary>
        /// Deletes an album release.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="albumRelease">The album release.</param>
        public void DeleteAlbumRelease(IDataSource dataSource, AlbumRelease albumRelease)
        {
            if (!dataSource.IsConnected())
                return;

            var albumReleaseTable = AlbumReleaseTable(dataSource);
            if (albumReleaseTable == null)
                return;

            albumReleaseTable.DeleteOnSubmit(albumRelease);
            dataSource.Save();
        }

        /// <summary>
        /// Adds an album collaborator.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="albumCollaborator">The album collaborator.</param>
        public void AddAlbumCollaborator(IDataSource dataSource, AlbumCollaborator albumCollaborator)
        {
            if (!dataSource.IsConnected())
                return;

            var albumCollaboratorTable = AlbumCollaboratorTable(dataSource);
            if (albumCollaboratorTable == null)
                return;

            albumCollaboratorTable.InsertOnSubmit(albumCollaborator);
            dataSource.Save();
        }

        /// <summary>
        /// Deletes an album collaborator.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="albumCollaborator">The album collaborator.</param>
        public void DeleteAlbumCollaborator(IDataSource dataSource, AlbumCollaborator albumCollaborator)
        {
            if (!dataSource.IsConnected())
                return;

            var albumCollaboratorTable = AlbumCollaboratorTable(dataSource);
            if (albumCollaboratorTable == null)
                return;

            albumCollaboratorTable.DeleteOnSubmit(albumCollaborator);
            dataSource.Save();
        }

        /// <summary>
        /// Adds an album credit.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="albumCredit">The album credit.</param>
        public void AddAlbumCredit(IDataSource dataSource, AlbumCredit albumCredit)
        {
            if (!dataSource.IsConnected())
                return;

            if (!_personRepository.PersonExists(dataSource, albumCredit.PersonId))
                _personRepository.AddPerson(dataSource, albumCredit.Person);

            var albumCreditTable = AlbumCreditTable(dataSource);
            if (albumCreditTable == null)
                return;

            albumCreditTable.InsertOnSubmit(albumCredit);
            dataSource.Save();
        }

        /// <summary>
        /// Deletes an album credit.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="albumCredit">The album credit.</param>
        public void DeleteAlbumCredit(IDataSource dataSource, AlbumCredit albumCredit)
        {
            if (!dataSource.IsConnected())
                return;

            var albumCreditTable = AlbumCreditTable(dataSource);
            if (albumCreditTable == null)
                return;

            if (albumCredit.Roles != null && albumCredit.Roles.Any())
                foreach (var role in albumCredit.Roles)
                    DeleteAlbumCreditRole(dataSource, albumCredit, role);

            albumCreditTable.DeleteOnSubmit(albumCredit);
            dataSource.Save();
        }

        /// <summary>
        /// Adds an album credit role.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="albumCredit">The album credit.</param>
        /// <param name="role">The role.</param>
        public void AddAlbumCreditRole(IDataSource dataSource, AlbumCredit albumCredit, Role role)
        {
            if (!dataSource.IsConnected())
                return;

            if (!_roleRepository.RoleExists(dataSource, role.Id))
                _roleRepository.AddRole(dataSource, role);

            var albumCreditRoleTable = AlbumCreditRoleTable(dataSource);
            if (albumCreditRoleTable == null)
                return;

            var albumCreditRole = AlbumCreditRole.Create(albumCredit, role);

            albumCreditRoleTable.InsertOnSubmit(albumCreditRole);
            dataSource.Save();
        }

        /// <summary>
        /// Deletes an album credit role.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="albumCredit">The album credit.</param>
        /// <param name="role">The role.</param>
        public void DeleteAlbumCreditRole(IDataSource dataSource, AlbumCredit albumCredit, Role role)
        {
            if (!dataSource.IsConnected())
                return;

            var albumCreditRoleTable = AlbumCreditRoleTable(dataSource);
            if (albumCreditRoleTable == null)
                return;

            var albumCreditRoleQuery =
                from acr in albumCreditRoleTable
                where acr.AlbumCreditId == albumCredit.Id && acr.RoleId == role.Id
                select acr;

            var albumCreditRole = albumCreditRoleQuery.SingleOrDefault();
            if (albumCreditRole == null)
                return;

            albumCreditRoleTable.DeleteOnSubmit(albumCreditRole);
            dataSource.Save();
        }

        /// <summary>
        /// Deletes the album.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="album">The album.</param>
        public void DeleteAlbum(IDataSource dataSource, Album album)
        {
            if (!dataSource.IsConnected())
                return;

            var albumTable = AlbumTable(dataSource);
            if (albumTable == null)
                return;

            var albumArtworkTable = AlbumArtworkTable(dataSource);
            var albumArtworkQuery =
                from aa in albumArtworkTable
                where aa.AlbumId == album.Id
                select aa;

            albumArtworkTable.DeleteAllOnSubmit(albumArtworkQuery.AsEnumerable());
            dataSource.Save();

            var albumMediaTable = AlbumMediaTable(dataSource);
            var albumMediaQuery =
                from am in albumMediaTable
                where am.AlbumId == album.Id
                select am;

            albumMediaTable.DeleteAllOnSubmit(albumMediaQuery.AsEnumerable()); 
            dataSource.Save();

            var albumReferenceTable = AlbumReferenceTable(dataSource);
            var albumReferencesQuery =
                from ar in albumReferenceTable
                where ar.AlbumId == album.Id
                select ar;

            albumReferenceTable.DeleteAllOnSubmit(albumReferencesQuery.AsEnumerable());
            dataSource.Save();

            var albumActivityTable = AlbumActivityTable(dataSource);
            var albumActivitiesQuery =
                from aa in albumActivityTable
                where aa.AlbumId == album.Id
                select aa;

            albumActivityTable.DeleteAllOnSubmit(albumActivitiesQuery.AsEnumerable());
            dataSource.Save();

            var albumMoodTable = AlbumMoodTable(dataSource);
            var albumMoodsQuery =
                from am in albumMoodTable
                where am.AlbumId == album.Id
                select am;

            albumMoodTable.DeleteAllOnSubmit(albumMoodsQuery.AsEnumerable());
            dataSource.Save();

            var albumTagTable = AlbumTagTable(dataSource);
            var albumTagsQuery =
                from at in albumTagTable
                where at.AlbumId == album.Id
                select at;

            albumTagTable.DeleteAllOnSubmit(albumTagsQuery.AsEnumerable());
            dataSource.Save();

            var albumReviewTable = AlbumReviewTable(dataSource);
            var albumReviewsQuery =
                from ar in albumReviewTable
                where ar.AlbumId == album.Id
                select ar;

            albumReviewTable.DeleteAllOnSubmit(albumReviewsQuery.AsEnumerable());
            dataSource.Save();

            var albumCollaboratorTable = AlbumCollaboratorTable(dataSource);
            var albumCollaboratorsQuery =
                from ac in albumCollaboratorTable
                where ac.AlbumId == album.Id
                select ac;

            albumCollaboratorTable.DeleteAllOnSubmit(albumCollaboratorsQuery.AsEnumerable());
            dataSource.Save();

            var albumReleaseTable = AlbumReleaseTable(dataSource);
            var albumReleaseQuery =
                from ar in albumReleaseTable
                where ar.AlbumId == album.Id
                select ar;

            albumReleaseTable.DeleteAllOnSubmit(albumReleaseQuery.AsEnumerable());
            dataSource.Save();

            var albumTrackTable = AlbumTrackTable(dataSource);
            var albumTrackQuery =
                from at in albumTrackTable
                where at.AlbumId == album.Id
                select at;

            albumTrackTable.DeleteAllOnSubmit(albumTrackQuery.AsEnumerable());
            dataSource.Save();

            var albumCreditTable = AlbumCreditTable(dataSource);
            var albumCreditsQuery =
                from ac in albumCreditTable
                where ac.AlbumId == album.Id
                select ac;

            var credits = albumCreditsQuery.ToList();
            foreach (var credit in credits)
            {
                var c = credit;
                var albumCreditRoleTable = AlbumCreditRoleTable(dataSource);
                var albumCreditRolesQuery =
                    from acr in albumCreditRoleTable
                    where acr.AlbumCreditId == c.Id
                    select acr;

                albumCreditRoleTable.DeleteAllOnSubmit(albumCreditRolesQuery.AsEnumerable());
                dataSource.Save();

                albumCreditTable.DeleteOnSubmit(credit);
                dataSource.Save();
            }

            albumTable.DeleteOnSubmit(album);
            dataSource.Save();
        }

        #region Tables

        private static Table<Album> AlbumTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<Album>() : null;
        }

        private static IEnumerable<Song> SongTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<Song>() : null;
        }

        private static Table<Artist> ArtistTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<Artist>() : null;
        }

        private static IEnumerable<Genre> GenreTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<Genre>() : null;
        }

        private static IEnumerable<TimeSignature> TimeSignatureTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<TimeSignature>() : null;
        }

        private static Table<Holiday> HolidayTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<Holiday>() : null;
        }

        private static Table<AlbumTrack> AlbumTrackTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<AlbumTrack>() : null;
        }

        private static Table<AlbumMedia> AlbumMediaTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<AlbumMedia>() : null;
        }

        private static Table<AlbumReference> AlbumReferenceTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<AlbumReference>() : null;
        }

        private static Table<AlbumActivity> AlbumActivityTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<AlbumActivity>() : null;
        }

        private static Table<AlbumMood> AlbumMoodTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<AlbumMood>() : null;
        }

        private static Table<AlbumReview> AlbumReviewTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<AlbumReview>() : null;
        }

        private static Table<AlbumTag> AlbumTagTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<AlbumTag>() : null;
        }

        private static Table<AlbumArtwork> AlbumArtworkTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<AlbumArtwork>() : null;
        }

        private static Table<AlbumCollaborator> AlbumCollaboratorTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<AlbumCollaborator>() : null;
        }

        private static Table<AlbumCredit> AlbumCreditTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<AlbumCredit>() : null;
        }

        private static Table<AlbumCreditRole> AlbumCreditRoleTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<AlbumCreditRole>() : null;
        }

        private static Table<AlbumRelease> AlbumReleaseTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<AlbumRelease>() : null;
        }

        private static Table<Role> RoleTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<Role>() : null;
        }

        private static IEnumerable<Person> PersonTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<Person>() : null;
        }

        #endregion
    }
}