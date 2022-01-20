using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeagueOfLegends_API.Models;
using LeagueOfLegends_API.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace LeagueOfLegends_API.Data
{
    public class SeedingService
    {

        public static async Task Seed(ApplicationDbContext database)
        {
            await SeedAll(database);
        }

        public static async Task SeedAll(ApplicationDbContext database)
        {
            if (!database.Personagens.Any())
            {
                Usuario usuarioBd = new Usuario();
                usuarioBd.Email = "admin@gft.cmo";
                usuarioBd.Senha = "senhaforte";
                await database.Usuarios.AddAsync(usuarioBd);

                #region habilidades
                //Ahri - mago
                Habilidade h0 = new Habilidade("Orbe da Ilusão", 700, 1, "Ahri lança e puxa seu orbe causando dano na ida e na volta.", 6.0);
                Habilidade h1 = new Habilidade("Fogo de Raposa", 500, 1, "Ahri cria três fogos de fagoso ao seu redor que atingem os inimigos.", 5.0);
                Habilidade h2 = new Habilidade("Encanto", 300, 1, "Ahri lança um beijo na direção do inimigo deixando o encantado e inofensivo.", 10.0);

                //Nasus - tank
                Habilidade h3 = new Habilidade("Ataque Sifão", 100, 0, "Nasus fortalece seu próximo ataque, caso abata a unidade ele ganha +5 de ataque.", 6.0);
                Habilidade h4 = new Habilidade("Murchar", 100, 1, "Nasus lança uma maldição e o alvo sofre lentidão de 50%", 15.0);
                Habilidade h5 = new Habilidade("Fogo Espiritual", 300, 1, "Nasus cria um terreno de fogo espiritual causando 2% da vida máxima do alvo por segundo.", 6.0);

                //Sona - suporte
                Habilidade h6 = new Habilidade("Hino do Valor", 100, 1, "Sona lança duas notas a inimigos próximos, causando dano mágico", 3.0);
                Habilidade h7 = new Habilidade("Área da Perseverança", 0.0, 1, "Sona cura um aliado próximo e concede um escudo", 5.0);
                Habilidade h8 = new Habilidade("Canção da Celeridade", 0.0, 1, "Sona concede um aumento de velocidade a campeões aliados próximos", 5.0);

                //Xayah - atirador
                Habilidade h9 = new Habilidade("Punhais Duplos", 100, 100, "Lança duas penas afiadas em direção aos inimigos", 3.0);
                Habilidade h10 = new Habilidade("Plumagem Mortífera", 0.0, 0, "Ganha 20% de aumento de velocidade e dispara projéteis adicionais", 5.0);
                Habilidade h11 = new Habilidade("Invocadora das Lâminas", 300, 0, "Puxa todas as penas causando dano e enraizando os inimigos", 7.0);

                //Zed - assassino
                Habilidade h12 = new Habilidade("Shuriken Laminado", 500, 0, "Zed e suas sombras lançam uma shuriken em direção aos inimigos", 6.0);
                Habilidade h13 = new Habilidade("Sombra Viva", 0.0, 0, "Invoca uma sombra que copia suas habilidades e pode ser usada para zed se teletransportar", 11.0);
                Habilidade h14 = new Habilidade("Corte Sombrio", 600, 0, "Gira e corta inimigos ao redor com suas lâminas", 7.0);

                List<Habilidade> habilidades = new List<Habilidade>();
                habilidades.Add(h0);
                habilidades.Add(h1);
                habilidades.Add(h2);
                habilidades.Add(h3);
                habilidades.Add(h4);
                habilidades.Add(h5);
                habilidades.Add(h6);
                habilidades.Add(h7);
                habilidades.Add(h8);
                habilidades.Add(h9);
                habilidades.Add(h10);
                habilidades.Add(h11);
                habilidades.Add(h12);
                habilidades.Add(h13);
                habilidades.Add(h14);

                #endregion

                List<Habilidade> ahriHabilidades = new List<Habilidade>();
                ahriHabilidades.Add(h0);
                ahriHabilidades.Add(h1);
                ahriHabilidades.Add(h2);

                List<Habilidade> nasusHabilidades = new List<Habilidade>();
                nasusHabilidades.Add(h3);
                nasusHabilidades.Add(h4);
                nasusHabilidades.Add(h5);

                List<Habilidade> sonaHabilidades = new List<Habilidade>();
                sonaHabilidades.Add(h6);
                sonaHabilidades.Add(h7);
                sonaHabilidades.Add(h8);

                List<Habilidade> xayahHabilidades = new List<Habilidade>();
                xayahHabilidades.Add(h9);
                xayahHabilidades.Add(h10);
                xayahHabilidades.Add(h11);

                List<Habilidade> zedHabilidades = new List<Habilidade>();
                zedHabilidades.Add(h12);
                zedHabilidades.Add(h13);
                zedHabilidades.Add(h14);

                Personagem mago = new Personagem("Ahri", "Uma vastaya, metade raposa e metade humana.", Classe.Mago, ahriHabilidades);
                Personagem tank = new Personagem("Nasus", "Um antigo Deus Shuriname com enormes poderes.", Classe.Tank, nasusHabilidades);
                Personagem suporte = new Personagem("Sona", "Uma musicista com habilidades especiais.", Classe.Suporte, sonaHabilidades);
                Personagem atirador = new Personagem("Xayah", "Uma vastaya rebelde com grande dominio de lâminas em formato de penas.", Classe.Atirador, xayahHabilidades);
                Personagem assassino = new Personagem("Zed", "Um assassino das sombras, hábil e silencioso.", Classe.Assassino, zedHabilidades);

                List<Personagem> personagens = new List<Personagem>();
                personagens.Add(mago);
                personagens.Add(tank);
                personagens.Add(suporte);
                personagens.Add(atirador);
                personagens.Add(assassino);

                foreach (var personagem in personagens)
                {
                    await database.AddAsync(personagem);
                }

                await database.SaveChangesAsync();
            }
        }
    }
}