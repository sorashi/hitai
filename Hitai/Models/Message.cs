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

        /// <summary>
        ///     Initialization vector
        /// </summary>
        [Key("iv")]
        public byte[] Iv { get; set; }

        /// <summary>
        ///     Aes encrypted content of the message
        /// </summary>
        [Key("message")]
        public byte[] Content { get; set; }
    }
}
