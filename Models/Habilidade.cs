using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LeagueOfLegends_API.Models
{
    public class Habilidade
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(60, ErrorMessage = "Este campo precisa ter entre 3 a 40 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo precisa ter entre 3 a 40 caracteres")]
        public string Nome { get; set; }

        [Required]
        public double Dano { get; set; }

        [Required]
        [MinLength(20, ErrorMessage = "Este campo precisa ter entre 20 a 400 caracteres")]
        public string Descricao { get; set; }

        [Required]
        public int TipoDano { get; set; } // 0 - Fisico | 1 - Mágico

        [Required]
        public double Cooldown { get; set; }
        public bool Status { get; set; }

        public Habilidade()
        {
            Status = true;
        }

        public Habilidade(string nome, double dano, int tipoDano, string descricao, double cooldown)
        {
            Nome = nome;
            Dano = dano;
            TipoDano = tipoDano;
            Descricao = descricao;
            Cooldown = cooldown;
            Status = true;
        }
    }
}