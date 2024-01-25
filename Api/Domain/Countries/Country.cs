﻿using Api.Data.Entities;

namespace Api.Domain.Countries;

public class Country : BaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Iso2Code { get; set; } = string.Empty;
}
