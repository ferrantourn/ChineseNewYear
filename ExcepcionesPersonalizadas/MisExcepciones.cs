using System;
using System.Runtime.Serialization;

namespace ExcepcionesPersonalizadas
{
    //[Serializable] DESCOMENTAR CUANDO SE USE WEB SERVICE
    public class MisExcepciones : Exception
    {

    }


    //[Serializable] DESCOMENTAR CUANDO SE USE WEB SERVICE
    public class ErrorGeneral : Exception
    {
        public ErrorGeneral()
        {

        }
        public ErrorGeneral(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }

        private const string Mensaje = "Hubo un error general, vuelva a intentarlo más tarde";

        public override string Message
        {
            get { return Mensaje; }
        }

    }

    //[Serializable] DESCOMENTAR CUANDO SE USE WEB SERVICE
    public class ErrorBaseDeDatos : Exception
    {
        public ErrorBaseDeDatos()
        {

        }

        public ErrorBaseDeDatos(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }

        private const string Mensaje = "Hubo un error en la base de datos, vuelva a intentarlo más tarde";

        public override string Message
        {
            get { return Mensaje; }
        }

    }

    [Serializable]
    public class ErrorDocumentoInvalido : Exception
    {
        public ErrorDocumentoInvalido()
        {

        }

        public ErrorDocumentoInvalido(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }

        private const string Mensaje = "Escriba la CI sin puntos ni guiones";

        public override string Message
        {
            get { return Mensaje; }
        }

    }

    [Serializable]
    public class ErrorUsuarioYaExiste : Exception
    {
        public ErrorUsuarioYaExiste()
        {

        }

        public ErrorUsuarioYaExiste(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }

        private const string Mensaje = "ERROR: Usuario ya existe en el sistema";

        public override string Message
        {
            get { return Mensaje; }
        }

    }

    [Serializable]
    public class ErrorUsuarioNoExiste : Exception
    {
        public ErrorUsuarioNoExiste()
        {

        }

        public ErrorUsuarioNoExiste(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }

        private const string Mensaje = "ERROR: Usuario no existe en el sistema";

        public override string Message
        {
            get { return Mensaje; }
        }

    }

    [Serializable]
    public class ErrorNoHayUsuarios : Exception
    {
        public ErrorNoHayUsuarios()
        {

        }

        public ErrorNoHayUsuarios(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }

        private const string Mensaje = "ERROR: No hay usuarios registrados en el sistema";

        public override string Message
        {
            get { return Mensaje; }
        }

    }

    
}

