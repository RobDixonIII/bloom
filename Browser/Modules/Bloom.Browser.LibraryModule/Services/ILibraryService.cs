﻿using System;
using Bloom.Browser.Common;
using Bloom.Domain.Models;
using Bloom.State.Domain.Models;

namespace Bloom.Browser.LibraryModule.Services
{
    public interface ILibraryService
    {
        /// <summary>
        /// Shows the create new library modal window.
        /// </summary>
        void ShowCreateNewLibraryModal();

        /// <summary>
        /// Shows the connected libraries modal window.
        /// </summary>
        void ShowConnectedLibrariesModal();

        /// <summary>
        /// Shows the library properties modal window.
        /// </summary>
        /// <param name="library">The library.</param>
        void ShowLibraryPropertiesModal(Library library);
        
        /// <summary>
        /// Creates a new library tab.
        /// </summary>
        void NewLibraryTab(Guid libraryId);

        /// <summary>
        /// Restores the library tab.
        /// </summary>
        /// <param name="tab">The library tab.</param>
        void RestoreLibraryTab(Tab tab);

        /// <summary>
        /// Duplicates a library tab.
        /// </summary>
        /// <param name="tabId">The tab identifier to duplicate.</param>
        void DuplicateLibraryTab(Guid tabId);

        /// <summary>
        /// Changes a library tab view.
        /// </summary>
        /// <param name="tabId">The tab identifier of the view.</param>
        /// <param name="viewType">The view type to change to.</param>
        void ChangeLibraryTabView(Guid tabId, ViewType viewType);
    }
}