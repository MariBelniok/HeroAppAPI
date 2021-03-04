using System.Collections.Generic;
using System;

namespace WebAPI.Dominio
{
    public class Batalha
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DeInicio { get; set; }
        public DateTime DtFim { get; set; }
        public List<HeroiBatalha> HeroisBatalhas { get; set; }
    }
}
