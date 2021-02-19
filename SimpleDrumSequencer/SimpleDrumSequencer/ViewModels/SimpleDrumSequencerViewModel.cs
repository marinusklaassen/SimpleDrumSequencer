using Plugin.SimpleAudioPlayer;
using SimpleDrumSequencer.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
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
        
        Stream GetStreamFromFile(string filename)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
                       var stream = assembly.GetManifestResourceStream("SimpleDrumSequencer." + "Audio.DrumKit.Kick 01.wav");

            return stream;
        }

        protected ISimpleAudioPlayer AudioPlayer = null;


        public Random random = new Random();

        protected List<string> Instruments = new List<string> { "Bass drum", "Snare drum", "Hihat", "Cymbal", "High tom", "Mid tom", "Low tom" };

        bool isRunning = false;
        public bool IsRunning
        {
            get { return isRunning; }
            set { SetProperty(ref isRunning, value); }
        }

        string lastPlayedInstrument = String.Empty;
        public string LastPlayedInstrument
        {
            get { return lastPlayedInstrument; }
            set { SetProperty(ref lastPlayedInstrument, value); }
        }

        public ObservableCollection<SequenceLane> SequenceLanes { get; set; } = new ObservableCollection<SequenceLane>();

        public SimpleDrumSequencerViewModel()
        {
            RandomizeCommand = new Command(OnRandomizeCommand);
            StartCommand = new Command(OnStartCommand);
            StopCommand = new Command(OnStopCommand);
            PlaySoundCommand = new Command<SequenceLane>(OnPlaySoundCommand);


            var stream = GetStreamFromFile("xxx.mp3");
            AudioPlayer = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
            AudioPlayer.Load(stream);


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

        public void OnStartCommand()
        {
            IsRunning = true;
        }

        public void OnStopCommand()
        {
            IsRunning = false;
        } 

        public void OnPlaySoundCommand(SequenceLane sequenceLane)
        {
            LastPlayedInstrument = sequenceLane.Instrument;
            AudioPlayer.Play();

        }

        public ICommand RandomizeCommand { get; }
        public ICommand StartCommand { get; }
        public ICommand StopCommand { get; }
        public ICommand PlaySoundCommand { get; }
    }
}

