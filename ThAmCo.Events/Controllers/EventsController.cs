using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;

using ThAmCo.Events.Data;
using ThAmCo.Events.Models;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace ThAmCo.Events.Controllers
{
    public class EventsController : Controller
    {
        private readonly EventsDbContext _context;

        public EventsController(EventsDbContext context)
        {
            _context = context;
        }

        // GET: Events
        public async Task<IActionResult> Index(int? id)
        {
            ViewData["eventID"] = id;
            return View(await _context.Events.ToListAsync());
        }

        public async Task<IActionResult> ListGuests(int? id)
        {
            var model = _context.Guests.Where(p => p.EventId == id).Include(g => g.Customer).ToList();
            var viewModel = model.Select(p => new EventViewModel
            {
                id = p.EventId,
                CustomerId = p.CustomerId,
                FirstName = p.Customer.FirstName,
                Surname = p.Customer.Surname,
                Attended = p.Attended
            });
            
            return View(viewModel);
        }

        
        public async Task<IActionResult> ReserveVenue(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .FirstOrDefaultAsync(m => m.Id == id);

            if (@event == null)
            {
                return NotFound();
            }
            var venues = new List<VenueDto>().AsEnumerable();
            HttpClient client = new HttpClient();
            client.BaseAddress = new System.Uri("http://localhost:23652/");
            client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
            HttpResponseMessage response = await client.GetAsync("api/availability");
            if (response.IsSuccessStatusCode)
            {
                venues = await response.Content.ReadAsAsync<IEnumerable<VenueDto>>();
            }
            else
            {
                Debug.WriteLine("reserve venue recieved a bad response from the web service");
            }
            var model = _context.Events.Where(m => m.Id == id);
            var viewModel = model.Select(p => new reserveVenueModel
            {
                Id = p.Id,
                Title = p.Title,
                Date = p.Date,
                Duration = p.Duration,
                TypeId = p.TypeId,
                Venues = new  SelectList(venues, "Code", "Name")
               

        }) ;
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAttended(int id, [Bind("CustomerId,EventId,Attended")] GuestBooking guestBooking)
        {


            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(guestBooking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(guestBooking.EventId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
            }


            return RedirectToAction(nameof(ListGuests), new { id = guestBooking.EventId });

        }


        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .FirstOrDefaultAsync(m => m.Id == id);

            if (@event == null)
            {
                return NotFound();
            }


            return View(@event);
        }

        // GET: Events/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Date,Duration,TypeId")] Event @event)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@event);
        }

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Date,Duration,TypeId")] Event @event)
        {
            if (id != @event.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.Id))
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
            return View(@event);
        }

        [HttpPost]
        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBooking(int id, [Bind("CustomerId,EventId,Attended,FirstName,Surname")] EventViewModel @model )
        {
            if (@model.id == null || @model.CustomerId == null)
            {
               return NotFound();
            }
            var @model2 = model;
            model2.id = id;
            //var Booking = await _context.Customers.FirstOrDefaultAsync(m => m.Id == CustomerId) ;
                
            //if(Booking == null)
            //{
               // return NotFound();
            //}
            return View(@model2);
        }

        public async Task<IActionResult> DeleteBookingConfirmed(int? id, [Bind("CustomerId,EventId,Attended,FirstName,Surname")] EventViewModel @model)
        {
            var @booking = await _context.Guests.FirstOrDefaultAsync(m => m.CustomerId == model.CustomerId && m.EventId == id); //&& m.EventId == EventId) ;
            //var @booking = await _context.Events.Include(g => g.Bookings).FirstOrDefault(g => g.Bookings.);
            
            _context.Guests.Remove(@booking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index) );
            
            
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await _context.Events.FindAsync(id);
            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index) );
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.Id == id);
        }
    }
}
