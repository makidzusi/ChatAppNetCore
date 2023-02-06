using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.DataAccess.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Message> SendedMessages { get; set; }
        public List<Message> ReceivedMessages { get; set; }
    }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Email).IsRequired();
            builder.HasIndex(x => x.Email).IsUnique(true);  
            builder.Property(x => x.Password).IsRequired();
            builder.Property(x => x.Name).IsRequired();
        }
    }
}
