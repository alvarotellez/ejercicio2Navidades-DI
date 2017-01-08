using ejercicio2.Models;
using ejercicio2.ViewModels;
using ejercicio2.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ejercicio2
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.viewModel = (clsMainPageVM)this.DataContext;
        }

       
        public clsMainPageVM viewModel { get;}
        
        #region METODOS REFERENTES AL ADAPTIVE TRIGGER
        private void AdaptativeStates_CurrentStateChanged(object sender, VisualStateChangedEventArgs e)
        {
            UpdateForVisualState(e.NewState, e.OldState);
        }

        private void UpdateForVisualState(VisualState newState, VisualState oldState)
        {
            var isNarrow = newState == movil;

            clsPersona p = viewModel.personaSeleccionada;

            if (isNarrow && oldState == monitor && p != null)
            {
                Frame.Navigate(typeof(Detail), p, new SuppressNavigationTransitionInfo());
            }

            EntranceNavigationTransitionInfo.SetIsTargetElement(listaPersonas, isNarrow);
        }

        private void seleccionarPersona(object sender, ItemClickEventArgs e)
        {
            var itemSeleccionado = (clsPersona)e.ClickedItem;


            if(AdaptativeStates.CurrentState == movil)
            {
                Frame.Navigate(typeof(Detail), itemSeleccionado, new DrillInNavigationTransitionInfo());
            }
        }

        #endregion
        #region METODO PARA GUARDAR
        /// <summary>
        /// Metodo que guarda los datos que el usuario ha cambiado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guardar(object sender, RoutedEventArgs e)
        {
            this.txtNombre.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            this.txtApellidos.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            this.txtFechaNac.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            this.txtTelefono.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            this.txtDireccion.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            listaPersonas.GetBindingExpression(ListView.ItemsSourceProperty).UpdateSource();
        }
        #endregion
    }
}
