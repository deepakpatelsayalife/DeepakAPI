using System;
using System.Collections.Generic;

namespace DeepakAPI.Model.Models
{
    public partial class State
    {
        public State()
        {
            City = new HashSet<City>();
        }

        public int Id { get; set; }
        public string StateName { get; set; }
        public int CountryId { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<City> City { get; set; }
    }
}
