using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Crews.PlanningCenter.Api.Models;

public sealed class ProductDefinition
{
    private readonly string _name;

    public static IEnumerable<ProductDefinition> All =>
    [
        Api,
        Calendar,
        CheckIns,
        Current,
        Giving,
        Groups,
        People,
        Publishing,
        Registrations,
        Services,
        Webhooks
    ];

    public static readonly ProductDefinition Api = new("api");
    public static readonly ProductDefinition Calendar = new("calendar");
    public static readonly ProductDefinition CheckIns = new("check-ins");
    public static readonly ProductDefinition Current = new("current");
    public static readonly ProductDefinition Giving = new("giving");
    public static readonly ProductDefinition Groups = new("groups");
    public static readonly ProductDefinition People = new("people");
    public static readonly ProductDefinition Publishing = new("publishing");
    public static readonly ProductDefinition Registrations = new("registrations");
    public static readonly ProductDefinition Services = new("services");
    public static readonly ProductDefinition Webhooks = new("webhooks");

    private ProductDefinition(string name) => _name = name;

    public override string ToString() => _name;

    [ExcludeFromCodeCoverage]
    public static implicit operator string(ProductDefinition productDefinition) => productDefinition._name;
}
