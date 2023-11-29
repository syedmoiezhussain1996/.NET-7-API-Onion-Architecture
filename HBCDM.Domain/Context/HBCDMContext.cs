using System;
using System.Collections.Generic;
using HBCDM.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HBCDM.Domain.Context;

public partial class HBCDMContext : DbContext
{
    public HBCDMContext()
    {
    }

    public HBCDMContext(DbContextOptions<HBCDMContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BuilderJobMaster> BuilderJobMasters { get; set; }

  
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BuilderJobMaster>(entity =>
        {
            entity.HasKey(e => new { e.BuilderId, e.ProjectId, e.JobNo }).HasName("PK_HBCDM_Builder_JobMaster");

            entity.ToTable("Builder_JobMaster", "HBCDM");

            entity.Property(e => e.BuilderId).HasMaxLength(32);
            entity.Property(e => e.ProjectId).HasMaxLength(32);
            entity.Property(e => e.JobNo).HasMaxLength(8);
            entity.Property(e => e.ActualDateClosed).HasColumnType("date");
            entity.Property(e => e.ActualEndDate).HasColumnType("date");
            entity.Property(e => e.ActualStartDate).HasColumnType("date");
            entity.Property(e => e.Baths).HasColumnType("decimal(4, 2)");
            entity.Property(e => e.Bedrooms).HasColumnType("decimal(4, 2)");
            entity.Property(e => e.ContractDate).HasColumnType("date");
            entity.Property(e => e.CreatedBy).HasMaxLength(128);
            entity.Property(e => e.CreatedOn).HasColumnType("smalldatetime");
            entity.Property(e => e.ElevationName).HasMaxLength(64);
            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.Garages).HasColumnType("decimal(4, 2)");
            entity.Property(e => e.JobAddressCity).HasMaxLength(64);
            entity.Property(e => e.JobAddressCountry).HasMaxLength(64);
            entity.Property(e => e.JobAddressCountryCode).HasMaxLength(8);
            entity.Property(e => e.JobAddressStateProv).HasMaxLength(64);
            entity.Property(e => e.JobAddressStreet1).HasMaxLength(128);
            entity.Property(e => e.JobAddressStreet2).HasMaxLength(128);
            entity.Property(e => e.JobAddressZipPostal).HasMaxLength(16);
            entity.Property(e => e.JobAid)
                .HasMaxLength(16)
                .HasColumnName("JobAId");
            entity.Property(e => e.JobBuildType).HasMaxLength(16);
            entity.Property(e => e.JobConstructionStatus).HasMaxLength(32);
            entity.Property(e => e.JobPhase).HasMaxLength(32);
            entity.Property(e => e.JobSalesStatus).HasMaxLength(32);
            entity.Property(e => e.JobType).HasMaxLength(32);
            entity.Property(e => e.LotPremium).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.OfferDate).HasColumnType("date");
            entity.Property(e => e.OfferType).HasMaxLength(16);
            entity.Property(e => e.OnHoldEstReleaseDate).HasColumnType("date");
            entity.Property(e => e.OnHoldReasonCode).HasMaxLength(32);
            entity.Property(e => e.OnHoldStartDate).HasColumnType("date");
            entity.Property(e => e.PlanElevId).HasMaxLength(32);
            entity.Property(e => e.PlanEndDate).HasColumnType("date");
            entity.Property(e => e.PlanName).HasMaxLength(64);
            entity.Property(e => e.PlanStartDate).HasColumnType("date");
            entity.Property(e => e.ProjectName).HasMaxLength(128);
            entity.Property(e => e.QmipublishedPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("QMIPublishedPrice");
            entity.Property(e => e.ReleasedToConstrDate).HasColumnType("date");
            entity.Property(e => e.ReleasedToSaleDate).HasColumnType("date");
            entity.Property(e => e.RevisedClosing).HasColumnType("date");
            entity.Property(e => e.StartDate).HasColumnType("date");
            entity.Property(e => e.Style).HasMaxLength(64);
            entity.Property(e => e.Swing).HasMaxLength(8);
            entity.Property(e => e.TargetClosing).HasColumnType("date");
            entity.Property(e => e.TotalContract).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UpdatedBy).HasMaxLength(128);
            entity.Property(e => e.UpdatedOn).HasColumnType("smalldatetime");
            entity.Property(e => e.WarrantyEffDate).HasColumnType("date");
            entity.Property(e => e.WarrantyExpDate).HasColumnType("date");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
