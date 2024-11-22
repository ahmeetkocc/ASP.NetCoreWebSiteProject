﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Data.DataEntities;

namespace Data.DataContext.EntityConfigurations
{
	public class SkillsConfigurations : IEntityTypeConfiguration<Skill>
	{
		public void Configure(EntityTypeBuilder<Skill> builder)
		{
			builder.HasKey(s => s.Id);
			builder.Property(s => s.Id).ValueGeneratedOnAdd();

			builder.Property(s => s.Name)
				.IsRequired();

		}
	}
}