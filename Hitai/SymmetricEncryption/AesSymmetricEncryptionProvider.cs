using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;
using MessagePack;

namespace Hitai.SymmetricEncryption
{
    [MessagePackObject]
    public class AesSymmetricEncryptionProvider
    {
        private readonly Aes _aes = Aes.Create();

        /// <summary>
        ///     Initialization vector
        /// </summary>
        [Key(0)]
        public byte[] Iv {
            get => _aes.IV;
            set => _aes.IV = value;
        }

        [Key(1)]
        public byte[] Key {
            get => _aes.Key;
            set => _aes.Key = value;
        }

        [Key(2)] public byte[] Salt { get; private set; }

        public void NewIv() {
            _aes.GenerateIV();
        }

        public void GenerateKey() {
            _aes.GenerateKey();
        }

        public void GenerateKeyFromPassword(string password, int saltSize = 16) {
            var pbkdf = new Rfc2898DeriveBytes(password, saltSize);
            Key = pbkdf.GetBytes(_aes.KeySize / 8);
            Salt = pbkdf.Salt;
        }

        public void GenerateKeyFromPassword(string password, byte[] salt) {
            var pbkdf = new Rfc2898DeriveBytes(password, salt);
            Key = pbkdf.GetBytes(_aes.KeySize / 8);
            Salt = pbkdf.Salt;
        }

        /// <summary>
        ///     Encrypts or decrypts <paramref name="data" />
        /// </summary>
        /// <param name="data"></param>
        /// <param name="encrypt">True for encrypt, false for decrypt</param>
        /// <returns></returns>
        public async Task<byte[]> TransformAsync(byte[] data, bool encrypt = true) {
            ICryptoTransform transform = encrypt ? _aes.CreateEncryptor() : _aes.CreateDecryptor();
            using (var ms = new MemoryStream()) {
                using (var cs = new CryptoStream(ms, transform, CryptoStreamMode.Write)) {
                    // TODO figure out why this stops when being async
                    cs.Write(data, 0, data.Length);
                    await cs.FlushAsync();
                }

                await ms.FlushAsync();
                return ms.ToArray();
            }
        }
    }
}
