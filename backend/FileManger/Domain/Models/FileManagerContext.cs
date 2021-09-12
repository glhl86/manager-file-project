﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Domain.Models
{
    public partial class FileManagerContext : DbContext
    {
        public FileManagerContext()
        {
        }

        public FileManagerContext(DbContextOptions<FileManagerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Structure> Structures { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}