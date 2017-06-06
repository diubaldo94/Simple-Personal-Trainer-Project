using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace WpfApplication1
{
    public class Appuntamento
    {
        public int ID { get; set; }
        public DateTime dataI { get; set; }
        public DateTime dataF { get; set; }
        public int clienteID { get; set; }
        
        public Appuntamento(DateTime dataI, DateTime dataF, int clienteID)
        {
            this.dataI = dataI;
            this.dataF = dataF;
            this.clienteID = clienteID;
        }

        public bool compatibile(DateTime orarioIniziale, DateTime orarioFinale)
        {
            if (this.dataI.Year == orarioIniziale.Year && this.dataI.Month == orarioIniziale.Month && this.dataI.Day == orarioIniziale.Day)
            {
                if ((orarioIniziale < this.dataI && orarioFinale <= this.dataI)||(orarioIniziale >= this.dataF && orarioFinale > this.dataF))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

    }
}
