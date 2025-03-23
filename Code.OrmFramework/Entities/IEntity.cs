namespace Code.OrmFramework.Entities
{
    public abstract class IEntity<TId> : ISoftDelete, IHasId<TId>
    {
        public required TId Id { get; set; }
        public bool IsDeleted { get; set; }
    }



}
