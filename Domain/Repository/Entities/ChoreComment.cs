using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Repository.Entities;

public class ChoreComment : BaseEntity 
{
    public Guid Id { get; set; }
    string Message { get; set; }
    string CustomerChoreId { get; set; }
    string UserId { get; set; }
}