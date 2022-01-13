using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAppEs.Entity;
using WebAppEs.Models;

namespace WebAppEs.Data
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		: base(options)
		{

		}

		public DbSet<RoleMenuPermission> RoleMenuPermission { get; set; }
		public DbSet<NavigationMenu> NavigationMenu { get; set; }
		public DbSet<MobileRND_SalesChannel> MobileRND_SalesChannel { get; set; }
		public DbSet<MobileRND_Zone> MobileRND_Zone { get; set; }
		public DbSet<MobileRND_CustomerInfo> MobileRND_CustomerInfo { get; set; }
		public DbSet<MobileRND_CourierInformation> MobileRND_CourierInformation { get; set; }
		public DbSet<MobileRND_EmployeeInformation> MobileRND_EmployeeInformation { get; set; }
		public DbSet<MobileRND_Assigned_Customer_TO_TSO> MobileRND_Assigned_Customer_TO_TSO { get; set; }
		public DbSet<MobileRND_Assigned_TSO_TO_ASM> MobileRND_Assigned_TSO_TO_ASM { get; set; }
		public DbSet<MobileRND_Assigned_ASM_TO_DNSM> MobileRND_Assigned_ASM_TO_DNSM { get; set; }
		public DbSet<MobileRND_PaymentType> MobileRND_PaymentType { get; set; }
		public DbSet<MobileRND_Brand> MobileRND_Brand { get; set; }
		public DbSet<MobileRND_Product> MobileRND_Product { get; set; }
		public DbSet<MobileRND_BookingEntry> MobileRND_BookingEntry { get; set; }
		public DbSet<MobileRND_BookingDetailsEntry> MobileRND_BookingDetailsEntry { get; set; }



		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<ApplicationUser>()
				.Property(e => e.Department)
				.HasMaxLength(100);

			builder.Entity<ApplicationUser>()
				.Property(e => e.Name)
				.HasMaxLength(100);

			builder.Entity<ApplicationUser>()
				.Property(e => e.EmployeeID)
				.HasMaxLength(100);

			builder.Entity<RoleMenuPermission>()
			.HasKey(c => new { c.RoleId, c.NavigationMenuId});
			

			base.OnModelCreating(builder);
		}
	}
}