using System;
using System.ComponentModel.DataAnnotations;
using Syper.ClientStateEnum;

namespace Syper.Clients;

public class CreateUpdateClientDto
{
    [Required]
    [StringLength(32)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [StringLength(32)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(128)]
    public string Email { get; set; } = string.Empty;

    public ClientState ClientState { get; set; } = ClientState.Pending;
}