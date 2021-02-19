using Plugin.SimpleAudioPlayer;
using SimpleDrumSequencer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Timers;

namespace SimpleDrumSequencer.Services
{
    public class SimpleDrumSequencerService : ISimpleDrumSequencerService
    {
        public ObservableCollection<SequencerLaneModel> SequencerLanes { get; set; } = new ObservableCollection<SequencerLaneModel>();

        public Random RandomValue = new Random();

        public int Position;

        public bool IsRunning { get; set; } 
        public Timer SequencerTimer = new Timer(126);

        public SimpleDrumSequencerService()
        {
            SequencerTimer.Elapsed += OnTimedEvent;
        }

        public ISimpleDrumSequencerService Randomize()
        {
            foreach (var sequencerLane in SequencerLanes)
            {
                foreach (var step in sequencerLane.SequencerSteps)
                {
                    step.IsActive = RandomValue.NextDouble() > 0.8;
                }
            }
            return this;
        }

        public ISimpleDrumSequencerService Reset()
        {
            foreach (var sequencerLane in SequencerLanes)
            {
                foreach (var step in sequencerLane.SequencerSteps)
                {
                    step.IsActive = false;
                }
            }
            return this;
        }

        public ISimpleDrumSequencerService AddInstrument(string instrumentName, string instrumentNameShort, Stream soundFileStream)
        {
            var audioPlayer = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
            audioPlayer.Load(soundFileStream);
            SequencerLanes.Add(
                  new SequencerLaneModel
                  {
                      InstrumentName = instrumentName,
                      InstrumentNameShort = instrumentNameShort,
                      NumberOfSteps = 16,
                      AudioPlayer = audioPlayer
                  });
            return this;
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            foreach (var sequencerLane in SequencerLanes)
            {
                if (sequencerLane.SequencerSteps[Position % sequencerLane.NumberOfSteps].IsActive)
                    sequencerLane.AudioPlayer.Play();
            }
            Position++;
        }

        public ISimpleDrumSequencerService Start()
        {
            SequencerTimer.Start();
            IsRunning = true; 
            return this;
        }

        public ISimpleDrumSequencerService Stop()
        {
            SequencerTimer.Stop();
            IsRunning = false;
            return this;
        }
    }
}
