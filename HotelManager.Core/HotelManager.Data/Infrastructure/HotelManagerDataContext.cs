using HotelManager.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.Data.Infrastructure
{
    public class HotelManagerDataContext : DbContext
    {
        public HotelManagerDataContext() : base("HotelManager")
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

            modelBuilder.Entity<Room>()
                .HasMany(r => r.Workorders)
                .WithRequired(wo => wo.Room)
                .HasForeignKey(wo => wo.RoomId);

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Reservations)
                .WithRequired(r => r.Customer)
                .HasForeignKey(r => r.CustomerId);




            modelBuilder.Entity<User>()
                .HasMany(u => u.Rooms)
                .WithRequired(r => r.User)
                .HasForeignKey(r => r.UserId);


            modelBuilder.Entity<User>()
                .HasMany(u => u.Customers)
                .WithRequired(t => t.User)
                .HasForeignKey(t => t.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Reservations)
                .WithRequired(l => l.User)
                .HasForeignKey(l => l.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Workorders)
                .WithRequired(wo => wo.User)
                .HasForeignKey(wo => wo.UserId)
                .WillCascadeOnDelete(false);


            base.OnModelCreating(modelBuilder);
        }


    }
}
