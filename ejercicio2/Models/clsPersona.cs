using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ejercicio2.Models
{
    public class clsPersona
    {
        #region Atributos
        private int _id;
        private String _nombre;
        private String _apellidos;
        private DateTime _fechaNac;
        private String _direccion;
        private String _telefono;
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
        #region Constructores 
        public clsPersona()
        {
            _id = 0;
            _nombre = "";
            _apellidos = "";
            _fechaNac = DateTime.Now;
            _direccion = "";
            _telefono = "";
        }

        public clsPersona(int id, String nombre, String apellidos, DateTime fechaNac, String direccion, String telefono)
        {
            this._id = id;
            this._nombre = nombre;
            this._apellidos = apellidos;
            this._fechaNac = fechaNac;
            this._direccion = direccion;
            this._telefono = telefono;
        }
        #endregion
        #region Get & Set
        public int Id
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
                OnPropertyChanged("Id");
            }
        }

        public String Nombre
        {
            get
            {
                return this._nombre;
            }
            set
            {
                this._nombre = value;
                OnPropertyChanged("Nombre");
            }
        }
        public String Apellidos
        {
            get
            {
                return this._apellidos;
            }
            set
            {
                this._apellidos = value;
                OnPropertyChanged("Apellidos");
            }
        }

        public DateTime FechaNac
        {
            get
            {
                return this._fechaNac;
            }
            set
            {
                this._fechaNac = value;
                OnPropertyChanged("FechaNac");
            }
        }

        public String Direccion
        {
            get
            {
                return this._direccion;
            }
            set
            {
                this._direccion = value;
                OnPropertyChanged("Direccion");
            }
        }

        public String Telefono
        {
            get
            {
                return this._telefono;
            }
            set
            {
                this._telefono = value;
                OnPropertyChanged("Telefono");
            }
        }
        #endregion
        #region OnProperyChanged
        /// <summary>
        /// Realiza la notificacion de que ha cambiado algo en el constructor
        /// </summary>
        /// <param name="name">Le pasamos un String dependiendo de a que atributo le hacemos el metodo set</param>
        private void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if(handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion
    }
}
