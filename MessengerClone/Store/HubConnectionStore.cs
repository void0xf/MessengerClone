using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessengerClone.Store
{
    public class HubConnectionStore
    {
        private static HubConnectionStore _instance;
        private static readonly object _lock = new object();
        private HubConnection _connection;

        private HubConnectionStore()
        {
            _connection = new HubConnectionBuilder().WithUrl("http://localhost:5000/chat").Build();
        }

        public static HubConnectionStore Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new HubConnectionStore();
                    }
                    return _instance;
                }
            }
        }

        public HubConnection Connection => _connection;
    }
}
