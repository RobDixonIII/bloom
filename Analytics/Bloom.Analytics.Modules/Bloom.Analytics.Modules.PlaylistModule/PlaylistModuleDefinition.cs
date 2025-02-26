﻿using Bloom.Analytics.Modules.PlaylistModule.Services;
using Prism.Modularity;
using Microsoft.Practices.Unity;

namespace Bloom.Analytics.Modules.PlaylistModule
{
    /// <summary>
    /// Analytics playlist module.
    /// </summary>
    [Module(ModuleName = "PlaylistModule")]
    public class PlaylistModuleDefinition : IModule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlaylistModule"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public PlaylistModuleDefinition(IUnityContainer container)
        {
            _container = container;
        }
        private readonly IUnityContainer _container;

        /// <summary>
        /// Notifies the module that it has be initialized.
        /// </summary>
        public void Initialize()
        {
            // Register services this module provides
            _container.RegisterType<IPlaylistService, PlaylistService>(new ContainerControlledLifetimeManager());
            _container.Resolve(typeof(IPlaylistService));
        }
    }
}
