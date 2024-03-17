using AllSports.Data;
using AllSports.Helpers;
using AllSports.Models;
using Microsoft.EntityFrameworkCore;

namespace AllSports.Repositories
{
    public class RepositoryUsuarios
    {
        private AllSportsContext context;

        public RepositoryUsuarios(AllSportsContext context)
        {
            this.context = context;
        }

        private async Task<int> GetMaxIdUsuarioAsync()
        {
            if (this.context.Usuarios.Count()==0)
            {
                return 1;
            }
            else
            {
                return await this.context.Usuarios.MaxAsync(z=> z.IdUsuario)+1;
            }
        }


        public async Task RegisterUser(string nombre, string apellidos, int nif, string email, string password,int idRole)
        {
            Usuario user = new Usuario
            {
                IdUsuario = await this.GetMaxIdUsuarioAsync(),
                Nombre = nombre,
                Apellidos = apellidos,
                Email = email,
                Nif = nif,
                //SACAMOS EL SALT
                Salt = HelperCryptography.GenerateSalt(),
            };
            //user.Password = HelperCryptography.EncryptPassword(password, user.Salt);
            user.Password=password;
            user.IdRolUsuario = idRole;
            this.context.Usuarios.Add(user);

            await this.context.SaveChangesAsync();
        }

        //public async Task<Usuario> LogInUserAsync(string email, string password)
        //{
        //    //var mail = email;
        //    //var pass = password;
        //    //try
        //    //{
        //        Usuario user = await this.context.Usuarios.FirstOrDefaultAsync(x=>x.Email == email);
        //        // Resto del código aquí
        //        if (user == null)
        //        {
        //            return null;
        //        }
        //        else
        //        {
        //            string salt = user.Salt;
        //            byte[] temp = HelperCryptography.EncryptPassword(password, salt);
        //            byte[] passUser = user.Password;
        //            bool response = HelperCryptography.CompareArrays(temp, passUser);

        //            if (response)
        //            {
        //                return user;
        //            }
        //            else
        //            {
        //                return null;
        //            }
        //        }
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    Console.WriteLine($"Error al ejecutar FirstOrDefaultAsync: {ex.Message}");
        //    //    throw; // Opcionalmente, puedes manejar la excepción de otra manera aquí
        //    //}
        //}

        public async Task<Usuario> LogInSeguridad(string email, string password)
        {
            Usuario usuario =await this.context.Usuarios.Where(z=>z.Email == email && z.Password == password).FirstOrDefaultAsync();
            return usuario;
        }
    }
}
