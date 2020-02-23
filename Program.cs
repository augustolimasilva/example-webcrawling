using HtmlAgilityPack;
using System;
using ScrapySharp.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Bot.MegaSena
{
    class Program
    {
        static void Main(string[] args)
        {

            var url = "https://1xbet.mobi/en/line/Tennis/";
            var webGet = new HtmlWeb();
            webGet.UsingCache = false;
            webGet.UsingCacheIfExists = false;
            List<Bet> bets = new List<Bet>();

            if (webGet.Load(url) is HtmlDocument document)
            {
                var nodes = document.DocumentNode.CssSelect(".eventLink").ToList();
                var cotacoes = document.DocumentNode.CssSelect(".events__cell_withCoefs").ToList();

                foreach (var node in nodes)
                {
                    Bet bet = new Bet();

                    List<HtmlNode> jogadores = node.CssSelect(".events__team").ToList();

                    if(!(jogadores.Count == 0))
                    {
                        bet.Jogador1 = jogadores.First().InnerText;
                        bet.Jogador2 = jogadores.Last().InnerText;
                        bet.Tipo = "Vencedor do game";
                        bet.Id1xbet = Convert.ToInt32(node.GetAttributeValue("data-idgame"));
                        bet.Link1xBet = node.GetAttributeValue("data-href");

                        foreach (var cotacao in cotacoes)
                        {
                            List<HtmlNode> cotacoesDoJogo = cotacao.CssSelect(".js-coef").ToList();

                            foreach (var cotacaoDoJogo in cotacoesDoJogo)
                            {
                                int gameId = Convert.ToInt32(cotacaoDoJogo.GetAttributeValue("data-gameid"));

                                if (gameId == bet.Id1xbet)
                                {
                                    String betName = cotacaoDoJogo.GetAttributeValue("data-betname");

                                    String vlCotacao = cotacaoDoJogo.GetAttributeValue("data-coef").Replace(".", ",");

                                    double vlRealCotacao = Convert.ToDouble(vlCotacao);

                                    if (betName.Equals("1"))
                                    {
                                        bet.Jogador1_1xbet = vlRealCotacao;
                                    }

                                    if (betName.Equals("2"))
                                    {
                                        bet.Jogador2_1xbet = vlRealCotacao;
                                    }
                                }
                            }
                        }

                        bets.Add(bet);
                    }
                }
            }

            bets.ForEach(i => Console.WriteLine("Jogador 1: " + i.Jogador1 + " Jogador 2: " + i.Jogador2 + " Cotação 1: " + i.Jogador1_1xbet + " Cotação 2: " + i.Jogador2_1xbet + " Link: " + i.Link1xBet + "\n"));

            Console.ReadLine();
        }
    }
}
