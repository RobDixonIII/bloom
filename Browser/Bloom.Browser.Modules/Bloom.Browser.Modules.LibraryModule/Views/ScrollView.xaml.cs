﻿using Bloom.Browser.Modules.LibraryModule.ViewModels;

namespace Bloom.Browser.Modules.LibraryModule.Views
{
    /// <summary>
    /// Interaction logic for ScrollView.xaml
    /// </summary>
    public partial class ScrollView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScrollView"/> class.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        public ScrollView(ScrollViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
