namespace EFCoreShadowForeignKey.Model
{
    public class RelatedEntity
    {
        public Guid Id { get; set; }
        public ICollection<BaseEntity> Entities { get; set; }
    }
}
