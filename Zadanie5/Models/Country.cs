using System;
using System.Collections.Generic;

namespace Zadanie5.Models
{
    public partial class Country
    {
        public Country()
        {
            Addresses = new HashSet<Address>();
        }

        public long CountryId { get; set; }
        public string? CountryName { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
    }
}
