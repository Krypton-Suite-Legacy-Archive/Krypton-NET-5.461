﻿// *****************************************************************************
// BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
//  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
// The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to license terms.
// 
//  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV) 2017 - 2020. All rights reserved. (https://github.com/Wagnerp/Krypton-NET-5.461)
//  Version 5.461.0.0  www.ComponentFactory.com
// *****************************************************************************

using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Navigator
{
    /// <summary>
    /// Navigator view element for drawing a tab check button for a krypton page.
    /// </summary>
    internal class ViewDrawNavCheckButtonTab : ViewDrawNavCheckButtonBar
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the ViewDrawNavCheckButtonTab class.
        /// </summary>
        /// <param name="navigator">Owning navigator instance.</param>
        /// <param name="page">Page this check button represents.</param>
        /// <param name="orientation">Orientation for the check button.</param>
        public ViewDrawNavCheckButtonTab(KryptonNavigator navigator,
                                         KryptonPage page,
                                         VisualOrientation orientation)
            : base(navigator, page, orientation,
                   page.StateDisabled.Tab, 
                   page.StateNormal.Tab,
                   page.StateTracking.Tab, 
                   page.StatePressed.Tab,
                   page.StateSelected.Tab,
                   page.OverrideFocus.Tab)
        {
        }

        /// <summary>
        /// Obtains the String representation of this instance.
        /// </summary>
        /// <returns>User readable name of the instance.</returns>
        public override string ToString()
        {
            // Return the class name and instance identifier
            return "ViewDrawNavCheckButtonTab:" + Id;
        }
        #endregion

        #region UpdateButtonSpecMapping
        /// <summary>
        /// Update the button spec manager mapping to reflect current settings.
        /// </summary>
        public override void UpdateButtonSpecMapping()
        {
            // Update the button spec manager for this tab to use a tab style for remapping
            ButtonSpecManager.SetRemapTarget(Navigator.Bar.TabStyle);
            ButtonSpecManager.RecreateButtons();
        }
        #endregion

        #region ButtonClickOnDown
        /// <summary>
        /// Should the item be selected on the mouse down.
        /// </summary>
        protected override bool ButtonClickOnDown => true;

        #endregion
    }
}
