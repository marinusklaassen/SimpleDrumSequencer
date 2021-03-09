using HighPrecisionTimer;
using Plugin.SimpleAudioPlayer;
using SimpleDrumSequencer.Models;
using SimpleDrumSequencer.Multimedia;
using SimpleDrumSequencer.Utility;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

// using System.Timers;
namespace SimpleDrumSequencer.Services
{
    public class SimpleDrumSequencerService : ISimpleDrumSequencerService
    {
        public ObservableCollection<SequencerLaneModel> SequencerLanes { get; set; } = new ObservableCollection<SequencerLaneModel>();

        public Random RandomValue = new Random();
        public int Position;

        public event EventHandler<PositionChangedEventArgs> PositionChanged;

        public bool IsRunning { get; set; }
        public Timer SequencerTimer = new Timer { Period = 126, Resolution = 1 };

        public MultimediaTimer HighTimes = new HighPrecisionTimer.MultimediaTimer() { Interval = 126 };

        public SimpleDrumSequencerService() // Seems to be a bit of queuing 
        {
            // SequencerTimer.Tick +=  new System.EventHandler(this.OnTimedEvent); 
            HighTimes.Elapsed += (o, e) =>
            {
                Parallel.ForEach(SequencerLanes, sequencerLane =>
                {
                    if (sequencerLane.SequencerSteps[Position].IsActive)
                        Task.Run(() => // Adding a Task.Run to call play audio actually minimizes gives a real differency by 5ms. 
                        {
                            sequencerLane.AudioPlayer.Play();
                        });
                });

                PositionChanged.Invoke(this, new PositionChangedEventArgs { Position = Position });
                Position = (Position + 1) % 16;
            };
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

        Stopwatch stopWatch = new Stopwatch();
        int lowestInterval;
        int highestInterval;

        private void OnTimedEvent(object sender, System.EventArgs e)
        {

            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;

            if (ts.Milliseconds < lowestInterval || lowestInterval == 0) lowestInterval = ts.Milliseconds;
            if (ts.Milliseconds > highestInterval) highestInterval = ts.Milliseconds;

            Debug.WriteLine("RunTime " + ts.Milliseconds.ToString());
            Debug.WriteLine("Lowest " + lowestInterval.ToString());
            Debug.WriteLine("Highest " + highestInterval.ToString());

            Position %= 16;
            foreach (var sequencerLane in SequencerLanes)
            {
                if (sequencerLane.SequencerSteps[Position].IsActive)
                    Task.Run(() => // Adding a Task.Run to call play audio actually minimizes gives a real differency by 5ms. 
                    {
                        sequencerLane.AudioPlayer.Play();
                    });
            }

            PositionChanged.Invoke(this, new PositionChangedEventArgs { Position = Position });
            Position++;
            stopWatch.Reset();
            stopWatch.Start();
        }

        public ISimpleDrumSequencerService Start()
        {
            HighTimes.Start();
            // SequencerTimer.Start();
            IsRunning = true;
            return this;
        }

        public ISimpleDrumSequencerService Stop()
        {
            HighTimes.Stop();
            //   SequencerTimer.Stop();
            IsRunning = false;
            return this;
        }

        public ISimpleDrumSequencerService SetVolume(double volume)
        {
            foreach (var audioPlayer in SequencerLanes.Select(o => o.AudioPlayer).ToList())
            {
                audioPlayer.Volume = volume;
            }
            return this;
        }
    }
}
