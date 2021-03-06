﻿using System;
using MonoGame.Extended.TextureAtlases;

namespace MonoGame.Extended.Gui.Controls
{
    public class GuiButton : GuiControl
    {
        public GuiButton()
        {
        }

        public GuiButton(TextureRegion2D backgroundRegion)
            : base(backgroundRegion)
        {
        }

        public event EventHandler Clicked;
        public event EventHandler PressedStateChanged;

        private bool _isPressed;
        public bool IsPressed
        {
            get { return _isPressed; }
            set
            {
                if (_isPressed != value)
                {
                    _isPressed = value;
                    PressedStyle?.ApplyIf(this, _isPressed);
                    PressedStateChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private GuiControlStyle _pressedStyle;
        public GuiControlStyle PressedStyle
        {
            get { return _pressedStyle; }
            set
            {
                if (_pressedStyle != value)
                {
                    _pressedStyle = value;
                    PressedStyle?.ApplyIf(this, _isPressed);
                }
            }
        }

        private bool _isPointerDown;

        public override void OnPointerDown(GuiPointerEventArgs args)
        {
            base.OnPointerDown(args);

            if (IsEnabled)
            {
                _isPointerDown = true;
                IsPressed = true;
            }
        }

        public override void OnPointerUp(GuiPointerEventArgs args)
        {
            base.OnPointerUp(args);

            _isPointerDown = false;

            if (IsPressed)
            {
                IsPressed = false;

                if (BoundingRectangle.Contains(args.Position) && IsEnabled)
                    Clicked?.Invoke(this, EventArgs.Empty);
            }
        }

        public override void OnPointerEnter(GuiPointerEventArgs args)
        {
            base.OnPointerEnter(args);

            if (IsEnabled && _isPointerDown)
                IsPressed = true;
        }

        public override void OnPointerLeave(GuiPointerEventArgs args)
        {
            base.OnPointerLeave(args);

            if (IsEnabled)
                IsPressed = false;
        }
    }
}