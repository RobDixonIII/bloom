﻿using Bloom.Browser.Modules.PersonModule.Services;
using Prism.Modularity;
using Microsoft.Practices.Unity;

namespace Bloom.Browser.Modules.PersonModule
{
    /// <summary>
    /// Browser person module.
    /// </summary>
    [Module(ModuleName = "PersonModule")]
    public class PersonModuleDefinition : IModule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonModule"/> class.
        /// </summary>
        /// <param name="container">The DI container.</param>
        public PersonModuleDefinition(IUnityContainer container)
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
            _container.RegisterType<IPersonService, PersonService>(new ContainerControlledLifetimeManager());
            _container.Resolve(typeof(IPersonService));
        }
    }
}
