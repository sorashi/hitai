using MessagePack;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Hitai.SymmetricEncryption
{
    [MessagePackObject]
    public class AesSymmetricEncryptionProvider
    {
        private readonly Aes Aes = Aes.Create();

        [Key(0)]
        public byte[] IV {
            get => Aes.IV;
            set => Aes.IV = value;
        }

        [Key(1)]
        public byte[] Key {
            get => Aes.Key;
            set => Aes.Key = value;
        }

        [Key(2)]
        public byte[] Salt { get; private set; } = null;

        public void NewIV() => Aes.GenerateIV();

        public void GenerateKey() => Aes.GenerateKey();

        public void GenerateKeyFromPassword(string password, int saltSize = 16) {
            var pbkdf = new Rfc2898DeriveBytes(password, saltSize);
            Key = pbkdf.GetBytes(Aes.KeySize / 8);
            Salt = pbkdf.Salt;
        }

        public void GenerateKeyFromPassword(string password, byte[] salt) {
            var pbkdf = new Rfc2898DeriveBytes(password, salt);
            Key = pbkdf.GetBytes(Aes.KeySize / 8);
            Salt = pbkdf.Salt;
        }

        /// <summary>
        /// Encrypts or decrypts <paramref name="data"/>
        /// </summary>
        /// <param name="data"></param>
        /// <param name="encrypt">True for encrypt, false for decrypt</param>
        /// <returns></returns>
        public async Task<byte[]> TransformAsync(byte[] data, bool encrypt = true) {
            ICryptoTransform transform;
            if (encrypt) transform = Aes.CreateEncryptor();
            else transform = Aes.CreateDecryptor();
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