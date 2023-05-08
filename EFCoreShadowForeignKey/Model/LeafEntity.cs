namespace EFCoreShadowForeignKey.Model
{
    public class LeafEntity : BaseEntity
    {
        // Having non-shadow property for foreign key makes it work!
        //public int RootId { get; set; }
        public RootEntity Root { get; set; }
    }
}
