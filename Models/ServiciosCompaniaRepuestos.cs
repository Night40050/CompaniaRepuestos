using CompaniaRepuestos.Data;
using CompaniaRepuestos.Models;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaCompaniaRepuestos.Models;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;


namespace CitaFacil.Models
{
    public class ServiciosCompaniaRepuestos
    {
        private readonly CompaniaRepuestosContext _context;

        public ServiciosCompaniaRepuestos(CompaniaRepuestosContext context)
        {
            _context = context;
        }


        public  string CifrarContraseña(string contraseña)
        {
            using(SHA256 sha256Hash=SHA256.Create())
            {
                //Convertir la contraseña en un array de bytes y calcular el hash
                byte[] bytesContraseña=Encoding.UTF8.GetBytes(contraseña);
                byte[] hashContraseña=sha256Hash.ComputeHash(bytesContraseña);

                //convetir el hash en una cadena hexadecimal
                StringBuilder builder=new StringBuilder();
                for(int i=0;i<hashContraseña.Length;i++)
                {
                    builder.Append(hashContraseña[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        /// toma una contraseña sin cifrar como entrada y devuelve una cadena de texto cifrada utilizando el algoritmo de hash bcrypt.
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        /// toma una contraseña en texto claro y una contraseña cifrada, y verifica si la contraseña en texto claro coincide con la contraseña cifrada utilizando el algoritmo de verificación de bcrypt.
        public bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        //toma un correo y una contraseña en texto claro como entrada. Intenta recuperar un usuario de la base de datos
        //con el correo proporcionado y luego verifica si la contraseña coincide utilizando el método CifrarContraseña.
        //Si la contraseña coincide, devuelve el usuario; de lo contrario, devuelve null.
        public Usuario comprobarContraseña(string correo, string contraseña)
        {
            Usuario cliente = _context.Usuarios.Include(c => c.idRol).FirstOrDefault(c => c.Correo == correo);
            if (cliente != null && contraseña!=null)
            {
                contraseña=CifrarContraseña(contraseña);
                if (cliente.contraseña == CifrarContraseña(contraseña))
                {
                    return cliente;
                }
                else {                    
                    return null;
                }
            }
            else { 
            return null;
            }
        }


        //toma un correo y una contraseña en texto claro como entrada.
        //Intenta recuperar un usuario de la base de datos con el correo proporcionado y luego verifica si la contraseña coincide utilizando el método VerifyPassword
        public bool IsUserLogin(string correo, string contrasena)
        {
            bool isUserLogin = true;

            Usuario usuario = _context.Usuarios.FirstOrDefault(u => u.Correo == correo);

            if (usuario == null)
            {
                if (usuario != null && !VerifyPassword(contrasena, usuario.contraseña))
                {
                    throw new Exception("Correo o Contraseña Incorrectos");
                }

                isUserLogin = false;
            }
            else if(!VerifyPassword(contrasena,usuario.contraseña))
            {
                throw new Exception("Correo o Contraseña Incorrectos");
            }
            return isUserLogin;
        }
    }
}
