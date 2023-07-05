using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cryptotracker.entity;

namespace cryptotracker.dataaccess.Abstract
{
    public interface IUserRepo:IRepo<User>
    {
        void AddFavorites(string userId, CryptoCurrency cryptoCurrency);
    }
}