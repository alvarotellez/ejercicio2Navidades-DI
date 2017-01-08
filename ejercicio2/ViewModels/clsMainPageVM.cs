using ejercicio2.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ejercicio2.ViewModels
{
    public class clsMainPageVM : clsVMBase
    {
        private clsPersona _personaSeleccionada;
        private ObservableCollection<clsPersona> _listado;
        private DelegateCommand _eliminarCommand;
        private DelegateCommand _guardarCommand;
        private DelegateCommand _crearCommand;

        public clsMainPageVM()
        {

            rellenarLista();
            _eliminarCommand = new DelegateCommand(EliminarCommand_Executed, EliminarCommand_CanExecute);
            _guardarCommand = new DelegateCommand(GuardarCommand_Executed, GuardarCommand_CanExecute);
            _crearCommand = new DelegateCommand(CrearCommand_Executed);
        }
        #region RELLENAR EL LISTADO OBTENIENDOLO DE AZURE
        /// <summary>
        /// Metodo que rellena la lista obteniendo los datos de Azure
        /// </summary>
        private async void rellenarLista()
        {
            clsListado lista = new clsListado();
            listado = await lista.getListado();
            NotifyPropertyChanged("listado");
        }
        #endregion
        #region SELECCION DE LA PERSONA
        /// <summary>
        /// Metodo que se encarga de saber que persona se a seleccionado
        /// </summary>
        public clsPersona personaSeleccionada
        {
            get
            {
                return _personaSeleccionada;
            }
            set
            {
                _personaSeleccionada = value;
                _eliminarCommand.RaiseCanExecuteChanged();
                _guardarCommand.RaiseCanExecuteChanged();
                NotifyPropertyChanged("personaSeleccionada");
            }
        }
        #endregion
        #region LISTADO
        /// <summary>
        /// Generacion del listado de personas
        /// </summary>
        public ObservableCollection<clsPersona> listado
        {
            get
            {
                return _listado;
            }
            set
            {
                _listado = value;
                NotifyPropertyChanged("listado");
            }
        }
        #endregion
        #region ACCIONES PARA ELIMINAR
        /// <summary>
        /// Que hace el btn eliminar cuando el usuario hace click
        /// </summary>
        public void btnEliminar_Click()
        {
            listado.Remove(personaSeleccionada);
        }

        public DelegateCommand eliminarCommand
        {
            get
            {
                return _eliminarCommand;
            }
        }
        /// <summary>
        /// Una vez el usuario ha pulsado eliminar se nos muestra una ventana emergente donde el usuario tiene que asegurarse de que realmente quiere eliminar a la persona
        /// </summary>
        private async void EliminarCommand_Executed()
        {
            ContentDialog dialogo = new ContentDialog();
            dialogo.Title = "Atención";
            dialogo.Content = "¿Desea eliminar la siguiente persona?";
            dialogo.PrimaryButtonText = "No";
            dialogo.SecondaryButtonText = "Si";

            ContentDialogResult resultado = await dialogo.ShowAsync();

            if (resultado == ContentDialogResult.Secondary)
            {

                ManejadoraPersona mp = new ManejadoraPersona();

                try
                {
                    mp.BorrarPersona(personaSeleccionada.Id);
                }
                catch (Exception)
                {

                }
                this.rellenarLista();

                if (Window.Current.Bounds.Width <= 720)
                {
                    ((Frame)Window.Current.Content).GoBack();
                }
            }
        }

        /// <summary>
        /// Metodo que comprueba si el usuario puede realizar la accion de borrado
        /// </summary>
        /// <returns></returns>
        private bool EliminarCommand_CanExecute()
        {
            bool puedeBorrar = false;
            if (personaSeleccionada != null)
                puedeBorrar = true;
            return puedeBorrar;
        }
        #endregion UNA PERSONA
        #region ACCIONES PARA GUARDAR PERSONA NUEVA
        public DelegateCommand guardarCommand
        {
            get
            {
                return _guardarCommand;
            }
        }

        private void GuardarCommand_Executed()
        {
            ManejadoraPersona mp = new ManejadoraPersona();
            if (personaSeleccionada.Id == 0)
            {
                mp.GuardarPersona(personaSeleccionada);
            }
            else
            {
                mp.ActualizarPersona(personaSeleccionada);
            }

            personaSeleccionada = null;

            this.rellenarLista();

            if (Window.Current.Bounds.Width <= 720)
            {
                ((Frame)Window.Current.Content).GoBack();
            }
        }

        private bool GuardarCommand_CanExecute()
        {
            bool puedeBorrar = false;
            if (personaSeleccionada != null)
                puedeBorrar = true;
            return puedeBorrar;
        }
        #endregion
        #region ACCIONES PARA CREAR UNA NUEVA PERSONA
        public DelegateCommand crearCommand
        {
            get
            {
                return _crearCommand;
            }
        }

        /// <summary>
        /// Crea una nueva persona
        /// </summary>
        private void CrearCommand_Executed()
        {
            personaSeleccionada = new clsPersona();
            NotifyPropertyChanged("personaSeleccionada");
        }
        #endregion
    }

}
