using Data.DataEntities;
using Data.DataContext;
using DataAPI.DTOs.ContactMessage;
using DataAPI.Services.Interfaces;

namespace DataAPI.Services.Implementations;

public class ContactMessageService : IContactMessageService
{
    private readonly AppDbContext _dataDbContext;

    public ContactMessageService(AppDbContext dataDbContext)
    {
        _dataDbContext = dataDbContext;
    }

    public async Task<ContactMessageDto?> GetContactMessageByIdAsync(int id)
    {
        var contactMessage = await _dataDbContext.ContactMessages.FindAsync(id);

        if (contactMessage == null)
            return null;

        return new ContactMessageDto
        {
            Id = contactMessage.Id,
            Name = contactMessage.Name,
            Email = contactMessage.Email,
            Subject = contactMessage.Subject,
            Message = contactMessage.Message,
            SentDate = contactMessage.SentDate,
            IsRead = contactMessage.IsRead,
            Reply = contactMessage.Reply,
            ReplyDate = contactMessage.ReplyDate
        };
    }

    public async Task<ContactMessageDto> CreateContactMessageAsync(CreateContactMessageDto createDto)
    {
        var newContactMessage = new ContactMessage
        {
            Name = createDto.Name,
            Email = createDto.Email,
            Subject = createDto.Subject,
            Message = createDto.Message,
            SentDate = createDto.SentDate,
            IsRead = createDto.IsRead,
            Reply = createDto.Reply,
            ReplyDate = createDto.ReplyDate
        };

        _dataDbContext.ContactMessages.Add(newContactMessage);
        await _dataDbContext.SaveChangesAsync();

        return new ContactMessageDto
        {
            Id = newContactMessage.Id,
            Name = newContactMessage.Name,
            Email = newContactMessage.Email,
            Subject = newContactMessage.Subject,
            Message = newContactMessage.Message,
            SentDate = newContactMessage.SentDate,
            IsRead = newContactMessage.IsRead,
            Reply = newContactMessage.Reply,
            ReplyDate = newContactMessage.ReplyDate
        };
    }

    public async Task<ContactMessageDto?> UpdateContactMessageAsync(UpdateContactMessageDto updateDto)
    {
        var existContactMessage = await _dataDbContext.ContactMessages.FindAsync(updateDto.Id);

        if (existContactMessage == null)
            return null;

        existContactMessage.Name = updateDto.Name;
        existContactMessage.Email = updateDto.Email;
        existContactMessage.Subject = updateDto.Subject;
        existContactMessage.Message = updateDto.Message;
        existContactMessage.SentDate = updateDto.SentDate;
        existContactMessage.IsRead = updateDto.IsRead;
        existContactMessage.Reply = updateDto.Reply;
        existContactMessage.ReplyDate = updateDto.ReplyDate;

        _dataDbContext.ContactMessages.Update(existContactMessage);
        await _dataDbContext.SaveChangesAsync();

        return new ContactMessageDto
        {
            Id = existContactMessage.Id,
            Name = existContactMessage.Name,
            Email = existContactMessage.Email,
            Subject = existContactMessage.Subject,
            Message = existContactMessage.Message,
            SentDate = existContactMessage.SentDate,
            IsRead = existContactMessage.IsRead,
            Reply = existContactMessage.Reply,
            ReplyDate = existContactMessage.ReplyDate
        };
    }

    public async Task<bool> DeleteContactMessageAsync(int id)
    {
        var existContactMessage = await _dataDbContext.ContactMessages.FindAsync(id);

        if (existContactMessage == null)
            return false;

        _dataDbContext.ContactMessages.Remove(existContactMessage);
        await _dataDbContext.SaveChangesAsync();

        return true;
    }
}
