﻿using System;
using System.Collections.Generic;
using Bloom.Browser.Common;
using Bloom.Browser.Menu.ViewModels;
using Telerik.Windows;
using Telerik.Windows.Controls;

namespace Bloom.Browser.Menu.Views
{
    /// <summary>
    /// Interaction logic for MenuView.xaml
    /// </summary>
    public partial class MenuView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuView" /> class.
        /// </summary>
        /// <param name="viewModel">The menu view model.</param>
        public MenuView(MenuViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;

            // Check the current skin.
            foreach (RadMenuItem menuItem in Skins.Items)
            {
                var skinName = (string) menuItem.CommandParameter;
                menuItem.IsChecked = skinName.Equals(viewModel.State.Skin, StringComparison.InvariantCultureIgnoreCase);
            }
        }

        /// <summary>
        /// Gets the application state.
        /// </summary>
        public State State
        {
            get { return ((MenuViewModel) DataContext).State; }
        }

        /// <summary>
        /// Called when a menu item is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RadRoutedEventArgs"/> instance containing the event data.</param>
        private void OnItemClick(object sender, RadRoutedEventArgs e)
        {
            var currentItem = e.OriginalSource as RadMenuItem;
            
            if (currentItem != null && currentItem.IsCheckable && currentItem.Tag != null)
            {
                if ((string) currentItem.CommandParameter == State.Skin)
                {
                    currentItem.IsChecked = true;
                    return;
                }

                var siblingItems = GetSiblingGroupItems(currentItem);
                if (siblingItems == null) 
                    return;

                foreach (var item in siblingItems)
                {
                    if (!Equals(item, currentItem))
                        item.IsChecked = false;
                }
            }
            
        }

        /// <summary>
        /// Gets the sibling group items of the provided current item.
        /// </summary>
        /// <param name="menuItem">The current item.</param>
        private IEnumerable<RadMenuItem> GetSiblingGroupItems(RadMenuItem menuItem)
        {
            var parentItem = menuItem.ParentOfType<RadMenuItem>();
            if (parentItem == null)
                return null;
            
            var items = new List<RadMenuItem>();
            foreach (var item in parentItem.Items)
            {
                var container = parentItem.ItemContainerGenerator.ContainerFromItem(item) as RadMenuItem;
                if (container == null || container.Tag == null)
                    continue;
                
                if (container.Tag.Equals(menuItem.Tag))
                    items.Add(container);
            }
            return items;
        }

    }
}