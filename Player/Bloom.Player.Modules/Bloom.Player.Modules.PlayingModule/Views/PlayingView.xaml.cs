﻿using Bloom.Player.Modules.PlayingModule.ViewModels;

namespace Bloom.Player.Modules.PlayingModule.Views
{
    /// <summary>
    /// Interaction logic for PlayingView.xaml
    /// </summary>
    public partial class PlayingView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayingView" /> class.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        public PlayingView(PlayingViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
