namespace CategoriaApi.Exceptions
{
using System;
    public class NullException : Exception
    {
        private const string menssagemPadrao = "O Objeto não existe";
        public NullException() : base(menssagemPadrao)
        {

        }

        public NullException(string menssagemPadrao) : base(menssagemPadrao)
        {
        }

        public NullException(Exception innerException) : base(menssagemPadrao, innerException)
        {

        }
    }
}
