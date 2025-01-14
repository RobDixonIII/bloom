﻿using System.IO;

namespace Bloom.Services
{
    /// <summary>
    /// Settings for Bloom.Services.
    /// </summary>
    public static class Settings
    {
        /// <summary>
        /// Gets the local data path.
        /// </summary>
        public static string LocalDataPath => Data.Settings.LocalDataPath;

        /// <summary>
        /// Gets the tests data path.
        /// </summary>
        public static string TestsDataPath => Data.Settings.TestsDataPath;

        /// <summary>
        /// Gets the browser executable path.
        /// </summary>
        public static string BrowserExecutablePath => Properties.Settings.Default.BrowserExecutablePath;

        /// <summary>
        /// Gets the name of the browser process.
        /// </summary>
        public static string BrowserProcessName => Properties.Settings.Default.BrowserProcessName;

        /// <summary>
        /// Gets the player executable path.
        /// </summary>
        public static string PlayerExecutablePath => Properties.Settings.Default.PlayerExecutablePath;

        /// <summary>
        /// Gets the name of the player process.
        /// </summary>
        public static string PlayerProcessName => Properties.Settings.Default.PlayerProcessName;

        /// <summary>
        /// Gets the analytics executable path.
        /// </summary>
        public static string AnalyticsExecutablePath => Properties.Settings.Default.AnalyticsExecutablePath;

        /// <summary>
        /// Gets the name of the analytics process.
        /// </summary>
        public static string AnalyticsProcessName => Properties.Settings.Default.AnalyticsProcessName;

        /// <summary>
        /// Gets the user profiles folder.
        /// </summary>
        public static string UserProfilesPath => Path.Combine(LocalDataPath, Properties.Settings.Default.UserProfilesFolder);

        /// <summary>
        /// Gets the people library folder.
        /// </summary>
        public static string PeopleLibraryFolder => Properties.Settings.Default.PeopleFolder;

        /// <summary>
        /// Gets the artists library folder.
        /// </summary>
        public static string ArtistsLibraryFolder => Properties.Settings.Default.ArtistsFolder;

        /// <summary>
        /// Gets the mixed artists library folder.
        /// </summary>
        public static string MixedArtistsLibraryFolder => Properties.Settings.Default.MixedArtistFolder;

        /// <summary>
        /// Gets the playlists library folder.
        /// </summary>
        public static string PlaylistsLibraryFolder => Properties.Settings.Default.PlaylistsFolder;

        /// <summary>
        /// Gets the cover artwork file name.
        /// </summary>
        public static string CoverArtFileName => Properties.Settings.Default.CoverArtFileName;

        /// <summary>
        /// Gets the name of an unknown entity.
        /// </summary>
        public static string UnknownName => Properties.Settings.Default.UnknownName;
    }
}
