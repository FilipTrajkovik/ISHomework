using EShopTicketCinema.Domain.DomainModels;
using EShopTicketCinema.Domain.DTO;
using EShopTicketCinema.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.IO;
using ClosedXML.Excel;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EShopTicketCinema.Web.Controllers
{
    public class TicketsController : Controller
    {
        private readonly ITicketService _ticketService;

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        // GET: Tickets
        public IActionResult Index()
        {
            var allTickets = this._ticketService.GetAllTickets();
            return View(allTickets);
        }

        [HttpPost]
        public IActionResult Index(DateTime filterDate)
        {
            if (filterDate == null)
            {
                return View(_ticketService.GetAllTickets());
            }
            else
            {
                return View(_ticketService.GetAllTicketsByDate(filterDate));
            }

        }

        public IActionResult AddTicketToCard(Guid? id)
        {
            var model = this._ticketService.GetShoppingCartInfo(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddTicketToCard([Bind("TicketId", "Quantity")] AddToShoppingCardDto item)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = this._ticketService.AddToShoppingCart(item, userId);

            if (result)
            {
                return RedirectToAction("Index", "Tickets");
            }

            return View(item);
        }

        [HttpPost]
        public FileContentResult ExportTickets(string Genre)
        {

            string fileName = "Tickets_Cinema.xlsx";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            List<Ticket> tickets = new List<Ticket>();

            if (Genre is null || Genre.Length == 0 || Genre.Equals(""))
            {
                tickets = _ticketService.GetAllTickets();
            }
            else
            {
                tickets = _ticketService.GetAllTicketsWithGenre(Genre);
            }

            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.Worksheets.Add("All Tickets Cinema");
                worksheet.Cell(1, 1).Value = "Movie";
                worksheet.Cell(1, 2).Value = "Description";
                worksheet.Cell(1, 3).Value = "Price";
                worksheet.Cell(1, 4).Value = "Rating";
                worksheet.Cell(1, 5).Value = "Duration";
                worksheet.Cell(1, 6).Value = "Genre";

                for (int i = 1; i <= tickets.Count(); i++)
                {
                    var item = tickets[i - 1];

                    worksheet.Cell(i + 1, 1).Value = item.MovieName.ToString();
                    worksheet.Cell(i + 1, 2).Value = item.MovieDescription.ToString();
                    worksheet.Cell(i + 1, 3).Value = item.MoviePrice.ToString();
                    worksheet.Cell(i + 1, 4).Value = item.MovieRating.ToString();
                    worksheet.Cell(i + 1, 5).Value = item.MovieDuration.ToString();
                    worksheet.Cell(i + 1, 6).Value = item.MovieGenre.ToString();
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(content, contentType, fileName);
                }

            }
        }

        // GET: Tickets/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = this._ticketService.GetDetailsForTicket(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: Tickets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,MovieName,MovieDescription,MovieImage,MoviePrice,MovieRating,MovieDuration,MovieGenre,DateValid")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                this._ticketService.CreateNewTicket(ticket);
                return RedirectToAction(nameof(Index));
            }
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = this._ticketService.GetDetailsForTicket(id);

            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,MovieName,MovieDescription,MovieImage,MoviePrice,MovieRating,MovieDuration,MovieGenre,DateValid")] Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    this._ticketService.UpdeteExistingTicket(ticket);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.Id))
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
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = this._ticketService.GetDetailsForTicket(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            this._ticketService.DeleteTicket(id);
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(Guid id)
        {
            return this._ticketService.GetDetailsForTicket(id) != null;
        }
    }
}
