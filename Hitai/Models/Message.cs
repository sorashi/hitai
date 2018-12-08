using MessagePack;

namespace Hitai.Models
{
    /// <summary>
    ///     Represents and encrypted message directed to a recipient
    /// </summary>
    [MessagePackObject]
    public class Message
    {
        /// <summary>
        ///     The short Id of the recipient
        /// </summary>
        [Key("id")]
        public string RecipientId { get; set; }

        [Key("key")] public byte[] EncryptedKey { get; set; }

        [Key("iv")] public byte[] IV { get; set; }

        /// <summary>
        ///     Aes encrypted content of the message
        /// </summary>
        [Key("message")]
        public byte[] Content { get; set; }
    }
}
