using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Nager.Date;
using System.Data.SqlClient;

namespace WpfApplication1
{
    public class Operations
    {
        Repository _repository;

        public Operations()
        {
            _repository = new Repository(@"Data Source=.\SQLExpress;Initial Catalog=Personal;User=;Password=");
        }
        

        public bool storeSerie(DateTime primogiorno, int settimane, List<DayOfWeek> listaGiorni, DateTime orarioI, DateTime orarioF, string nome, string cognome, string email)
        {
            Cliente cliente = new Cliente(nome, cognome, email);
            int clienteID=_repository.storeCliente(cliente);
            var appuntamenti = _repository.getAllAppuntamenti();
            DateTime gg = primogiorno;
            int numapp = listaGiorni.Count * settimane;
            int cont = 0;
            while(cont<numapp)
            {
                if (listaGiorni.Contains(gg.DayOfWeek)) 
                    if (!appuntamenti.Any(i => !i.compatibile(new DateTime(gg.Year, gg.Month, gg.Day, orarioI.Hour, orarioI.Minute, 0), new DateTime(gg.Year, gg.Month, gg.Day, orarioF.Hour, orarioF.Minute, 0))))
                        if (!DateSystem.IsPublicHoliday(gg, CountryCode.IT))
                        {
                            cont++;
                            Appuntamento app = new Appuntamento(new DateTime(gg.Year, gg.Month, gg.Day, orarioI.Hour, orarioI.Minute, 0), new DateTime(gg.Year, gg.Month, gg.Day, orarioF.Hour, orarioF.Minute, 0), clienteID);
                            _repository.storeAppuntamento(app);
                        }
                gg=gg.AddDays(1);
            }
            return true;
        }

        public bool storeAppuntamento(DateTime dataI, DateTime dataF, string nome, string cognome, string email)
        {
            Cliente cliente = new Cliente(nome, cognome, email);
            int clienteID=_repository.storeCliente(cliente);
            var appuntamenti = _repository.getAllAppuntamenti();
            DateTime gg = dataI;
            bool cont = true;
            while (cont)
            {
                if (!appuntamenti.Any(i => !i.compatibile(new DateTime(gg.Year, gg.Month, gg.Day, dataI.Hour, dataI.Minute, 0), new DateTime(gg.Year, gg.Month, gg.Day, dataF.Hour, dataF.Minute, 0))))
                        if (!DateSystem.IsPublicHoliday(gg, CountryCode.IT))
                        {
                            cont = false;
                            Appuntamento app = new Appuntamento(new DateTime(gg.Year, gg.Month, gg.Day, dataI.Hour, dataI.Minute, 0), new DateTime(gg.Year, gg.Month, gg.Day, dataF.Hour, dataF.Minute, 0), clienteID);
                            _repository.storeAppuntamento(app);
                    }
                gg.AddDays(1);
            }

            return true;
        }
        

    }
}
