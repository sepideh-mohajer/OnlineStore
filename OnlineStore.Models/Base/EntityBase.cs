namespace OnlineStore.Models.Base
{
    public class EntityBase
    {
        public int Id { get; set; }
        private bool _isActive = true;
        public bool IsActive
        {
            get => _isActive;
            set => _isActive = (value == true) ? value : false;
        }

        private DateTime _createdOn = DateTime.Now;
        public DateTime CreatedOn
        {
            get => _createdOn;
            set => _createdOn = (value > DateTime.MinValue) ? value : DateTime.Now;
        }
        public int CreatedByUserId { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedByUserId { get; set; }
    }
}
