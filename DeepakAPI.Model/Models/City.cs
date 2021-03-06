using System;
using System.Collections.Generic;

namespace DeepakAPI.Model.Models
{
    public partial class City
    {
        public int Id { get; set; }
        public string CityName { get; set; }
        public int ZipCode { get; set; }
        public int StateId { get; set; }

        public virtual State State { get; set; }
    }
}
