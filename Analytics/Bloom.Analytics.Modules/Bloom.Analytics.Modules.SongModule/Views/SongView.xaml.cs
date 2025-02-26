﻿using Bloom.Analytics.Modules.SongModule.ViewModels;

namespace Bloom.Analytics.Modules.SongModule.Views
{
    /// <summary>
    /// Interaction logic for SongView.xaml
    /// </summary>
    public partial class SongView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SongView"/> class.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        public SongView(SongViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
