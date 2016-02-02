﻿using System;
using Bloom.Common;
using Bloom.State.Domain.Models;

namespace Bloom.Analytics.ArtistModule.Services
{
    public interface IArtistService
    {
        void NewArtistTab(Buid artistId);

        void RestoreArtistTab(Tab tab);

        void DuplicateArtistTab(Guid tabId);
    }
}