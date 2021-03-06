﻿using Microsoft.EntityFrameworkCore;
using Summer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Summer.Dbcontex
{
    public class SummerContext : DbContext, ISummerContext
    {

        
        public SummerContext(DbContextOptions<SummerContext> options) : base(options)
        {
            
        }        



        public DbSet<Client> Clients { get; set; }
        //public DbSet<Course> Courses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(Configuration.GetConnectionString("CBA_Database") );     

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().HasKey(i => i.ClientID);
            modelBuilder.Entity<Client>().Property(i => i.Address).HasMaxLength(100);
            modelBuilder.Entity<Client>().Property(i => i.City).HasMaxLength(50);
            modelBuilder.Entity<Client>().Property(i => i.FirstName).HasMaxLength(50);
            modelBuilder.Entity<Client>().Property(i => i.LastName).HasMaxLength(50);

            modelBuilder.Entity<Client>().Property(i => i.BirthDay).IsRequired();
        }

    }



}
