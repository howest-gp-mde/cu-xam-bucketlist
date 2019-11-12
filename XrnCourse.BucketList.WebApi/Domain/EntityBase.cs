namespace XrnCourse.BucketList.WebApi.Domain
{
    public abstract class EntityBase<TKey>
    {
        public TKey Id { get; set; }
    }
}
