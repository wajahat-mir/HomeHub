using AutoMapper;
using HomeHub.Models;
using HomeHub.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeHub.Profiles
{
    public class DeviceProfile : Profile
    {
        public DeviceProfile()
        {
            CreateMap<Device, DeviceViewModel>();
            CreateMap<DeviceViewModel, Device>();
        }
    }
}
