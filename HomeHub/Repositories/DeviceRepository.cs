using HomeHub.Data;
using HomeHub.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeHub.Repositories
{
    public interface IDeviceRepository
    {
        Task<IEnumerable<Device>> GetAllDevices(string UserId);
        Task<Device> GetDevice(int id);
        Task CreateDevice(Device device);
        Task UpdateDevice(Device device);
        Task RemoveDevice(int id);
        bool FindDuplicateName(string name, int id);
    }

    public class DeviceRepository : IDeviceRepository
    {
        private readonly ApplicationDbContext _db;

        public DeviceRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Device>> GetAllDevices(string UserId)
        {
            var devices = await _db.Devices.Where(d => d.UserID == UserId).ToListAsync();

            return devices;
        }

        public async Task<Device> GetDevice(int id)
        {
            return await _db.Devices.FindAsync(id);
        }

        public async Task CreateDevice(Device device)
        {
            await _db.Devices.AddAsync(device);
            _db.SaveChanges();
        }

        public async Task UpdateDevice(Device device)
        {
            _db.Entry(device).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task RemoveDevice(int id)
        {
            Device device = await GetDevice(id);
            _db.Devices.Remove(device);
            await _db.SaveChangesAsync();
        }

        public bool FindDuplicateName(string name, int id)
        {
            bool Exists = _db.Devices.Any(d => d.Name == name && d.DeviceId != id);
            return Exists;
        }
    }
}
