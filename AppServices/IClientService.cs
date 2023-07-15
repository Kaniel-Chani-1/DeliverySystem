using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppServices
{
    public interface IClientService
    {
        public List<ClientViewModel> GetList();
    }
}
