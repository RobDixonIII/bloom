﻿using System;
using System.Collections.Generic;
using System.Linq;
using Bloom.Browser.Person.ViewModels;
using Bloom.Browser.Person.Views;
using Bloom.Controls;
using Bloom.PubSubEvents;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Bloom.Browser.Person.Services
{
    public class PersonService : IPersonService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonService"/> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        public PersonService(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _tabs = new List<Tab>();

            // Subscribe to events
            _eventAggregator.GetEvent<NewPersonTabEvent>().Subscribe(NewPersonTab);
            _eventAggregator.GetEvent<DuplicateTabEvent>().Subscribe(DuplicatePersonTab);
        }
        private readonly IEventAggregator _eventAggregator;

        public void NewPersonTab(object nothing)
        {
            NewPersonTab();
        }

        public void NewPersonTab()
        {
            var personViewModel = new PersonViewModel();
            var personView = new PersonView(personViewModel);
            var personTab = new Tab
            {
                Header = "Person",
                Content = personView
            };

            _tabs.Add(personTab);
            _eventAggregator.GetEvent<AddTabEvent>().Publish(personTab);
        }

        public void DuplicatePersonTab(Guid tabId)
        {
            var existingTab = _tabs.FirstOrDefault(tab => tab.Id == tabId);
            if (existingTab == null)
                return;

            var personViewModel = new PersonViewModel();
            var personView = new PersonView(personViewModel);
            var personTab = new Tab
            {
                Header = "Person",
                Content = personView
            };

            _tabs.Add(personTab);
            _eventAggregator.GetEvent<AddTabEvent>().Publish(personTab);
        }

        private readonly List<Tab> _tabs;
    }
}