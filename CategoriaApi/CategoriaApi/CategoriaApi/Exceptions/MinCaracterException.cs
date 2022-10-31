namespace CategoriaApi.Exceptions
{
    using System;
    public class MinCaracterException : Exception
    {
        private const string MenssagemPadrao = "Minimo de caracteres não atingido";
        public MinCaracterException() : base(MenssagemPadrao)
        {

        }
        public MinCaracterException(string mensagem) : base(mensagem)
        {

        }

        public MinCaracterException(Exception exception) : base(MenssagemPadrao, exception)
        {

        }
    }
}
