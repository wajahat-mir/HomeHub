using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HomeHub.Data;
using HomeHub.ViewModels;
using HomeHub.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using HomeHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;

namespace HomeHub.Controllers
{
    [Authorize]
    public class DevicesController : Controller
    {
        private readonly IDeviceRepository _repo;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHostingEnvironment _env;

        public DevicesController(IDeviceRepository repo, IMapper mapper, UserManager<ApplicationUser> userManager, IHostingEnvironment env)
        {
            _repo = repo;
            _mapper = mapper;
            _userManager = userManager;
            _env = env;
        }

        // GET: Devices
        public async Task<IActionResult> Index()
        {
            var devices = await _repo.GetAllDevices(_userManager.GetUserId(User));
            var devicesViewModels = _mapper.Map<IEnumerable<DeviceViewModel>>(devices);
            ; return View(devicesViewModels);
        }

        // GET: Devices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var device = await _repo.GetDevice(Convert.ToInt32(id));
            var deviceViewModel = _mapper.Map<DeviceViewModel>(device);

            if (deviceViewModel == null)
            {
                return NotFound();
            }

            return View(deviceViewModel);
        }

        // GET: Devices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Devices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,MACAddress,location,Description")] DeviceViewModel deviceViewModel)
        {
            if (ModelState.IsValid)
            {
                var device = _mapper.Map<Device>(deviceViewModel);
                device.UserID = _userManager.GetUserId(User);
                await _repo.CreateDevice(device);
                return RedirectToAction(nameof(Index));
            }
            return View(deviceViewModel);
        }

        // GET: Devices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var device = await _repo.GetDevice(Convert.ToInt32(id));
            var deviceViewModel = _mapper.Map<DeviceViewModel>(device);
            if (deviceViewModel == null)
            {
                return NotFound();
            }
            return View(deviceViewModel);
        }

        // POST: Devices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DeviceId,Name,MACAddress,location,Description")] DeviceViewModel deviceViewModel)
        {
            if (id != deviceViewModel.DeviceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var device = _mapper.Map<Device>(deviceViewModel);
                    device.UserID = _userManager.GetUserId(User);
                    await _repo.UpdateDevice(device);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_repo.GetDevice(deviceViewModel.DeviceId) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(deviceViewModel);
        }

        // GET: Devices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var device = await _repo.GetDevice(Convert.ToInt32(id));
            var deviceViewModel = _mapper.Map<DeviceViewModel>(device);

            if (deviceViewModel == null)
            {
                return NotFound();
            }

            return View(deviceViewModel);
        }

        // POST: Devices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repo.RemoveDevice(id);
            return RedirectToAction(nameof(Index));
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult VerifyDeviceName(string Name, int DeviceId)
        {
            if (_repo.FindDuplicateName(Name, DeviceId))
                return Json($"The name {Name} is already in use.");
            return Json(true);
        }
    }
}
