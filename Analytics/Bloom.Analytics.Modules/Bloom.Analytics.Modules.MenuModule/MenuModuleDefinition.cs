﻿using Bloom.Analytics.Modules.MenuModule.Views;
using Prism.Modularity;
using Prism.Regions;

namespace Bloom.Analytics.Modules.MenuModule
{
    /// <summary>
    /// Analytics menu Prism module.
    /// </summary>
    [Module(ModuleName = "MenuModule")]
    public class MenuModuleDefinition : IModule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuModule"/> class.
        /// </summary>
        /// <param name="regionManager">The region manager.</param>
        public MenuModuleDefinition(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }
        private readonly IRegionManager _regionManager;

        /// <summary>
        /// Notifies the module that it has be initialized.
        /// </summary>
        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion(Common.Settings.MenuRegion, typeof(MenuView));
        }
    }
}
