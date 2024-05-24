using Microsoft.BugTracker.Entities;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.BugTracker.Repositories;
public class TicketRepository(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;

    public async Task<List<Ticket>> GetAllTicketsAsync()
    {
        return await _context.Tickets.ToListAsync();
    }

    public async Task AddTicketAsync(Ticket ticket)
    {
        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync();
    }

    // Additional methods to update, delete, etc.
}