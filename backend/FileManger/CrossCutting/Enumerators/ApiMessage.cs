using System;
using System.Collections.Generic;
using System.Text;

namespace CrossCutting.Enumerators
{
    public static class ApiMessage
    {
        public const string DELETE = "Borrado correctamente";
        public const string SUCCESFULLY = "Creado correctamente";
        public const string UPDATED = "Actualizado correctamente";
        public const string INTERNAL_ERROR = "Internal server error";
        public const string FILE = "File";
        public const string FOLDER = "Folder";
        public const string OK = "OK";
        public const string INVALID_LOGIN = "Login inválido";
        public const string LOGOUT = "Sesión cerrada";
        public const string FORGOTPASSWORD = "Clave actualizada correctamente";
        public const string INVALID_FORGOTPASSWORD = "no se pudo cambiar la clave";
        public const string EMAIL_MESSAGE = "Archivos configurados";
    }
}
