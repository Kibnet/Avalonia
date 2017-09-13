﻿using System;
using Avalonia.Input;
using Avalonia.Threading;

namespace Avalonia.Controls
{
    public class RepeatButton : Button
    {
        /// <summary>
        /// Defines the <see cref="Delay"/> property.
        /// </summary>
        public static readonly StyledProperty<int> DelayProperty =
            AvaloniaProperty.Register<Button, int>(nameof(Delay), 100);

        private DispatcherTimer _repeatTimer;

        /// <summary>
        /// Gets or sets the amount of time, in milliseconds, of repeating clicks.
        /// </summary>
        public int Delay
        {
            get { return GetValue(DelayProperty); }
            set { SetValue(DelayProperty, value); }
        }

        private void StartTimer()
        {
            if (_repeatTimer == null)
            {
                _repeatTimer = new DispatcherTimer();
                _repeatTimer.Tick += (o, e) => OnClick();
            }

            if (_repeatTimer.IsEnabled) return;

            _repeatTimer.Interval = TimeSpan.FromMilliseconds(Delay);
            _repeatTimer.Start();
        }

        private void StopTimer()
        {
            _repeatTimer?.Stop();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.Key == Key.Space)
            {
                StartTimer();
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);

            StopTimer();
        }

        protected override void OnPointerPressed(PointerPressedEventArgs e)
        {
            base.OnPointerPressed(e);

            if (e.MouseButton == MouseButton.Left)
            {
                StartTimer();
            }
        }

        protected override void OnPointerReleased(PointerReleasedEventArgs e)
        {
            base.OnPointerReleased(e);

            if (e.MouseButton == MouseButton.Left)
            {
                StopTimer();
            }
        }
    }
}