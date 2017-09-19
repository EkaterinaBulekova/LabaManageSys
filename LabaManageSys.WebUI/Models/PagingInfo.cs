using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabaManageSys.WebUI.Models
{
    public class PagingInfo
    {
        // Кол-во общее
        public int TotalItems { get; set; }

        // Кол-во на одной странице
        public int ItemsPerPage { get; set; }

        // Номер текущей страницы
        public int CurrentPage { get; set; }

        // Общее кол-во страниц
        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)this.TotalItems / this.ItemsPerPage); }
        }
    }
}