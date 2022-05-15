using EFCore.Data_models;

namespace ASP.NETCoreWebAPI.Models.DataTransferObjects;

public record class TagDto
(
    int Id,
    string ProductTag
);