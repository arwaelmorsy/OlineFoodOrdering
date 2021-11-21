﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FoodAwayApii.Models
{
    public class Restaurant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RestaurantId { get; set; }
        [Required]
        [DisplayName("Name")]
        public string RestaurantName { get; set; }

        public byte[] Image { get; set; }

        [StringLength(5)]
        [Required]

        public string HotLine { get; set; }

        [Required]
        public string Description { get; set; }

        [DisplayName("Website")]
        public string WebSite { get; set; }
        [Required]
        public int StartWorkingHours { get; set; }
        [Required]
        public int EndWorkingHours { get; set; }

        public DateTime? Date { get; set; }

        [Required]




        public int MaxDeliveryTime { get; set; }
        [ForeignKey("Partener")]
        public int PartenerID { get; set; }
        public virtual Partener Partener { get; set; }
        [ForeignKey("Address")]
        public int AddressID { get; set; }
        public virtual Address Address { get; set; }

        public virtual List<RestaurantCustomer> MemberComments { get; set; }
        public virtual List<RestaurantCusine> restaurantCusines { get; set; }
        public virtual List<Cuisine> Cuisines { get; set; }
        public virtual List<Menu> Menus { get; set; }

    }
}