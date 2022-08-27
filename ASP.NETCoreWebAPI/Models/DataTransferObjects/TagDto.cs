using EFCore.Data_models;

namespace ASP.NETCoreWebAPI.Models.DataTransferObjects;

public sealed record class TagDto
(
    int Id,
    string ProductTag
);