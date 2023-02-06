using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApp.DataAccess.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        
        public int SenderId { get; set; }
        public User? Sender { get; set; }
        public int RecipientId { get; set; }
        public User? Recipient { get; set; }
       
    }

    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            
            builder.HasKey(x => new { x.SenderId, x.RecipientId});
            builder.HasOne(x => x.Sender).WithMany(x => x.SendedMessages);
            builder.HasOne(x =>x.Recipient).WithMany(x =>x.ReceivedMessages);

        }
    }


}
