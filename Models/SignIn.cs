using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Registro_de_Usuario.Models
{
    public class SignIn
    {
        [Required(ErrorMessage = "<font color='red'>El nombre es requerido</font>")]
        [Display(Name = "Nombre")]
        public String Name { get; set; }

        [Required(ErrorMessage = "<font color='red'>El apellido es requerido</font>")]
        [Display(Name = "Apellido")]
        public String LastNAme { get; set; }

        [Required(ErrorMessage = "<font color='red'>El usuario es requerido</font>")]
        [Display(Name = "Usuario")]
        public String User { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "<font color='red'>El email es requerido</font>")]
        [Display(Name = "Correo electronico")]
        public String Email { get; set; }

        [Required(ErrorMessage = "<font color='red'>La contraseña es requerido</font>")]
        [Display(Name = "Contraseña")]
        public String Password { get; set; }

        UsersDataDataContext user = new UsersDataDataContext();
        Users obj = new Users();
        public bool signIn()
        {
            var query = from u in user.Users
                        where u.Email == Email ||
                        u.UserName == User
                        select u;
            if (query.Count() > 0)
            {
                return false;
            }
            else
            {
                obj.Name = Name;
                obj.LastName = LastNAme;
                obj.UserName = User;
                obj.Email = Email;
                obj.Password = Password;

                user.Users.InsertOnSubmit(obj);
                user.SubmitChanges();
                return true;
            }
        }
    }
}