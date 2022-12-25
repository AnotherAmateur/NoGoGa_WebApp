using Microsoft.EntityFrameworkCore;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design;

namespace DataLayer
{
	public class EFDBContext : DbContext
	{
		public DbSet<User> User { get; set; }
		public DbSet<Game> Game { get; set; }
		public EFDBContext(DbContextOptions<EFDBContext> options) : base(options) { }
	}

	public class EFDBContextFactory : IDesignTimeDbContextFactory<EFDBContext>
	{
		public EFDBContext CreateDbContext(string[] args)
		{
			var optionBuilder = new DbContextOptionsBuilder<EFDBContext>();
			optionBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=manateeDB;Trusted_Connection=True;MultipleActiveResultSets=true", b => b.MigrationsAssembly("DL"));

			return new EFDBContext(optionBuilder.Options);
		}
	}
}
