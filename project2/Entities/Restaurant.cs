using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace project2.Entities
{
    public class Restaurant
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Seats { get; set; }
        public int LocationId { get; set; }

        [ForeignKey("LocationId")]
        public City Location { get; set; }



    }
}