﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RailwayManagementSystem.Data;
using RailwayManagementSystem.Models;
using RailwayManagementSystem.Services;

namespace RailwayManagementSystem.Controllers
{
    /// <summary>
    /// Controller for Booking infoes page
    /// </summary>
    public class BookingInfoesController : Controller
    {
        private readonly RMSContext _context;
        private readonly LoginService _loginService;
        private List<Station> Stations { get; set; }
        private static BookingInfo BookingInfoInEdit { get; set; }
        public BookingInfoesController(RMSContext context, LoginService loginService)
        {
            _context = context;
            _loginService = loginService;
        }

        private async Task LoadStations()
        {
            Stations = await _context.Station.ToListAsync();
            TempData["Stations"] = Stations;
        }

        // GET: BookingInfoes
        public async Task<IActionResult> Index()
        {
            if (LoginService.IsLoggedIn)
            {
                if (LoginService.IsAdmin)
                {
                    return View(await _context.BookingInfo.ToListAsync());
                }
                return View(await _context.BookingInfo.Where(b => b.UserId == _loginService.GetCurrentUserId()).ToListAsync());
            }
            return RedirectToAction("Create");
        }

        // GET: BookingInfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingInfo = await _context.BookingInfo
                .FirstOrDefaultAsync(m => m.Pnr == id);
            if (bookingInfo == null)
            {
                return NotFound();
            }

            return View(bookingInfo);
        }

        // GET: BookingInfoes/Create
        public async Task<IActionResult> Create()
        {
            await LoadStations();
            return View();
        }

        // POST: BookingInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Pnr,BookingDate,SourceStation,DestinationStation,TicketFare,BookingStatus")] BookingInfo bookingInfo)
        {
            if (ModelState.IsValid)
            {
                //assigning the bookingInfo.UserId to the current logged in user id
                bookingInfo.UserId = _loginService.GetCurrentUserId();
                bookingInfo.BookingStatus = true;
                _context.Add(bookingInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bookingInfo);
        }

        // GET: BookingInfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingInfo = await _context.BookingInfo.FindAsync(id);
            if (bookingInfo == null)
            {
                return NotFound();
            }
            BookingInfoInEdit = bookingInfo;
            return View(bookingInfo);
        }

        // POST: BookingInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Pnr,BookingDate,SourceStation,DestinationStation,TicketFare,BookingStatus")] BookingInfo bookingInfo)
        {
            if (id != bookingInfo.Pnr)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    bookingInfo.UserId = BookingInfoInEdit.UserId;
                    _context.Update(bookingInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingInfoExists(bookingInfo.Pnr))
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
            return View(bookingInfo);
        }

        // GET: BookingInfoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingInfo = await _context.BookingInfo
                .FirstOrDefaultAsync(m => m.Pnr == id);
            if (bookingInfo == null)
            {
                return NotFound();
            }

            return View(bookingInfo);
        }

        // POST: BookingInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookingInfo = await _context.BookingInfo.FindAsync(id);
            if (bookingInfo != null)
            {
                bookingInfo.BookingStatus = false;
                _context.Update(bookingInfo);
                //_context.BookingInfo.Remove(bookingInfo);//do not remove the entry from database, only change booking status
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingInfoExists(int id)
        {
            return _context.BookingInfo.Any(e => e.Pnr == id);
        }
    }
}
