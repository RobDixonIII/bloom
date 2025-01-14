﻿using System;
using Bloom.Data.Interfaces;
using Bloom.Events;
using Bloom.Modules.LibraryModule.Services;
using Bloom.Services;
using Bloom.State.Data.Respositories;
using Bloom.State.Domain.Models;
using Prism.Events;

namespace Bloom.Analytics.State.Services
{
    /// <summary>
    /// Service for managing the analytics application state.
    /// </summary>
    public class AnalyticsStateService : TabbedStateBaseService, IAnalyticsStateService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnalyticsStateService" /> class.
        /// </summary>
        /// <param name="stateDataSource">The state data source.</param>
        /// <param name="suiteStateRepository">The suite state repository.</param>
        /// <param name="analyticsStateRepository">The analytics state repository.</param>
        /// <param name="libraryConnectionRepository">The library connection repository.</param>
        /// <param name="tabRepository">The tab repository.</param>
        /// <param name="sharedLibraryService">The shared library service.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        public AnalyticsStateService(IDataSource stateDataSource, ISuiteStateRepository suiteStateRepository, IAnalyticsStateRepository analyticsStateRepository,
            ILibraryConnectionRepository libraryConnectionRepository, ITabRepository tabRepository, ISharedLibraryService sharedLibraryService, IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
            StateDataSource = stateDataSource;
            SuiteStateRepository = suiteStateRepository;
            LibraryConnectionRepository = libraryConnectionRepository;
            TabRepository = tabRepository;
            _sharedLibraryService = sharedLibraryService;
            _analyticsStateRepository = analyticsStateRepository;
            
            EventAggregator.GetEvent<SaveStateEvent>().Subscribe(SaveState);
            EventAggregator.GetEvent<ConnectionRemovedEvent>().Subscribe(CloseLibraryTabs);
        }
        private readonly IAnalyticsStateRepository _analyticsStateRepository;
        private readonly ISharedLibraryService _sharedLibraryService;

        /// <summary>
        /// Initializes the analytics application state.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">The file path to the state database file has not been specified.</exception>
        public AnalyticsState InitializeState(User user)
        {
            State = _analyticsStateRepository.GetAnalyticsState(user) ?? NewAnalyticsState(user);
            var process = ((AnalyticsState) State).ProcessName;
            var now = DateTime.Now;
            SuiteState = SuiteStateRepository.GetSuiteState() ?? NewSuiteState(process, now);
            SuiteState.LastProcessAccess = process;
            SuiteState.ProcessAccessedOn = now;

            if (State.User == null)
                return (AnalyticsState) State;

            State.User.LastLogin = DateTime.Now;
            if (State.Connections == null || State.Connections.Count <= 0)
                return (AnalyticsState) State;

            _sharedLibraryService.ConnectLibraries(State.Connections, user, false, true);
            SaveState();

            return (AnalyticsState) State;
        }

        private SuiteState NewSuiteState(string process, DateTime accessedOn)
        {
            var suiteState = new SuiteState
            {
                LastProcessAccess = process,
                ProcessAccessedOn = accessedOn
            };
            SuiteStateRepository.AddSuiteState(suiteState);
            SaveState();

            return suiteState;
        }

        private AnalyticsState NewAnalyticsState(User user)
        {
            var analyticsState = new AnalyticsState();
            analyticsState.SetUser(user);
            analyticsState.Connections = LibraryConnectionRepository.ListLibraryConnections(true);
            _analyticsStateRepository.AddAnalyticsState(analyticsState);

            return analyticsState;
        }

        private void SaveState(ApplicationState state)
        {
            State = (AnalyticsState) state;
            SaveState();
        }
    }
}
