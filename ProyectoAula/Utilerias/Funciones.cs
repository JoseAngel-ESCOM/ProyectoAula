using System;

namespace ProyectoAula.Utilerias
{
    public class Funciones
    {
        public static string PasswordSeguro(int longitudContrasenia = 10)
        {
            Random rdn = new Random();
            string caracteres = "abcdefghijklmnopqrstuvxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890%&$#@";
            int longitud = caracteres.Length;
            char letra;
            string contraseniaAleatoria = string.Empty;
            for (int i = 0; i < longitudContrasenia; i++)
            {
                letra = caracteres[rdn.Next(longitud)];
                contraseniaAleatoria += letra.ToString();
            }
            return contraseniaAleatoria;
        }
    }
}
