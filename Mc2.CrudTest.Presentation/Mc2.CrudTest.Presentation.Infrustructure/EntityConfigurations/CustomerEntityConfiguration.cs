using Mc2.CrudTest.Presentation.Domain.AggregatesModel.CustomerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Presentation.Infrustructure.EntityConfigurations
{
    public class CustomerEntityConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("customers", "customer");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Firstname).HasMaxLength(200).IsRequired();
            builder.Property(m => m.Lastname).HasMaxLength(200).IsRequired();
            builder.Property(m => m.DateOfBirth).IsRequired();

            builder
                .OwnsOne(o => o.PhoneNumber, a =>
                {
                    a.Property(b => b.PhoneNumber).IsRequired().HasColumnName(nameof(PhoneNumberValueObject.PhoneNumber));
                    a.WithOwner();
                });

            builder
               .OwnsOne(o => o.Email, a =>
               {
                   a.Property(b => b.Email).HasMaxLength(200).IsRequired().HasColumnName(nameof(EmailValueObject.Email));
                   a.WithOwner();
                   a.HasIndex(b => b.Email).IsUnique(true);
               });

            builder
             .OwnsOne(o => o.BankAccountNumber, a =>
             {
                 a.Property(b => b.BankAccountNumber).HasMaxLength(200).IsRequired().HasColumnName(nameof(BankAccountNumberValueObject.BankAccountNumber));
                 a.WithOwner();
             });

            builder.Ignore(b => b.DomainEvents);
            builder.HasIndex(b => new { b.DateOfBirth, b.Firstname, b.Lastname }).IsUnique(true);
        }
    }
}
