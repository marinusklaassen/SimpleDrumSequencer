using Plugin.SimpleAudioPlayer;
using SimpleDrumSequencer.Models.Base;
using SimpleDrumSequencer.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace SimpleDrumSequencer.Models
{
    public class SequencerLaneModel : ModelBase
    {
        public ObservableCollection<SequencerStepModel> SequencerSteps { get; set; } = new ObservableCollection<SequencerStepModel>();

        string instrumentName = String.Empty;
        public string InstrumentName
        {
            get { return instrumentName; }
            set { SetProperty(ref instrumentName, value); }
        }

        string instrumentNameShort = String.Empty;
        public string InstrumentNameShort
        {
            get { return instrumentNameShort; }
            set { SetProperty(ref instrumentNameShort, value); }
        }

        int numberOfSteps;
        public int NumberOfSteps
        {
            get { return numberOfSteps; }
            set
            {
                SequencerSteps.Clear();
                for(int position = 0; position < value; position++)
                {
                    SequencerSteps.Add(new SequencerStepModel { Position = position });
                }
                SetProperty(ref numberOfSteps, value);
            }
        }

        public ISimpleAudioPlayer AudioPlayer { get; set; }
    }
}

