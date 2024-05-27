using Microsoft.BugTracker.Entities;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.BugTracker.Repositories;
public class TicketRepository(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;

    public async Task<List<Ticket>> GetAllProjectTicketsAsync(string projectId)
    {
        var allTickets = await _context.Tickets.ToListAsync();
        var projectTickets = allTickets.Where(t => t.ProjectId == projectId).ToList();
        return projectTickets;
    }

    public async Task AddTicketAsync(Ticket ticket)
    {
        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync();
    }
    
    public async Task<Ticket> GetProjectTicketById(string ticketId)
    {
        return await _context.Tickets.FindAsync(ticketId);
    }

    public async Task<Ticket> UpdateProjectTicketAsync(Ticket ticket)
    {
        var oldTicket = await _context.Tickets.FindAsync(ticket.Id);
        if (oldTicket == null)
        {
            throw new ArgumentException("Ticket not found with id: " + oldTicket.Id);
        }
        ticket.SetUpdatedAt(DateTime.Now.ToUniversalTime());
        _context.Entry(oldTicket).CurrentValues.SetValues(ticket);
        await _context.SaveChangesAsync();
        return ticket;
    }

    public async Task DeleteProjectTicketById(string ticketId)
    {
        var ticket = await _context.Tickets.FindAsync(ticketId);
        if (ticket != null)
        {
            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new ArgumentNullException("Project not found");
        }
    }

}