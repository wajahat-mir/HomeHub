using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomeHub.Models
{
    public class Device
    {
        public int DeviceId { get; set; }
        public string Name { get; set; }
        public string MACAddress { get; set; }
        public Location location { get; set; }
        public string Description { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserID { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }

    public enum Location
    {
        [Display(Name = "Master Bedroom")]
        MasterBedroom,
        [Display(Name = "Bedroom")]
        Bedroom,
        [Display(Name = "Living Bedroom")]
        LivingRoom,
        [Display(Name = "Garage")]
        Garage,
        [Display(Name = "Dining Bedroom")]
        DiningRoom,
        [Display(Name = "Kitchen")]
        Kitchen,
        [Display(Name = "Driveway")]
        Driveway,
        [Display(Name = "Backyard")]
        Backyard,
        [Display(Name = "Front Lawn")]
        FrontLawn     
    }
}
