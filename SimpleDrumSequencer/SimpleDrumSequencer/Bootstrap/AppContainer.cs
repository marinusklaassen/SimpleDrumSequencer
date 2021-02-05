using System;
using Autofac;
using System.Collections.Generic;
using System.Text;
using SimpleDrumSequencer.ViewModels;
using SimpleDrumSequencer.ViewModels.Base;
using SimpleDrumSequencer.Services.Navigation;
using SimpleDrumSequencer.Constracts.Navigation;
using Xamarin.Forms;
using SimpleDrumSequencer.Views;

namespace SimpleDrumSequencer.Bootstrap
{
    public class AppContainer
    {
        private static IContainer _container;

        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            LoadViewModels(builder);
            LoadViews(builder);

            builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();

            _container = builder.Build();
        }

        private static void LoadViewModels(ContainerBuilder builder)
        {
            builder.RegisterType<ViewModelBase>();
            builder.RegisterType<SimpleDrumSequencerViewModel>();
        }

        private static void LoadViews(ContainerBuilder builder)
        {
            builder.RegisterType<SimpleDrumSequencerView>();
        }

        public static object Resolve(Type typeName)
        {
            return _container.Resolve(typeName);
        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}
