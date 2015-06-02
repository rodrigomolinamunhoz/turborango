namespace TurboRango.Dominio
{
    internal class Restaurante
    {
        /// <summary>
        /// Capacidade (lotação máxima) do restaurante.
        /// </summary>
        internal int Capacidade { get; set; }
        internal string Nome { get; set; }
        internal Contato Contato { get; set; }
        internal Localizacao Localizacao { get; set; }
        internal Categoria Categoria { get; set; }

        /*
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        internal string Imprimir(int x)
        {
            return null;
        }
        */
    }
}
