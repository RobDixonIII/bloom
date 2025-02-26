﻿using System;
using System.Collections.Generic;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    /// <summary>
    /// Access methods for artist data.
    /// </summary>
    public interface IArtistRepository
    {
        /// <summary>
        /// Gets the artist.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="artistId">The artist identifier.</param>
        Artist GetArtist(IDataSource dataSource, Guid artistId);

        /// <summary>
        /// Finds all artists with the given name.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="artistName">An artist name.</param>
        List<Artist> FindArtist(IDataSource dataSource, string artistName);

        /// <summary>
        /// Lists the artists.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        List<Artist> ListArtists(IDataSource dataSource);

        /// <summary>
        /// Adds an artist.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="artist">The artist.</param>
        void AddArtist(IDataSource dataSource, Artist artist);

        /// <summary>
        /// Adds an artist member.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="member">The artist member.</param>
        void AddArtistMember(IDataSource dataSource, ArtistMember member);

        /// <summary>
        /// Deletes an artist member.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="member">The artist member.</param>
        void DeleteArtistMember(IDataSource dataSource, ArtistMember member);

        /// <summary>
        /// Adds an artist member role.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="member">The member.</param>
        /// <param name="role">The role.</param>
        void AddArtistMemberRole(IDataSource dataSource, ArtistMember member, Role role);

        /// <summary>
        /// Deletes an artist member role.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="member">The member.</param>
        /// <param name="role">The role.</param>
        void DeleteArtistMemberRole(IDataSource dataSource, ArtistMember member, Role role);

        /// <summary>
        /// Adds an artist photo.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="artist">An artist.</param>
        /// <param name="photo">The photo.</param>
        /// <param name="priority">The priority.</param>
        void AddArtistPhoto(IDataSource dataSource, Artist artist, Photo photo, int priority);

        /// <summary>
        /// Deletes the artist photo.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="artist">The artist.</param>
        /// <param name="photo">The photo.</param>
        void DeleteArtistPhoto(IDataSource dataSource, Artist artist, Photo photo);

        /// <summary>
        /// Deletes an artist.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="artist">The artist.</param>
        void DeleteArtist(IDataSource dataSource, Artist artist);
    }
}
