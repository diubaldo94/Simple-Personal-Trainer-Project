using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    interface IRepository
    {
        List<Appuntamento> getAllAppuntamenti();
        int storeAppuntamento(Appuntamento app);
        int storeCliente(Cliente cliente);
    }
}
