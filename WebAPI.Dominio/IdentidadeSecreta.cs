using System;

namespace WebAPI.Dominio
{
    public class IdentidadeSecreta
    {
        public int Id { get; set; }
        public int NomeReal { get; set; }
        public int HeroiId { get; set; }
        public Heroi Heroi { get; set; }
    }
}
