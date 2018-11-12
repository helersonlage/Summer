using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Summer.Model;
using System;

namespace Summer.Dbcontex
{
    public interface ISummerContext : IDisposable
    {
        DbSet<Client> Clients { get; set; }

        int SaveChanges();
        EntityEntry Entry(object entity);
    }
}