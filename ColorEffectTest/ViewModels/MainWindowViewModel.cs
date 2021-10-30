using Reactive.Bindings;

namespace ColorEffectTest.ViewModels
{
    public class MainWindowViewModel
    {
        public ReactiveProperty<string> Title { get; set; } = new ReactiveProperty<string>("Color Effect Test");

        public ReactiveProperty<Effects.ArrayEffect> ArrayEffect { get; set; } = new ReactiveProperty<Effects.ArrayEffect>();

        public MainWindowViewModel()
        {

        }
    }
}
