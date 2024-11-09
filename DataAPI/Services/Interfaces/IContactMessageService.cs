using DataAPI.DTOs.ContactMessage;

namespace DataAPI.Services.Interfaces;

public interface IContactMessageService
{
    Task<ContactMessageDto?> GetContactMessageByIdAsync(int id);
    Task<ContactMessageDto> CreateContactMessageAsync(CreateContactMessageDto createDto);
    Task<ContactMessageDto?> UpdateContactMessageAsync(UpdateContactMessageDto updateDto);
    Task<bool> DeleteContactMessageAsync(int id);
}
