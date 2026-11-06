using System;
using System.Data.SqlTypes;
using Backend.DTOs;

namespace Backend.Services;

public interface IPostService
{
public Task<IEnumerable<PostDto>> Get();
}
