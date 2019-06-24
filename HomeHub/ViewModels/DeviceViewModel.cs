using HomeHub.CustomAttributes;
using HomeHub.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomeHub.ViewModels
{
    [NotMapped]
    public class DeviceViewModel
    {
        [Key]
        [ScaffoldColumn(false)]
        public int DeviceId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Device Name is required"), MaxLength(50, ErrorMessage = "Device name must be within 50 characters")]
        [Remote(action: "VerifyDeviceName", controller: "Devices", AdditionalFields = "DeviceId")]
        [DisplayName("Device Name")]

        public string Name { get; set; }
        [Required(AllowEmptyStrings =false, ErrorMessage = "MAC Address is required"), MaxLength(17, ErrorMessage = "The value is too long")]
        [DisplayName("MAC Address")]
        [MACAddress]
        public string MACAddress { get; set; }
        [EnumDataType(typeof(Location))]
        [DisplayName("Location")]
        public Location location { get; set; }
        [StringLength(1000, ErrorMessage = "Description must be less than a 1000 characters")]
        public string Description { get; set; }
        [ScaffoldColumn(false)]
        public string UserID { get; set; }
    }
}
