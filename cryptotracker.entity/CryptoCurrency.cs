using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cryptotracker.entity
{
    public class CryptoCurrency
    {
        public string Id { get; set; }
        public string? UserId {get; set;}
        public User User { get; set; }
        public string Rank { get; set; }
        public string Symbol { get; set; }
        public string Name {get; set;}
        public string Supply { get; set; }  
        public string Volume24Hr {get; set;}
        public string Price {get; set;}
        public string ChangePercentFor24Hr {get; set;}
    }
}