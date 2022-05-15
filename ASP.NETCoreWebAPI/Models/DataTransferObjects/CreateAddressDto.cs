﻿namespace ASP.NETCoreWebAPI.Models.DataTransferObjects;

public record class CreateAddressDto
(
    string City,
    string Country,
    string ZipCode,
    string Street,
    int Building,
    int Flat,
    int Id = 0
);