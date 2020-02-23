using System;
using System.Collections.Generic;
using System.Text;

namespace Bot.MegaSena
{
    class Bet
    {
        public String Jogador1 { get; set; }
        public String Jogador2 { get; set; }
        public String Tipo { get; set; }
        public int Id1xbet { get; set; }
        public int IdBet365 { get; set; }
        public Double Jogador1_1xbet { get; set; }
        public Double Jogador1_bet365 { get; set; }
        public Double Jogador2_1xbet { get; set; }
        public Double Jogador2_bet365 { get; set; } 
        public Double Probabilidade { get; set; }
        public String LinkBet365 { get; set; }
        public String Link1xBet { get; set; }
    }
}
