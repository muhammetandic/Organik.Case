namespace Organik.Case.Domain.Common
{
    public abstract class BaseEntity : IEntity, IAuditableEntity
	{
		public uint  Id { get; set; }
        public DateTimeOffset Created { get; set; }
        public uint CreatedBy { get; set; }
        public DateTimeOffset? LastModified { get; set; }
        public uint? LastModifiedBy { get; set; }
    }
}

