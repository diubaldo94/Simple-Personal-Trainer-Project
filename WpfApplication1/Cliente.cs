using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    public class Cliente
    {
        public int ID { get; set; }
        public string nome { get; set; }
        public string cognome { get; set; }
        public string email { get; set; }

        public Cliente(string nome, string cognome, string email)
        {
            this.nome = nome;
            this.cognome = cognome;
            this.email = email;
        }
        
    }
}
