using ejercicio2.Models;
using ejercicio2.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en http://go.microsoft.com/fwlink/?LinkId=234238

namespace ejercicio2.Views
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Detail : Page
    {
        public Detail()
        {
            this.InitializeComponent();
            this.viewModel = (ViewModels.clsMainPageVM)this.DataContext;
        }
        public clsMainPageVM viewModel { get; }

        public clsMainPageVM ViewModel { get; }
        #region CODIGO DEL EJEMPLO DE GITHUB
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            clsPersona miPersona;
            if (e.Parameter == null)
            {
                miPersona = new clsPersona();
            }
            else
            {
                miPersona = (clsPersona)e.Parameter;
            }
            
               
          
            ViewModel.personaSeleccionada = miPersona;

            var backStack = Frame.BackStack;
            var backStackCount = backStack.Count;

            if (backStackCount > 0)
            {
                var masterPageEntry = backStack[backStackCount - 1];
                backStack.RemoveAt(backStackCount - 1);

                var modifiedEntry = new PageStackEntry(
                    masterPageEntry.SourcePageType,
                    miPersona,
                    masterPageEntry.NavigationTransitionInfo
                    );
                backStack.Add(modifiedEntry);
            }

            SystemNavigationManager systemNavigationManager = SystemNavigationManager.GetForCurrentView();
            systemNavigationManager.BackRequested += DetailPage_BackRequested;
            systemNavigationManager.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            SystemNavigationManager systemNavigationManager = SystemNavigationManager.GetForCurrentView();
            systemNavigationManager.BackRequested -= DetailPage_BackRequested;
            systemNavigationManager.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
        }


        private void OnBackRequested()
        {

            Frame.GoBack(new DrillInNavigationTransitionInfo());
        }
        private bool ShouldGoToWideState()
        {
            return Window.Current.Bounds.Width >= 720;
        }

        private void PageRoot_Loaded(object sender, RoutedEventArgs e)
        {
            if (ShouldGoToWideState())
            {
                NavigateBackForWideState(useTransition: true);
            }

            Window.Current.SizeChanged += Window_SizeChanged;
        }


        void NavigateBackForWideState(bool useTransition)
        {
            NavigationCacheMode = NavigationCacheMode.Disabled;

            if (useTransition)
            {
                Frame.GoBack(new EntranceNavigationTransitionInfo());
            }
            else
            {
                Frame.GoBack(new SuppressNavigationTransitionInfo());
            }
        }

        private void PageRoot_Unloaded(object sender, RoutedEventArgs e)
        {
            Window.Current.SizeChanged -= Window_SizeChanged;
        }

        private void Window_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            if (ShouldGoToWideState())
            {
                Window.Current.SizeChanged -= Window_SizeChanged;

                NavigateBackForWideState(useTransition: false);
            }
        }


        private void DetailPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            e.Handled = true;

            OnBackRequested();
        }
        #endregion
        #region METODO PARA GUARDAR
        /// <summary>
        /// Metodo que guarda los datos que el usuario ha cambiado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            this.txtNombre.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            this.txtApellidos.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            this.txtFechaNac.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            this.txtTelefono.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            this.txtDireccion.GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }
        #endregion
    }
}
