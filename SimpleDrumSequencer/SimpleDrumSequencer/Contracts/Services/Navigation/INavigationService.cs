using SimpleDrumSequencer.ViewModels.Base;
using System.Threading.Tasks;

namespace SimpleDrumSequencer.Constracts.Navigation
{
    public interface INavigationService
    {
        Task NavigateToAsync<TViewModel>();
    }
}
