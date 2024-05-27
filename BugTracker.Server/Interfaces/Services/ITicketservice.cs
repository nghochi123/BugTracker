using Microsoft.BugTracker.Entities;
using Microsoft.BugTracker.Dtos;

namespace Microsoft.BugTracker.Interfaces;
public interface ITicketService {
    Task<List<TicketDto>> GetAllTicketsAsync(string ticketId);
    Task AddTicketAsync(Ticket ticket);
    Task<TicketDto> GetProjectTicketById(string ticketId);
    Task<Ticket> UpdateProjectTicketAsync(Ticket ticket);
    Task DeleteProjectTicketById(string ticketId);
}