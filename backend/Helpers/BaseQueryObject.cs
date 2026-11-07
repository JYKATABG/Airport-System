using System;

namespace backend.Helpers;

public class BaseQueryObject
{
    public string? SortBy { get; set; }
    public bool IsDescending { get; set; } = false;

    public int PageNumber { get; set; } = 1;
    public int _pageSize = 10;
    private int MaxPageSize { get; set; } = 50;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }
}
