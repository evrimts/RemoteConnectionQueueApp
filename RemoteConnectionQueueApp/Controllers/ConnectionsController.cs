using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RemoteConnectionQueueApp.Areas.Identity.Data;
using RemoteConnectionQueueApp.Data;
using RemoteConnectionQueueApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemoteConnectionQueueApp.Controllers
{
    public class ConnectionsController : Controller
    {

        private readonly ApplicationDbContext _db;
        private readonly INotyfService _notyf;

        [BindProperty]
        public RemoteConnection RemoteConnection { get; set; }

        public IEnumerable<Queue> Queues { get; set; }

        public ConnectionsController(ApplicationDbContext db, INotyfService notyf)
        {
            _db = db;
            _notyf = notyf;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Connect(int id)
        {

            RemoteConnection rc = await _db.RemoteConnection.FirstOrDefaultAsync(u => u.Id == id);

            return View(rc);
        }
        public async Task<IActionResult> JoinQueue(int id)
        {

            //queue = _db.Queue.FirstOrDefault(u => u.RemoteConnectionId == id);

            Queues = await _db.Queue.Where(u => u.RemoteConnectionId == id).ToListAsync();

            ViewBag.RemoteConnectionId = id;

            return View(Queues);
        }

        public IActionResult ChangeQueue(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            //Check if already connected
            RemoteConnection = new RemoteConnection();
            RemoteConnection = _db.RemoteConnection.FirstOrDefault(u => u.Id == id);

            if (RemoteConnection == null)
            {
                return NotFound();
            }
            else
            {
                if (RemoteConnection.BagliKisi == User.Identity.Name)
                {
                    _notyf.Error("Zaten bağlısınız.");
                    return RedirectToAction("Index");
                }
            }

            Queue queue = new Queue();

            //Check if already in queue:
            queue = _db.Queue.FirstOrDefault(i => i.RemoteConnectionId == id && i.BekleyenKisi == User.Identity.Name);

            if (queue == null)
            {
                //Not in queue. Add.
                Queue queue1 = new Queue();
                queue1.BekleyenKisi = User.Identity.Name;
                queue1.RemoteConnectionId = id;
                _db.Queue.Add(queue1);
                _db.SaveChanges();
            }
            else
            {
                //User in queue. Remove.
                _db.Queue.Remove(queue);
                _db.SaveChanges();
            }
            
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult Upsert(int? id)
        {
            RemoteConnection = new RemoteConnection();
            if (id == null)
            {
                //create
                return View(RemoteConnection);
            }
            //update
            RemoteConnection = _db.RemoteConnection.FirstOrDefault(u => u.Id == id);
            if (RemoteConnection == null)
            {
                return NotFound();
            }
            return View(RemoteConnection);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Connect()
        {
            if (ModelState.IsValid)
            {
                RemoteConnection.BagliKisi = User.Identity.Name;
                RemoteConnection.BaglantiZamani = DateTime.Now;
                _db.RemoteConnection.Update(RemoteConnection);

                //Remove if in queue
                Queue queue = new Queue();

                queue = _db.Queue.FirstOrDefault(i => i.RemoteConnectionId == RemoteConnection.Id && i.BekleyenKisi == User.Identity.Name);

                if (queue != null)
                {
                    //User in queue. Remove.
                    _db.Queue.Remove(queue);
                }
                
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(RemoteConnection);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {
            if (ModelState.IsValid)
            {
                if (RemoteConnection.Id == 0)
                {
                    //create
                    _db.RemoteConnection.Add(RemoteConnection);
                }
                else
                {
                    _db.RemoteConnection.Update(RemoteConnection);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(RemoteConnection);
        }

        #region API Calls
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.RemoteConnection.ToListAsync() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var ConnFromDb = await _db.RemoteConnection.FirstOrDefaultAsync(u => u.Id == id);
            if (ConnFromDb == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            _db.RemoteConnection.Remove(ConnFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successfull" });
        }

        [HttpPost]
        public async Task<IActionResult> Disconnect(int id)
        {
            var ConnFromDb = await _db.RemoteConnection.FirstOrDefaultAsync(u => u.Id == id);
            if (ConnFromDb == null)
            {
                return Json(new { success = false, message = "Ayrılırken hata oluştu." });
            }

            if (ConnFromDb.BagliKisi != User.Identity.Name)
            {
                return Json(new { success = false, message = "Zaten bağlı değilsiniz." });
            }

            ConnFromDb.BagliKisi = null;
            ConnFromDb.BaglantiZamani = null;
            ConnFromDb.Note = null;
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Ayrılma işlemi başarılı." });
        }
        #endregion
    }
}
