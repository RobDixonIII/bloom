﻿using System;

namespace Bloom.Browser.Modules.HomeModule.ViewModels
{
    /// <summary>
    /// View model for GettingStartedView.xaml
    /// </summary>
    public class GettingStartedViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GettingStartedViewModel"/> class.
        /// </summary>
        /// <param name="tabId">The tab identifier.</param>
        public GettingStartedViewModel(Guid tabId)
        {
            TabId = tabId;
        }

        /// <summary>
        /// Gets or sets the tab identifier.
        /// </summary>
        public Guid TabId { get; set; }
    }
}
