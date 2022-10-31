namespace CategoriaApi.Exceptions
{
using System;
    public class AlreadyExistsExceprion : Exception
    {
        private const string menssagemPadrao = "O objeto ja existe";
        public AlreadyExistsExceprion() : base(menssagemPadrao)
        {

        }
        public AlreadyExistsExceprion(string menssagem) : base(menssagem)
        {

        }

        public AlreadyExistsExceprion(Exception innerException) : base(menssagemPadrao, innerException)
        {

        }
    }
}
