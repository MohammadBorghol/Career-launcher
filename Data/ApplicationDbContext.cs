using AIGenerator.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace AIGenerator.Data;

public class ApplicationDbContext : IdentityDbContext<Person>
{

    public DbSet<Person> Persons { get; set; }

    public DbSet<Admin> Admins { get; set; }

    public DbSet<EndUser> EndUsers { get; set; }

    public DbSet<Resume> Resumes { get; set; }

    public DbSet<Portfolio> Portfolios { get; set; }

    public DbSet<Experience> Experiences { get; set; }

    public DbSet<Education> Educations { get; set; }

    public DbSet<Certificate> Certificates { get; set; }

    public DbSet<Skill> Skills { get; set; }

    public DbSet<Language> Languages { get; set; }

    public DbSet<Service> Services { get; set; }

    public DbSet<Project> Projects { get; set; }

    public DbSet<PortfolioService> portfolioServices { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public ApplicationDbContext()
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Resume - EndUser (many resumes to one EndUser)
        builder.Entity<Resume>()
            .HasOne(r => r.endUser)
            .WithMany(e => e.resumes)
            .HasForeignKey(c=>c.endUserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Portfolio - EndUser (many portfolios to one EndUser)
        builder.Entity<Portfolio>()
            .HasOne(p => p.endUser)
            .WithMany(e => e.portfolios)
            .HasForeignKey(c=>c.endUserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Certificate - Resume (many certificates to one resume)
        builder.Entity<Certificate>()
            .HasOne(c => c.resume)
            .WithMany(r => r.certificates)
            .HasForeignKey(c => c.resumeId)
            .OnDelete(DeleteBehavior.Cascade);

        // Education - Resume (many educations to one resume)
        builder.Entity<Education>()
            .HasOne(e => e.resume)
            .WithMany(r => r.educations)
            .HasForeignKey(e => e.resumeId)
            .OnDelete(DeleteBehavior.Cascade);

        // Experience - Resume (many experiences to one resume)
        builder.Entity<Experience>()
            .HasOne(e => e.resume)
            .WithMany(r => r.experiences)
            .HasForeignKey(e => e.resumeId)
            .OnDelete(DeleteBehavior.Cascade);

        // Language - Resume (many languages to one resume)
        builder.Entity<Language>()
            .HasOne(l => l.resume)
            .WithMany(r => r.languages)
            .HasForeignKey(l => l.resumeId)
            .OnDelete(DeleteBehavior.Cascade);

        // Skill - Resume (many skills to one resume)
        builder.Entity<Skill>()
            .HasOne(sk => sk.resume)
            .WithMany(r => r.skills)
            .HasForeignKey(sk => sk.resumeId)
            .OnDelete(DeleteBehavior.Cascade);

        // Project - Service (many projects to one service)
        builder.Entity<Project>()
            .HasOne(p => p.service)
            .WithMany(s => s.projects)
            .HasForeignKey(p => p.serviceId)
            .OnDelete(DeleteBehavior.Cascade);

        // Project - Portfolio (many projects to one portfolio)
        builder.Entity<Project>()
            .HasOne(p => p.portfolio)
            .WithMany(pf => pf.projects)
            .HasForeignKey(p => p.portfolioId)
            .OnDelete(DeleteBehavior.NoAction);

        // Service - Portfolio (many services to many portfolio)
        // Configure PortfolioService as a join table
        builder.Entity<PortfolioService>()
            .HasKey(ps => new { ps.portfolioId, ps.serviceId });

        builder.Entity<PortfolioService>()
            .HasOne(ps => ps.portfolio)
            .WithMany(p => p.portfolioServices)
            .HasForeignKey(ps => ps.portfolioId);

        builder.Entity<PortfolioService>()
            .HasOne(ps => ps.service)
            .WithMany(s => s.portfolioServices)
            .HasForeignKey(ps => ps.serviceId);

        // Resume - IsDeleted default false
        builder.Entity<Resume>()
            .Property(r => r.isDeleted)
            .HasDefaultValue(false);

        // Portfolio - IsDeleted default false
        builder.Entity<Portfolio>()
            .Property(p => p.isDeleted)
            .HasDefaultValue(false);

        // Experience -IsCurrent default false 
        builder.Entity<Experience>()
            .Property(e => e.isCurrent)
            .HasDefaultValue(false);
    }


}
