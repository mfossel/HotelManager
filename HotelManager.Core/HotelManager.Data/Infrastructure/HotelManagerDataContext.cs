using HotelManager.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.Data.Infrastructure
{
    public class WingmanDataContext : DbContext
    {
        public WingmanDataContext() : base("HotelManager")
        {
        }

        //SQL Tables
        public IDbSet<Customer> Customers { get; set; }
        public IDbSet<Reservation> Reservations { get; set; }
        public IDbSet<Room> Rooms { get; set; }
        public IDbSet<User> Users { get; set; }
        public IDbSet<Workorder> Workorders { get; set; }

        //Model Relationships
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Room>()
              .HasMany(r => r.Reservations)
              .WithRequired(res => res.Room)
              .HasForeignKey(res => res.RoomId);

            modelBuilder.Entity<Property>()
                .HasMany(p => p.WorkOrders)
                .WithRequired(wo => wo.Property)
                .HasForeignKey(wo => wo.PropertyId);

            modelBuilder.Entity<Tenant>()
                .HasMany(t => t.Leases)
                .WithRequired(l => l.Tenant)
                .HasForeignKey(l => l.TenantId);

            modelBuilder.Entity<Tenant>()
                .HasMany(t => t.WorkOrders)
                .WithOptional(wo => wo.Tenant)
                .HasForeignKey(wo => wo.TenantId);



            modelBuilder.Entity<PropertyManagerUser>()
                .HasMany(u => u.Properties)
                .WithRequired(p => p.User)
                .HasForeignKey(p => p.UserId);


            modelBuilder.Entity<PropertyManagerUser>()
                .HasMany(u => u.Tenants)
                .WithRequired(t => t.User)
                .HasForeignKey(t => t.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PropertyManagerUser>()
                .HasMany(u => u.Leases)
                .WithRequired(l => l.User)
                .HasForeignKey(l => l.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PropertyManagerUser>()
                .HasMany(u => u.WorkOrders)
                .WithRequired(wo => wo.User)
                .HasForeignKey(wo => wo.UserId)
                .WillCascadeOnDelete(false);


            base.OnModelCreating(modelBuilder);
        }


    }
}
