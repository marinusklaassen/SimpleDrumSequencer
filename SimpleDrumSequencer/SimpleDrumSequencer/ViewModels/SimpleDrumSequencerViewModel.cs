using SimpleDrumSequencer.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SimpleDrumSequencer.ViewModels
{
    public class SequenceLane : ViewModelBase
    {
        public ObservableCollection<SequenceStep> SequenceSteps { get; set; } = new ObservableCollection<SequenceStep>();

        string instrument = String.Empty;
        public string Instrument
        {
            get { return instrument; }
            set { SetProperty(ref instrument, value); }
        }

        int numberOfSteps;
        public int NumberOfSteps
        {
            get { return numberOfSteps; }
            set
            {
                SequenceSteps.Clear();
                foreach (var position in Enumerable.Range(0, value))
                {
                    SequenceSteps.Add(new SequenceStep { Position = position });
                }
                SetProperty(ref numberOfSteps, value);
            }
        }
    }

    public class SequenceStep : ViewModelBase
    {
        bool on = false;
        public bool On
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

    public class SimpleDrumSequencerViewModel : ViewModelBase
    {
        public Random random = new Random();

        protected List<string> Instruments = new List<string> { "Bass drum", "Snare drum", "Hihat", "Cymbal", "High tom", "Mid tom", "Low tom" };

        public ObservableCollection<SequenceLane> SequenceLanes { get; set; } = new ObservableCollection<SequenceLane>();

        public SimpleDrumSequencerViewModel()
        {
            RandomizeCommand = new Command(OnRandomizeCommand);

            foreach (var instrument in Instruments)
            {
                SequenceLanes.Add(
                    new SequenceLane
                    {
                        Instrument = instrument,
                        NumberOfSteps = 16
                    });
            }
        }

        public void OnRandomizeCommand()
        {
            foreach (var sequencerLane in SequenceLanes)
            {
                foreach (var step in sequencerLane.SequenceSteps)
                {
                    step.On = random.NextDouble() > 0.5;
                }
            }
        }

        public ICommand RandomizeCommand { get; }
    }
}
