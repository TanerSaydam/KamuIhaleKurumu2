using AcikArrtirmaServer.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcikArrtirmaServer.Domain.Notices;

public sealed class Notice : Entity
{
    private Notice()
    { }

    public Notice(string title, string description, Money startingFee, int expireTime)
    {
        Title = title;
        Description = description;
        StartingFee = startingFee;
        IsCompleted = false;
        ExpireTime = expireTime;
        CreatedDate = DateTime.Now;
    }

    public string Title { get; private set; }
    public string Description { get; private set; }
    public Money StartingFee { get; private set; }
    public bool IsCompleted { get; private set; }
    public int ExpireTime { get; private set; }
    public DateTime CreatedDate { get; private set; }
}

public sealed record Money(decimal Amount, string Currency);