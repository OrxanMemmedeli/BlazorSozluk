using BlazorSozluk.Api.Domain.Entities.Common;

namespace BlazorSozluk.Api.Domain.Entities;

public class EmailConfirmation : BaseEntity
{
    public string OldEmailAdress { get; set; }
    public string NewEmailAdress { get; set; }
}
