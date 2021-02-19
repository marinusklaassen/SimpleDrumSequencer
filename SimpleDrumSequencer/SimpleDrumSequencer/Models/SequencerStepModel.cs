using SimpleDrumSequencer.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleDrumSequencer.Models
{
    public class SequencerStepModel : ModelBase
    {
        bool on = false;
        public bool IsActive
        {
            get { return on; }
            set { SetProperty(ref on, value); }
        }
        int position = 0;
        public int Position
        {
            get { return position; }
            set { SetProperty(ref position, value); }
        }
    }
}

