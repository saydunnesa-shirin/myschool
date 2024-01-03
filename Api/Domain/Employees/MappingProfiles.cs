﻿using Api.Features.Employees;
using AutoMapper;

namespace Api.Domain.Employees;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        GetList();
    }

    private void GetList()
    {
        CreateMap<Employee, GetEmployee.Result>(MemberList.None);
    }
}