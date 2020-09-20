using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Registro_de_Usuario.Models
{
    public class UsersDatos
    {
        UsersDataDataContext user = new UsersDataDataContext();

        public List<UsersDatosModel> usersDatos()
        {
            List<UsersDatosModel> List = new List<UsersDatosModel>();
            var query = from u in user.Users select u;
            var listData = query.ToList();
            foreach(var Data in listData)
            {
                List.Add(new UsersDatosModel()
                {
                    Id = Data.IdUser,
                    Name = Data.Name,
                    LastName = Data.LastName,
                    User = Data.UserName,
                    Email = Data.Email,
                    Password = Data.Password
                });
            }
            return List;
        }
        public UsersDatosModel editDatos(int id)
        {
            UsersDatosModel datos = user.Users.Where(x => x.IdUser == id).Select(x =>
            new UsersDatosModel()
            {
                Id = x.IdUser,
                Name = x.Name,
                LastName = x.LastName,
                User = x.UserName,
                Email = x.Email,
                Password = x.Password
            }).SingleOrDefault();
            return datos;
        }
        public bool actualizar(UsersDatosModel model)
        {
            Users u = user.Users.Where(x => x.IdUser == model.Id).Single<Users>();
            u.Name = model.Name;
            u.LastName = model.LastName;
            u.UserName = model.User;
            u.Email = model.Email;
            u.Password = model.Password;
            user.SubmitChanges();
            return true;
        }
        public bool Eliminar(UsersDatosModel model)
        {
            Users u = user.Users.Where(x => x.IdUser == model.Id).Single<Users>();
            user.Users.DeleteOnSubmit(u);
            user.SubmitChanges();
            return true;
        }
    }
    public class UsersDatosModel
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String LastName { get; set; }
        public String User { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
    }
}