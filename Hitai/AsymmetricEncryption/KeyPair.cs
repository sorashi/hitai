using Hitai.ArmorProviders;
using Hitai.SymmetricEncryption;
using MessagePack;
using System;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Hitai.AsymmetricEncryption
{
    [MessagePackObject]
    public class KeyPair
    {
        public KeyPair() {
        }

        [IgnoreMember]
        public BigInteger Id => Modulus;

        [IgnoreMember]
        public string ShortId {
            get {
                var stringModulus = Id.ToString();
                return stringModulus.Substring(stringModulus.Length - 8);
            }
        }

        [IgnoreMember]
        public bool IsPrivate => PrivateExponent != null;

        #region Properties

        [Key(0)]
        public string UserId { get; set; }

        [Key(1)]
        public BigInteger Modulus { get; set; }

        [Key(2)]
        public byte[] PrivateExponent { get; set; }

        [Key(3)]
        public BigInteger Exponent { get; set; }

        [Key(4)]
        public byte[] PasswordHash { get; set; }

        [Key(5)]
        public byte[] Salt { get; set; }

        [Key(6)]
        public byte[] IV { get; set; }

        [Key(7)]
        public DateTime CreationTime { get; set; }

        [Key(8)]
        public DateTime LastEdited { get; set; }

        [Key(9)]
        public DateTime Expires { get; set; }

        /// <summary>
        /// Index of the RsaProvider from <see cref="AsymmetricEncryptionController.ProviderList"/>
        /// </summary>
        [Key(10)]
        public int RsaProvider { get; set; }

        #endregion Properties

        public byte[] ToMessagePack() {
            return LZ4MessagePackSerializer.Serialize(this);
        }

        public static KeyPair FromMessagePack(byte[] msgPack) {
            return LZ4MessagePackSerializer.Deserialize<KeyPair>(msgPack);
        }

        public static async Task<KeyPair> LoadAsync(string path) {
            // TODO: async file reading
            var contents = File.ReadAllBytes(path);
            await Task.FromResult(0);
            var armorProviderType = ArmorRecognizer.RecognizeArmor(contents);
            var armorProvider = (IArmorProvider)Activator.CreateInstance(armorProviderType);
            var armorType = armorProvider.GetArmorType(contents);
            if (armorType != ArmorType.PublicKey && armorType != ArmorType.PrivateKey)
                throw new InvalidOperationException("Armor neni spravneho typu (neobsahuje ani verejny, ani soukromy klic)");
            var rawData = armorProvider.FromArmor(contents).rawData;
            var keyPair = LZ4MessagePackSerializer.Deserialize<KeyPair>(rawData);
            return keyPair;
        }

        public async Task SaveAsync(string path, IArmorProvider armorProvider) {
            await Task.FromResult(0);
            var rawData = LZ4MessagePackSerializer.Serialize(this);
            var armorType = ArmorType.PublicKey;
            if (IsPrivate) armorType = ArmorType.PrivateKey;
            var contents = armorProvider.ToArmor(rawData, armorType);
            // TODO: async file writing
            File.WriteAllBytes(path, contents);
        }
        /// <summary>
        /// Encrypts and saves the <see cref="PrivateExponent"/>
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task SetPrivateExponentAsync(byte[] data, string password) {
            var aes = new AesSymmetricEncryptionProvider();
            aes.GenerateKeyFromPassword(password);
            var sha = SHA256.Create();
            var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(password).Concat(aes.Salt).ToArray());
            PasswordHash = hash;
            Salt = aes.Salt;
            IV = aes.IV;
            PrivateExponent = await aes.TransformAsync(data);
        }

        public async Task<byte[]> GetPrivateExponentAsync(string password) {
            if (!IsPrivate)
                throw new InvalidOperationException("Not a private keypair");
            var aes = new AesSymmetricEncryptionProvider();
            if (!CheckPassword(password))
                throw new CryptographicException("Invalid password");
            aes.GenerateKeyFromPassword(password, Salt);
            aes.IV = IV;
            return await aes.TransformAsync(PrivateExponent, false);
        }

        public bool CheckPassword(string password) {
            var sha = SHA256.Create();
            var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(password).Concat(Salt).ToArray());
            return Enumerable.SequenceEqual(PasswordHash, hash);
        }
        public override bool Equals(object obj) {
            if (obj as KeyPair != null)
                return Equals(obj as KeyPair);
            return base.Equals(obj);
        }
        /// <summary>
        /// Strips this keypair from private data
        /// </summary>
        public void Strip() {
            this.PrivateExponent = null;
            GC.Collect();
        }
        public KeyPair ToPublic() {
            return new KeyPair() {
                Exponent = Exponent,
                Modulus = Modulus
            };
        }

        public bool Equals(KeyPair other) => other.Modulus.Equals(Modulus);

        public override int GetHashCode() => Modulus.GetHashCode();

        // TODO: add signatures
    }
}