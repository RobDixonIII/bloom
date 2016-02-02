﻿using System;
using Bloom.Analytics.Common;
using Bloom.Domain.Models;

namespace Bloom.Analytics.LibraryModule.ViewModels
{
    public class LibraryViewModel
    {
        public LibraryViewModel(Library library, ViewType viewType, Guid tabId)
        {
            ViewType = viewType;
            Library = library;
            TabId = tabId;
        }

        public Guid TabId { get; set; }

        public Library Library { get; set; }

        public ViewType ViewType { get; set; }
    }
}