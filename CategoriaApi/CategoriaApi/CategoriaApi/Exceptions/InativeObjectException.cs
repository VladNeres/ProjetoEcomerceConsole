namespace CategoriaApi.Exceptions
{
using System;
    public class InativeObjectException : Exception
    {
        private const string MenssagemPadrao = "O objeto esta inativo por favor indique um objeto ativo";

        public InativeObjectException() : base(MenssagemPadrao)
        {

        }
        public InativeObjectException(string Menssegem) : base(Menssegem)
        {

        }
        public InativeObjectException(Exception exception) : base(MenssagemPadrao, exception)
        {

        }
    }
}
