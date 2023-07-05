using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cryptotracker.entity;
namespace cryptotracker.web.ViewModals
{
    public class CryptoCurrencyDataViewModal
    {
        public List<CryptoCurrency> CryptoCurrencies {get; set;}
        public int DataPerPage { get; set; }  
        public int CurrentPage { get; set; } 
        public int PageCount()  
        {  
            return Convert.ToInt32(Math.Ceiling(CryptoCurrencies.Count() / (double)DataPerPage));  
        }
        public List<CryptoCurrency> PaginatedBlogs()  
        {  
            int start = (CurrentPage - 1) * DataPerPage;
            return CryptoCurrencies.Skip(start).Take(DataPerPage).ToList();  
        }
    }
}