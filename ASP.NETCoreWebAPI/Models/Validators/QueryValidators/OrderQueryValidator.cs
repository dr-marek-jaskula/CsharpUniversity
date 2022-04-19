using ASP.NETCoreWebAPI.Models.DataTransferObjects;
using ASP.NETCoreWebAPI.Models.QueryObjects;
using FluentValidation;

namespace ASP.NETCoreWebAPI.Models.Validators;

//OrderQueryValidator determines the pagination parameters like PageSizes and Columns that can be used for sorting

public class OrderQueryValidator : AbstractValidator<OrderQuery>
{
    private readonly int[] _allowedPageSizes = new[] { 5, 10, 15 };
    private readonly string[] _allowedSortByColumnNames = { nameof(OrderDto.Product.Name), nameof(OrderDto.Amount) }; //Can be more

    //Validation for OrderQuery
    public OrderQueryValidator()
    {
        RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1);

        RuleFor(r => r.PageSize).Custom((value, context) =>
        {
            if (!_allowedPageSizes.Contains(value))
                context.AddFailure("PageSize", $"PageSize must be in [{string.Join(",", _allowedPageSizes)}]");
        });

        RuleFor(r => r.SortBy)
            .Must(value => string.IsNullOrEmpty(value) || _allowedSortByColumnNames.Contains(value))
            .WithMessage($"Sort by is optional or must be in [{string.Join(",", _allowedSortByColumnNames)}]");
    }
}