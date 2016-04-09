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
        public IDbSet<Hotel> Hotels { get; set; }
        public IDbSet<Reservation> Reservations { get; set; }
        public IDbSet<Room> Rooms { get; set; }
        public IDbSet<User> Users { get; set; }
        public IDbSet<Workorder> Workorders { get; set; }

        //Model Relationships
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Response

            //Submission
            modelBuilder.Entity<Submission>()
                        .HasMany(s => s.Responses)
                        .WithRequired(r => r.Submission)
                        .HasForeignKey(r => r.SubmissionId)
                        .WillCascadeOnDelete(false);

            //Topic
            modelBuilder.Entity<Topic>()
                        .HasMany(s => s.Submissions)
                        .WithRequired(s => s.Topic)
                        .HasForeignKey(s => s.TopicId);

            //WingmanUser
            modelBuilder.Entity<WingmanUser>()
                        .HasMany(wu => wu.Responses)
                        .WithRequired(r => r.User)
                        .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<WingmanUser>()
                        .HasMany(wu => wu.Submissions)
                        .WithRequired(s => s.User)
                        .HasForeignKey(s => s.UserId)
                        .WillCascadeOnDelete(false);

            // Specify Relationships
            modelBuilder.Entity<WingmanUser>()
                        .HasMany(u => u.Roles)
                        .WithRequired(ur => ur.User)
                        .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<Role>()
                        .HasMany(r => r.Users)
                        .WithRequired(ur => ur.Role)
                        .HasForeignKey(ur => ur.RoleId);

            modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });

            base.OnModelCreating(modelBuilder);
        }


    }
}
