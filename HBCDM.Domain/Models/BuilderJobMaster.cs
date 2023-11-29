using System;
using System.Collections.Generic;

namespace HBCDM.Domain.Models;

public partial class BuilderJobMaster
{
    public string BuilderId { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public string JobPhase { get; set; } = null!;

    public string JobNo { get; set; } = null!;

    public string? ProjectName { get; set; }

    public string? JobAid { get; set; }

    public string? JobAddressStreet1 { get; set; }

    public string? JobAddressStreet2 { get; set; }

    public string? JobAddressCity { get; set; }

    public string? JobAddressStateProv { get; set; }

    public string? JobAddressZipPostal { get; set; }

    public string? JobAddressCountry { get; set; }

    public string? JobAddressCountryCode { get; set; }

    public string? PlanName { get; set; }

    public string? PlanElevId { get; set; }

    public string? ElevationName { get; set; }

    public string JobType { get; set; } = null!;

    public string? JobBuildType { get; set; }

    public string JobSalesStatus { get; set; } = null!;

    public string JobConstructionStatus { get; set; } = null!;

    public bool Model { get; set; }

    public bool? QuickMoveIn { get; set; }

    public decimal? QmipublishedPrice { get; set; }

    public decimal? LotPremium { get; set; }

    public string? Swing { get; set; }

    public short? Area { get; set; }

    public decimal? Bedrooms { get; set; }

    public decimal? Baths { get; set; }

    public decimal? Garages { get; set; }

    public string? Style { get; set; }

    public bool? Sellable { get; set; }

    public DateTime? ReleasedToSaleDate { get; set; }

    public decimal? TotalContract { get; set; }

    public DateTime? OfferDate { get; set; }

    public string? OfferType { get; set; }

    public DateTime? ContractDate { get; set; }

    public DateTime? TargetClosing { get; set; }

    public DateTime? RevisedClosing { get; set; }

    public DateTime? ActualDateClosed { get; set; }

    public DateTime? ReleasedToConstrDate { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public DateTime? PlanStartDate { get; set; }

    public DateTime? PlanEndDate { get; set; }

    public DateTime? ActualStartDate { get; set; }

    public DateTime? ActualEndDate { get; set; }

    public bool? OnHold { get; set; }

    public string? OnHoldReasonCode { get; set; }

    public DateTime? OnHoldStartDate { get; set; }

    public DateTime? OnHoldEstReleaseDate { get; set; }

    public DateTime? WarrantyEffDate { get; set; }

    public DateTime? WarrantyExpDate { get; set; }

    public int CheckSum { get; set; }

    public DateTime UpdatedOn { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;
}
