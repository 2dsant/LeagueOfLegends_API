using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LeagueOfLegends_API.Models.Enums;

namespace LeagueOfLegends_API.Models
{
    public class Personagem
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(60, ErrorMessage = "Este campo precisa ter entre 3 a 40 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo precisa ter entre 3 a 40 caracteres")]
        public string Nome { get; set; }

        [Required]
        [MaxLength(150, ErrorMessage = "Este campo precisa ter entre 20 a 150 caracteres")]
        [MinLength(20, ErrorMessage = "Este campo precisa ter entre 20 a 150 caracteres")]
        public string Descricao { get; set; }

        [Required]
        public Classe Classe { get; set; }

        [Required]
        public ICollection<Habilidade> Habilidades { get; set; } = new List<Habilidade>();
        public bool Status { get; set; }

        public Personagem()
        {
            Status = true;
        }

        public Personagem(string nome, string descricao, Classe classe, List<Habilidade> habilidades)
        {
            Nome = nome;
            Descricao = descricao;
            Classe = classe;
            Habilidades = habilidades;
            Status = true;
        }
    }
}