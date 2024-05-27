using Microsoft.BugTracker.Repositories;
using Microsoft.BugTracker.Entities;
using Microsoft.BugTracker.Dtos;
using Microsoft.BugTracker.Interfaces;

namespace Microsoft.BugTracker.Services;
public class TicketService(TicketRepository ticketRepository) : ITicketService
{
    private readonly TicketRepository _ticketRepository = ticketRepository;

    public async Task<List<TicketDto>> GetAllTicketsAsync(string ticketId)
    {
        var tickets = await _ticketRepository.GetAllProjectTicketsAsync(ticketId);
        var ticketDtos = new List<TicketDto>();
        foreach (var ticket in tickets)
        {
            ticketDtos.Add(new TicketDto(
                ticket.Id,
                ticket.Title, 
                ticket.Description,
                ticket.Priority,
                ticket.Status,
                ticket.AssignedUserNames,
                ticket.CreatedAt,
                ticket.UpdatedAt,
                ticket.Tags
            ));
        }
        return ticketDtos;
    }

    public async Task AddTicketAsync(Ticket ticket)
    {
        await _ticketRepository.AddTicketAsync(ticket);
    }

    public async Task<TicketDto> GetProjectTicketById(string ticketId)
    {
        var ticket = await _ticketRepository.GetProjectTicketById(ticketId);
        var ticketDto = new TicketDto(
                ticket.Id,
                ticket.Title, 
                ticket.Description,
                ticket.Priority,
                ticket.Status,
                ticket.AssignedUserNames,
                ticket.CreatedAt,
                ticket.UpdatedAt,
                ticket.Tags
            );
        return ticketDto;
    }
    
    public async Task<Ticket> UpdateProjectTicketAsync(Ticket ticket)
    {
        return await _ticketRepository.UpdateProjectTicketAsync(ticket);
    }

    public async Task DeleteProjectTicketById(string ticketId)
    {
        await _ticketRepository.DeleteProjectTicketById(ticketId);
    }
}