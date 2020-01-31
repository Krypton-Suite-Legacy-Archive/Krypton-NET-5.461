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

using System.Drawing;
using System.Diagnostics;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Allow the background values to be forced to fixed values.
    /// </summary>
    public class PaletteBorderInheritForced : PaletteBorderInherit
    {
        #region Instance Fields
        private IPaletteBorder _inherit;
        private PaletteDrawBorders _forceBorderEdges;
        private bool _forceBorders;

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the PaletteBorderInheritForced class.
        /// </summary>
        /// <param name="inherit">Border palette to inherit from.</param>
        public PaletteBorderInheritForced(IPaletteBorder inherit)
        {
            // Remember inheritance border
            _inherit = inherit;

            // Default values
            MaxBorderEdges = PaletteDrawBorders.All;
            ForceGraphicsHint = PaletteGraphicsHint.Inherit;
            BorderIgnoreNormal = false;
        }
        #endregion

        #region SetInherit
        /// <summary>
        /// Gets and sets the palette to inherit from.
        /// </summary>
        public void SetInherit(IPaletteBorder paletteBorder)
        {
            Debug.Assert(paletteBorder != null);
            _inherit = paletteBorder; 
        }
        #endregion

        #region ForceBorderEdges
        /// <summary>
        /// Force the border edges to a particular value.
        /// </summary>
        /// <param name="forceBorderEdges">Borders to force.</param>
        public void ForceBorderEdges(PaletteDrawBorders forceBorderEdges)
        {
            _forceBorderEdges = forceBorderEdges;
            _forceBorders = true;
        }
        #endregion

        #region MaxBorderEdges
        /// <summary>
        /// Gets and sets the maximum edges allowed.
        /// </summary>
        public PaletteDrawBorders MaxBorderEdges { get; set; }

        #endregion

        #region BorderIgnoreNormal
        /// <summary>
        /// Gets and sets the ignoring of normal borders.
        /// </summary>
        public bool BorderIgnoreNormal { get; set; }

        #endregion

        #region ForceGraphicsHint
        /// <summary>
        /// Gets and sets the forced value for the graphics hint.
        /// </summary>
        public PaletteGraphicsHint ForceGraphicsHint { get; set; }

        #endregion

        #region IPaletteBorder
        /// <summary>
        /// Gets a value indicating if border should be drawn.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>InheritBool value.</returns>
        public override InheritBool GetBorderDraw(PaletteState state)
        {
            return _inherit.GetBorderDraw(state);
        }

        /// <summary>
        /// Gets a value indicating which borders to draw.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteDrawBorders value.</returns>
        public override PaletteDrawBorders GetBorderDrawBorders(PaletteState state)
        {
            if (_forceBorders)
            {
                return _forceBorderEdges;
            }
            else
            {
                // If no border edges are allowed then provide none
                if ((MaxBorderEdges == PaletteDrawBorders.None) || (BorderIgnoreNormal && (state == PaletteState.Normal)))
                {
                    return PaletteDrawBorders.None;
                }
                else
                {
                    // Get the requested set of edges
                    PaletteDrawBorders inheritEdges = _inherit.GetBorderDrawBorders(state);

                    // Limit the edges to those allowed
                    return (inheritEdges & MaxBorderEdges);
                }
            }
        }

        /// <summary>
        /// Gets the graphics drawing hint.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>PaletteGraphicsHint value.</returns>
        public override PaletteGraphicsHint GetBorderGraphicsHint(PaletteState state) =>
            ForceGraphicsHint != PaletteGraphicsHint.Inherit ? ForceGraphicsHint : _inherit.GetBorderGraphicsHint(state);

        /// <summary>
        /// Gets the first border color.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetBorderColor1(PaletteState state) => _inherit.GetBorderColor1(state);

        /// <summary>
        /// Gets the second border color.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color value.</returns>
        public override Color GetBorderColor2(PaletteState state) => _inherit.GetBorderColor2(state);

        /// <summary>
        /// Gets the color drawing style.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color drawing style.</returns>
        public override PaletteColorStyle GetBorderColorStyle(PaletteState state) => _inherit.GetBorderColorStyle(state);

        /// <summary>
        /// Gets the color alignment style.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Color alignment style.</returns>
        public override PaletteRectangleAlign GetBorderColorAlign(PaletteState state) => _inherit.GetBorderColorAlign(state);

        /// <summary>
        /// Gets the color border angle.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Angle used for color drawing.</returns>
        public override float GetBorderColorAngle(PaletteState state) => _inherit.GetBorderColorAngle(state);

        /// <summary>
        /// Gets the border width.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Border width.</returns>
        public override int GetBorderWidth(PaletteState state) => _inherit.GetBorderWidth(state);

        /// <summary>
        /// Gets the border rounding.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Border rounding.</returns>
        public override int GetBorderRounding(PaletteState state) => _inherit.GetBorderRounding(state);

        /// <summary>
        /// Gets a border image.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image instance.</returns>
        public override Image GetBorderImage(PaletteState state) => _inherit.GetBorderImage(state);

        /// <summary>
        /// Gets the border image style.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image style value.</returns>
        public override PaletteImageStyle GetBorderImageStyle(PaletteState state) => _inherit.GetBorderImageStyle(state);

        /// <summary>
        /// Gets the image alignment style.
        /// </summary>
        /// <param name="state">Palette value should be applicable to this state.</param>
        /// <returns>Image alignment style.</returns>
        public override PaletteRectangleAlign GetBorderImageAlign(PaletteState state) => _inherit.GetBorderImageAlign(state);
        #endregion
    }
}
