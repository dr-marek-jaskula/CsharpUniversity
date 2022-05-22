using EFCore.Data_models.Views;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Data_models.Configurations;

public class EmployeeContactDataEntityTypeConfiguration : IEntityTypeConfiguration<EmployeeContactData>
{
    public void Configure(EntityTypeBuilder<EmployeeContactData> builder)
    {
        //Name of the view needs to be the same as in the database (the view will be create in the database using the migrations)
        builder.ToView("View_EmployeeContactData");

        //If its a view, it has to be pointed out that this entity does not have the key
        builder.HasNoKey();
    }
}