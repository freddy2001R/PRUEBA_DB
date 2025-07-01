using Microsoft.EntityFrameworkCore;
using PRUEBA.Models;
using System;
using BCrypt.Net; // Asegúrate de que el paquete NuGet BCrypt.Net-Next esté instalado

namespace PRUEBA.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Procedure> Procedures { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<DataSet> DataSets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuraciones de relaciones
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserID, ur.RoleID });

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserID);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleID);

            modelBuilder.Entity<Procedure>()
                .HasOne(p => p.CreatedByUser)
                .WithMany(u => u.CreatedProcedures)
                .HasForeignKey(p => p.CreatedByUserID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Procedure>()
                .HasOne(p => p.LastModifiedByUser)
                .WithMany(u => u.ModifiedProcedures)
                .HasForeignKey(p => p.LastModifiedUserID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DataSet>()
                .HasOne(ds => ds.Procedure)
                .WithMany(p => p.DataSets)
                .HasForeignKey(ds => ds.ProcedureID);

            modelBuilder.Entity<DataSet>()
                .HasOne(ds => ds.Field)
                .WithMany(f => f.DataSets)
                .HasForeignKey(ds => ds.FieldId);

            // Datos de semilla

            // Roles
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleID = 1, RoleName = "Admin" },
                new Role { RoleID = 2, RoleName = "User" }
            );

          
            modelBuilder.Entity<User>().HasData(
                new User { UserID = 1, Username = "admin", Password = "TU_HASH_REAL_PARA_ADMIN123_AQUI", Email = "admin@example.com" },
                new User { UserID = 2, Username = "user1", Password = "TU_HASH_REAL_PARA_USER123_AQUI", Email = "user1@example.com" }
            );

            // UserRoles
            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { ID = 1, UserID = 1, RoleID = 1 },
                new UserRole { ID = 2, UserID = 2, RoleID = 2 }  
            );

            // Procedures 
            modelBuilder.Entity<Procedure>().HasData(
                new Procedure { ProcedureID = 1, ProcedureName = "Data Cleaning", Description = "Process for cleaning raw data.", CreatedByUserID = 1, CreatedDate = new DateTime(2023, 1, 10, 8, 0, 0, DateTimeKind.Utc), LastModifiedUserID = 1, LastModifiedDate = new DateTime(2023, 1, 10, 8, 0, 0, DateTimeKind.Utc) },
                new Procedure { ProcedureID = 2, ProcedureName = "Statistical Analysis", Description = "Performing statistical tests on data.", CreatedByUserID = 1, CreatedDate = new DateTime(2023, 2, 15, 9, 30, 0, DateTimeKind.Utc), LastModifiedUserID = 1, LastModifiedDate = new DateTime(2023, 2, 15, 9, 30, 0, DateTimeKind.Utc) },
                new Procedure { ProcedureID = 3, ProcedureName = "Data Visualization", Description = "Creating charts and dashboards.", CreatedByUserID = 2, CreatedDate = new DateTime(2023, 3, 20, 10, 0, 0, DateTimeKind.Utc), LastModifiedUserID = 2, LastModifiedDate = new DateTime(2023, 3, 20, 10, 0, 0, DateTimeKind.Utc) },
                new Procedure { ProcedureID = 4, ProcedureName = "Machine Learning Model", Description = "Developing and training ML models.", CreatedByUserID = 1, CreatedDate = new DateTime(2023, 4, 25, 11, 45, 0, DateTimeKind.Utc), LastModifiedUserID = 1, LastModifiedDate = new DateTime(2023, 4, 25, 11, 45, 0, DateTimeKind.Utc) },
                new Procedure { ProcedureID = 5, ProcedureName = "Data Integration", Description = "Combining data from various sources.", CreatedByUserID = 2, CreatedDate = new DateTime(2023, 5, 1, 13, 0, 0, DateTimeKind.Utc), LastModifiedUserID = 2, LastModifiedDate = new DateTime(2023, 5, 1, 13, 0, 0, DateTimeKind.Utc) }
            );

            // Fields
            modelBuilder.Entity<Field>().HasData(
                new Field { FieldID = 1, FieldName = "SampleField1", DataType = "String" },
                new Field { FieldID = 2, FieldName = "SampleField2", DataType = "Number" }
            );

            // DataSets 
            modelBuilder.Entity<DataSet>().HasData(
                new DataSet { DataSetID = 1, DataSetName = "Sales Data 2023", Description = "Annual sales records.", ProcedureID = 1, FieldId = 1, CreatedDate = new DateTime(2023, 1, 10, 8, 15, 0, DateTimeKind.Utc), LastModifiedDate = new DateTime(2023, 1, 10, 8, 15, 0, DateTimeKind.Utc) },
                new DataSet { DataSetID = 2, DataSetName = "Customer Feedback", Description = "Survey responses from Q1.", ProcedureID = 2, FieldId = 2, CreatedDate = new DateTime(2023, 2, 15, 9, 45, 0, DateTimeKind.Utc), LastModifiedDate = new DateTime(2023, 2, 15, 9, 45, 0, DateTimeKind.Utc) },
                new DataSet { DataSetID = 3, DataSetName = "Product Inventory", Description = "Current stock levels.", ProcedureID = 1, FieldId = 1, CreatedDate = new DateTime(2023, 1, 20, 10, 0, 0, DateTimeKind.Utc), LastModifiedDate = new DateTime(2023, 1, 20, 10, 0, 0, DateTimeKind.Utc) },
                new DataSet { DataSetID = 4, DataSetName = "Website Traffic", Description = "Visitor data for Q2.", ProcedureID = 3, FieldId = 2, CreatedDate = new DateTime(2023, 3, 25, 10, 30, 0, DateTimeKind.Utc), LastModifiedDate = new DateTime(2023, 3, 25, 10, 30, 0, DateTimeKind.Utc) },
                new DataSet { DataSetID = 5, DataSetName = "Employee Performance", Description = "Quarterly performance reviews.", ProcedureID = 2, FieldId = 1, CreatedDate = new DateTime(2023, 2, 20, 11, 0, 0, DateTimeKind.Utc), LastModifiedDate = new DateTime(2023, 2, 20, 11, 0, 0, DateTimeKind.Utc) },
                new DataSet { DataSetID = 6, DataSetName = "Financial Records", Description = "Annual financial statements.", ProcedureID = 5, FieldId = 1, CreatedDate = new DateTime(2023, 5, 5, 14, 0, 0, DateTimeKind.Utc), LastModifiedDate = new DateTime(2023, 5, 5, 14, 0, 0, DateTimeKind.Utc) },
                new DataSet { DataSetID = 7, DataSetName = "Customer Demographics", Description = "Demographic data of customer base.", ProcedureID = 3, FieldId = 2, CreatedDate = new DateTime(2023, 3, 30, 9, 0, 0, DateTimeKind.Utc), LastModifiedDate = new DateTime(2023, 3, 30, 9, 0, 0, DateTimeKind.Utc) },
                new DataSet { DataSetID = 8, DataSetName = "Server Logs", Description = "Server activity and error logs.", ProcedureID = 4, FieldId = 1, CreatedDate = new DateTime(2023, 4, 30, 12, 0, 0, DateTimeKind.Utc), LastModifiedDate = new DateTime(2023, 4, 30, 12, 0, 0, DateTimeKind.Utc) },
                new DataSet { DataSetID = 9, DataSetName = "HR Data", Description = "Human Resources employee data.", ProcedureID = 5, FieldId = 2, CreatedDate = new DateTime(2023, 5, 10, 10, 0, 0, DateTimeKind.Utc), LastModifiedDate = new DateTime(2023, 5, 10, 10, 0, 0, DateTimeKind.Utc) },
                new DataSet { DataSetID = 10, DataSetName = "Marketing Campaign Data", Description = "Performance data for recent campaigns.", ProcedureID = 3, FieldId = 1, CreatedDate = new DateTime(2023, 4, 5, 15, 0, 0, DateTimeKind.Utc), LastModifiedDate = new DateTime(2023, 4, 5, 15, 0, 0, DateTimeKind.Utc) },
                new DataSet { DataSetID = 11, DataSetName = "Supply Chain Data", Description = "Logistics and inventory flow.", ProcedureID = 1, FieldId = 2, CreatedDate = new DateTime(2023, 2, 1, 9, 0, 0, DateTimeKind.Utc), LastModifiedDate = new DateTime(2023, 2, 1, 9, 0, 0, DateTimeKind.Utc) },
                new DataSet { DataSetID = 12, DataSetName = "IoT Sensor Readings", Description = "Data from various IoT devices.", ProcedureID = 4, FieldId = 1, CreatedDate = new DateTime(2023, 5, 15, 16, 0, 0, DateTimeKind.Utc), LastModifiedDate = new DateTime(2023, 5, 15, 16, 0, 0, DateTimeKind.Utc) },
                new DataSet { DataSetID = 13, DataSetName = "Customer Support Tickets", Description = "Records of customer support interactions.", ProcedureID = 2, FieldId = 2, CreatedDate = new DateTime(2023, 3, 1, 10, 0, 0, DateTimeKind.Utc), LastModifiedDate = new DateTime(2023, 3, 1, 10, 0, 0, DateTimeKind.Utc) },
                new DataSet { DataSetID = 14, DataSetName = "Research Study Results", Description = "Data from scientific research.", ProcedureID = 5, FieldId = 1, CreatedDate = new DateTime(2023, 5, 20, 11, 0, 0, DateTimeKind.Utc), LastModifiedDate = new DateTime(2023, 5, 20, 11, 0, 0, DateTimeKind.Utc) },
                new DataSet { DataSetID = 15, DataSetName = "Social Media Engagement", Description = "Metrics from social media platforms.", ProcedureID = 3, FieldId = 2, CreatedDate = new DateTime(2023, 4, 10, 13, 0, 0, DateTimeKind.Utc), LastModifiedDate = new DateTime(2023, 4, 10, 13, 0, 0, DateTimeKind.Utc) }
            );
        }
    }
}