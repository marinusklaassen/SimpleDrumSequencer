using SimpleDrumSequencer.Models;
using System.Windows.Input;

namespace SimpleDrumSequencer.ViewModels
{
    public interface ISimpleDrumSequencerViewModel
    {
        bool IsRunning { get; set; }
        string LastPlayedInstrumentName { get; set; }
        string LastPlayedInstrumentNameShort { get; set; }
        ICommand PlaySoundCommand { get; }
        ICommand RandomizeCommand { get; }
        ICommand StartCommand { get; }
        ICommand StopCommand { get; }

        void OnPlaySoundCommand(SequencerLaneModel sequencerLane);
        void OnRandomizeCommand();
        void OnStartCommand();
        void OnStopCommand();
    }
}