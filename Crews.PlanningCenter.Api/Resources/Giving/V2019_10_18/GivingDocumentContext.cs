/*
=======================================================================
This code is automatically generated. Please do not modify it directly.
=======================================================================
*/

using Crews.PlanningCenter.Models.Giving.V2019_10_18.Entities;
using JsonApiFramework.ServiceModel.Configuration;

namespace Crews.PlanningCenter.Api.Resources.Giving.V2019_10_18;

/// <summary>
/// JSON API document context for the Planning Center Giving API.
/// </summary>
public class GivingDocumentContext : PlanningCenterDocumentContext
{
  internal GivingDocumentContext(JsonApiFramework.JsonApi.Document document) : base(document) { }
  internal GivingDocumentContext() : base() { }

  /// <inheritdoc />
  protected override void OnServiceModelCreating(IServiceModelBuilder builder)
  {
    base.OnServiceModelCreating(builder);
  
    builder.Configurations.Add(new BatchConfiguration());
    builder.Configurations.Add(new BatchGroupConfiguration());
    builder.Configurations.Add(new CampusConfiguration());
    builder.Configurations.Add(new DesignationConfiguration());
    builder.Configurations.Add(new DesignationRefundConfiguration());
    builder.Configurations.Add(new DonationConfiguration());
    builder.Configurations.Add(new FundConfiguration());
    builder.Configurations.Add(new InKindDonationConfiguration());
    builder.Configurations.Add(new LabelConfiguration());
    builder.Configurations.Add(new NoteConfiguration());
    builder.Configurations.Add(new OrganizationConfiguration());
    builder.Configurations.Add(new PaymentMethodConfiguration());
    builder.Configurations.Add(new PaymentSourceConfiguration());
    builder.Configurations.Add(new PersonConfiguration());
    builder.Configurations.Add(new PledgeConfiguration());
    builder.Configurations.Add(new PledgeCampaignConfiguration());
    builder.Configurations.Add(new RecurringDonationConfiguration());
    builder.Configurations.Add(new RecurringDonationDesignationConfiguration());
    builder.Configurations.Add(new RefundConfiguration());
  }

  internal class BatchConfiguration : ResourceTypeBuilder<Batch> { public BatchConfiguration() { } }
  internal class BatchGroupConfiguration : ResourceTypeBuilder<BatchGroup> { public BatchGroupConfiguration() { } }
  internal class CampusConfiguration : ResourceTypeBuilder<Campus> { public CampusConfiguration() { } }
  internal class DesignationConfiguration : ResourceTypeBuilder<Designation> { public DesignationConfiguration() { } }
  internal class DesignationRefundConfiguration : ResourceTypeBuilder<DesignationRefund> { public DesignationRefundConfiguration() { } }
  internal class DonationConfiguration : ResourceTypeBuilder<Donation> { public DonationConfiguration() { } }
  internal class FundConfiguration : ResourceTypeBuilder<Fund> { public FundConfiguration() { } }
  internal class InKindDonationConfiguration : ResourceTypeBuilder<InKindDonation> { public InKindDonationConfiguration() { } }
  internal class LabelConfiguration : ResourceTypeBuilder<Label> { public LabelConfiguration() { } }
  internal class NoteConfiguration : ResourceTypeBuilder<Note> { public NoteConfiguration() { } }
  internal class OrganizationConfiguration : ResourceTypeBuilder<Organization> { public OrganizationConfiguration() { } }
  internal class PaymentMethodConfiguration : ResourceTypeBuilder<PaymentMethod> { public PaymentMethodConfiguration() { } }
  internal class PaymentSourceConfiguration : ResourceTypeBuilder<PaymentSource> { public PaymentSourceConfiguration() { } }
  internal class PersonConfiguration : ResourceTypeBuilder<Person> { public PersonConfiguration() { } }
  internal class PledgeConfiguration : ResourceTypeBuilder<Pledge> { public PledgeConfiguration() { } }
  internal class PledgeCampaignConfiguration : ResourceTypeBuilder<PledgeCampaign> { public PledgeCampaignConfiguration() { } }
  internal class RecurringDonationConfiguration : ResourceTypeBuilder<RecurringDonation> { public RecurringDonationConfiguration() { } }
  internal class RecurringDonationDesignationConfiguration : ResourceTypeBuilder<RecurringDonationDesignation> { public RecurringDonationDesignationConfiguration() { } }
  internal class RefundConfiguration : ResourceTypeBuilder<Refund> { public RefundConfiguration() { } }
}

