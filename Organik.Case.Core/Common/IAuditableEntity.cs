namespace Organik.Case.Domain.Common
{
    public interface IAuditableEntity
	{
		DateTimeOffset Created { get; set; }
		uint  CreatedBy { get; set; }
		DateTimeOffset? LastModified { get; set; }
		uint? LastModifiedBy { get; set; }
	}
}

