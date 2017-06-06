using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    class Repository : IRepository
    {
        string _connectionString;

        public Repository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<Appuntamento> getAllAppuntamenti()
        {
            SqlConnection cnn = new SqlConnection(_connectionString);
            var lista = cnn.Query<dynamic>("Select * from APPUNTAMENTI");
            var listaReturn = new List<Appuntamento>();
            foreach (dynamic item in lista)
            {
                var di = new DateTime(item.dataInizio.Year, item.dataInizio.Month, item.dataInizio.Day, item.dataInizio.Hour, item.dataInizio.Minute, 0);
                var df = new DateTime(item.dataFine.Year, item.dataFine.Month, item.dataFine.Day, item.dataFine.Hour, item.dataFine.Minute, 0);
                listaReturn.Add(new Appuntamento(di, df, item.cliente_id));
            }
            return listaReturn;
        }

        public int storeAppuntamento(Appuntamento app)
        {
            SqlConnection cnn = new SqlConnection(_connectionString);
            var query=cnn.Query<int>(@"insert into appuntamenti (dataInizio, dataFine, cliente_id) VALUES (@dataI, @dataF, @clienteID) select @@identity", new { dataI = app.dataI, dataF = app.dataF, clienteID = app.clienteID });
            return query.First();
        }

        public int storeCliente(Cliente cliente)
        {
            SqlConnection cnn = new SqlConnection(_connectionString);
            var query = cnn.Query<int>(@"insert into clienti (nome, cognome, email) VALUES (@nome, @cognome, @email) select @@identity", new { nome = cliente.nome, cognome=cliente.cognome, email=cliente.email });
            return query.First();
        }
    }
}
