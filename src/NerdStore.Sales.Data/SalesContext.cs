﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NerdStore.Core.Abstractions;
using NerdStore.Core.Messages;
using NerdStore.Sales.Data.Extensions;
using NerdStore.Sales.Domain;

namespace NerdStore.Sales.Data;

public class SalesContext : DbContext, IUnitOfWork
{
    private readonly IMediatorHandler _mediatorHandler;

    public SalesContext(DbContextOptions<SalesContext> options, IMediatorHandler mediatorHandler)
        : base(options)
    {
        _mediatorHandler = mediatorHandler;
    }

    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Voucher> Vouchers { get; set; }


    public async Task<bool> CommitAsync()
    {
        foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("CreatedAt") != null))
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property("CreatedAt").CurrentValue = DateTime.Now;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Property("CreatedAt").IsModified = false;
            }
        }

        var success = await base.SaveChangesAsync() > 0;

        if (success) 
            await _mediatorHandler.PublishEvents(this);

        return success;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
            e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(100)");

        modelBuilder.Ignore<Event>();

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SalesContext).Assembly);

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

        modelBuilder.HasSequence<int>("MySequence").StartsAt(1000).IncrementsBy(1);
        base.OnModelCreating(modelBuilder);
    }
}
