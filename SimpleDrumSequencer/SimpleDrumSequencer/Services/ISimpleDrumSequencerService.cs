using SimpleDrumSequencer.Models;
using System.Collections.ObjectModel;
using System.IO;

namespace SimpleDrumSequencer.Services
{
    public interface ISimpleDrumSequencerService
    {
        ObservableCollection<SequencerLaneModel> SequencerLanes { get; set; }
        bool IsRunning { get; }

        ISimpleDrumSequencerService AddInstrument(string instrumentName, string instrumentNameShort, Stream soundFileStream);
        ISimpleDrumSequencerService Randomize();
        ISimpleDrumSequencerService Reset();
        ISimpleDrumSequencerService Start();
        ISimpleDrumSequencerService Stop();
    }
}