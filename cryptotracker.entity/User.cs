using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cryptotracker.entity
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public List<CryptoCurrency> Favorites { get; set; }
        public User()
        {
            Favorites = new List<CryptoCurrency>(); // Favorites listesi başlatılıyor
        }
    }
}