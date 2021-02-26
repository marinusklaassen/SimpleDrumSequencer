using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleDrumSequencer.Utility
{
    public class PositionChangedEventArgs : EventArgs
    {
        public int Position { get; set; }
    }
}
