﻿using Bloom.Player.Common;
using Bloom.Services;

namespace Bloom.Player
{
    /// <summary>
    /// Interaction logic for Shell.xaml
    /// </summary>
    public partial class Shell
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Shell" /> class.
        /// </summary>
        /// <param name="skinningService">The skinning service.</param>
        public Shell(ISkinningService skinningService)
        {
            InitializeComponent();
            var state = new State();
            DataContext = state;

            skinningService.SetSkin(state.Skin);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Window.Closing" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains the event data.</param>
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            var state = (State) DataContext;
            state.Save();
        }
    }
}