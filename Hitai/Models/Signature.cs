using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MessagePack;

namespace Hitai.Models
{
    [MessagePackObject()]
    public class Signature
    {
        /// <summary>
        /// The <see cref="AsymmetricEncryption.KeyPair.ShortId"/> of the signature's author
        /// </summary>
        [Key(0)]
        public string AuthorId { get; set; }
        /// <summary>
        /// The actual signature data created by signing the <see cref="Hash"/>
        /// </summary>
        [Key(1)]
        public byte[] SignatureData { get; set; }
        /// <summary>
        /// SHA256 of the <see cref="Data"/>
        /// </summary>
        [IgnoreMember]
        public byte[] Hash { get; set; }

        /// <summary>
        /// The encrypted <see cref="Models.Message"/> 
        /// </summary>
        private Message _message;
        /// <summary>
        /// The original signed data
        /// </summary>
        [Key(2)]
        public byte[] Data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns><see cref="Hitai.Models.Message"/> if there is some inside <see cref="Data"/>, otherwise <c>null</c>.</returns>
        public Message GetMessage() {
            if (_message != null) return _message;
            try {
                _message = LZ4MessagePackSerializer.Deserialize<Message>(Data);
            }
            catch {
                // ignored
            }
            return _message;
        }
        /// <summary>
        /// Checks whether <see cref="Data"/> contain a <see cref="Models.Message"/>
        /// </summary>
        /// <returns></returns>
        public bool ContainsMessage() => GetMessage() != null;
        /// <summary>
        /// Converts <see cref="Data"/> to <see cref="string"/>.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"><see cref="Data"/> is a <see cref="Models.Message"/>.</exception>
        public string GetCleartext() {
            if(ContainsMessage()) throw new InvalidOperationException("The signature does not contain cleartext");
            return Encoding.UTF8.GetString(Data);
        }
        /// <summary>
        /// Checks whether <see cref="Hash"/> is correct SHA256 of <see cref="Data"/>.
        /// </summary>
        /// <returns></returns>
        public bool CheckHash() {
            var sha = SHA256.Create();
            return Hash.SequenceEqual(sha.ComputeHash(Data));
        }
        /// <summary>
        /// Computes SHA256 from <see cref="Data"/> and saves it to <see cref="Hash"/>.
        /// </summary>
        public void ComputeHash() {
            var sha = SHA256.Create();
            Hash = sha.ComputeHash(Data);
        }
    }
}
