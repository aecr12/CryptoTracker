using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cryptotracker.entity;
using cryptotracker.dataaccess.Abstract;
using Microsoft.EntityFrameworkCore;

namespace cryptotracker.dataaccess.Concrete.EF
{
    public class EFUserRepo : EFGenericRepo<User, CTrackerContext>, IUserRepo
    {
        public void AddFavorites(string userId, CryptoCurrency cryptoCurrency)
        {
            using(var context = new CTrackerContext())
            {
                var user = context.Users.FirstOrDefault(u=>u.UserId==userId);
                user.Favorites.Add(cryptoCurrency);
                context.Entry(user).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}