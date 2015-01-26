﻿using System.Windows;
using Bloom.Analytics.Album;
using Bloom.Analytics.Artist;
using Bloom.Analytics.Library;
using Bloom.Analytics.Menu;
using Bloom.Analytics.Person;
using Bloom.Analytics.Playlist;
using Bloom.Analytics.Song;
using Bloom.Services;
using Bloom.Taxonomies;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.UnityExtensions;

namespace Bloom.Analytics
{
    /// <summary>
    /// The bootstrapper initialized the application and resolves dependencies.
    /// </summary>
    public class Bootstrapper : UnityBootstrapper
    {
        /// <summary>
        /// Creates the shell or main window of the application.
        /// </summary>
        /// <returns>
        /// The shell of the application.
        /// </returns>
        /// <remarks>
        /// If the returned instance is a <see cref="T:System.Windows.DependencyObject" />, the
        /// <see cref="T:Microsoft.Practices.Prism.Bootstrapper" /> will attach the default <see cref="T:Microsoft.Practices.Prism.Regions.IRegionManager" /> of
        /// the application in its <see cref="F:Microsoft.Practices.Prism.Regions.RegionManager.RegionManagerProperty" /> attached property
        /// in order to be able to add regions by using the <see cref="F:Microsoft.Practices.Prism.Regions.RegionManager.RegionNameProperty" />
        /// attached property from XAML.
        /// </remarks>
        protected override DependencyObject CreateShell()
        {
            InjectServices();
            return Container.Resolve<Shell>();
        }

        /// <summary>
        /// Initializes the shell.
        /// </summary>
        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (Window) Shell;
            Application.Current.MainWindow.Show();
        }

        /// <summary>
        /// Configures the <see cref="T:Microsoft.Practices.Prism.Modularity.IModuleCatalog" /> used by Prism.
        /// </summary>
        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            var moduleCatalog = (ModuleCatalog) ModuleCatalog;
            moduleCatalog.AddModule(typeof (MenuModule));
            moduleCatalog.AddModule(typeof (TaxonomiesModule));
            moduleCatalog.AddModule(typeof (LibraryModule));
            moduleCatalog.AddModule(typeof (PersonModule));
            moduleCatalog.AddModule(typeof (ArtistModule));
            moduleCatalog.AddModule(typeof (SongModule));
            moduleCatalog.AddModule(typeof (AlbumModule));
            moduleCatalog.AddModule(typeof (PlaylistModule));
        }

        /// <summary>
        /// Injects the services into the DI container.
        /// </summary>
        protected void InjectServices()
        {
            Container.RegisterType<ISkinningService, SkinningService>(new ContainerControlledLifetimeManager());
            Container.Resolve<ISkinningService>();
        }
    }
}