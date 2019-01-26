using System;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Hitai.ArmorProviders;
using Hitai.SymmetricEncryption;
using MessagePack;

namespace Hitai.AsymmetricEncryption
{
    [MessagePackObject]
    public class Keypair
    {
        [IgnoreMember] public BigInteger Id => Modulus;

        [IgnoreMember]
        public string ShortId {
            get {
                string stringModulus = Id.ToString();
                return stringModulus.Substring(stringModulus.Length - 8);
            }
        }

        [IgnoreMember] public bool IsPrivate => PrivateExponent != null;

        public byte[] ToMessagePack() {
            return LZ4MessagePackSerializer.Serialize(this);
        }

        public static Keypair FromMessagePack(byte[] msgPack) {
            return LZ4MessagePackSerializer.Deserialize<Keypair>(msgPack);
        }

        public static async Task<Keypair> LoadAsync(string path) {
            // TODO: async file reading
            byte[] contents = File.ReadAllBytes(path);
            await Task.FromResult(0);
            Type armorProviderType = ArmorRecognizer.RecognizeArmor(contents);
            var armorProvider = (IArmorProvider) Activator.CreateInstance(armorProviderType);
            ArmorType armorType = armorProvider.GetArmorType(contents);
            if (armorType != ArmorType.PublicKey && armorType != ArmorType.PrivateKey)
                throw new InvalidOperationException(
                    "Armor není správného typu (neobsahuje ani veřejný, ani soukromý klíč)");
            byte[] rawData = armorProvider.FromArmor(contents).rawData;
            var keyPair = LZ4MessagePackSerializer.Deserialize<Keypair>(rawData);
            return keyPair;
        }

        public async Task SaveAsync(string path, IArmorProvider armorProvider) {
            await Task.FromResult(0);
            byte[] rawData = LZ4MessagePackSerializer.Serialize(this);
            var armorType = ArmorType.PublicKey;
            if (IsPrivate) armorType = ArmorType.PrivateKey;
            byte[] contents = armorProvider.ToArmor(rawData, armorType);
            // TODO: async file writing
            File.WriteAllBytes(path, contents);
        }

        /// <summary>
        ///     Encrypts and saves the <see cref="PrivateExponent" />
        /// </summary>
        /// <param name="data"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task SetPrivateExponentAsync(byte[] data, string password) {
            var aes = new AesSymmetricEncryptionProvider();
            aes.GenerateKeyFromPassword(password);
            SHA256 sha = SHA256.Create();
            byte[] hash =
                sha.ComputeHash(Encoding.UTF8.GetBytes(password).Concat(aes.Salt).ToArray());
            PasswordHash = hash;
            Salt = aes.Salt;
            Iv = aes.Iv;
            PrivateExponent = await aes.TransformAsync(data);
        }

        public async Task<byte[]> GetPrivateExponentAsync(string password) {
            if (!IsPrivate)
                throw new InvalidOperationException("Not a private keypair");
            var aes = new AesSymmetricEncryptionProvider();
            if (!CheckPassword(password))
                throw new CryptographicException("Invalid password");
            aes.GenerateKeyFromPassword(password, Salt);
            aes.Iv = Iv;
            return await aes.TransformAsync(PrivateExponent, false);
        }

        public bool CheckPassword(string password) {
            SHA256 sha = SHA256.Create();
            byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(password).Concat(Salt).ToArray());
            return PasswordHash.SequenceEqual(hash);
        }

        public override bool Equals(object obj) {
            if (obj is Keypair keyPair)
                return Equals(keyPair);
            return base.Equals(obj);
        }

        /// <summary>
        ///     Strips this keypair from private data
        /// </summary>
        public void Strip() {
            PrivateExponent = null;
            GC.Collect();
        }

        public Keypair ToPublic() {
            return new Keypair {
                Exponent = Exponent,
                Modulus = Modulus,
                CreationTime = CreationTime,
                Expires = Expires,
                UserId = UserId,
                LastEdited = LastEdited,
                RsaProvider = RsaProvider
            };
        }

        public bool Equals(Keypair other) {
            return other.Modulus.Equals(Modulus);
        }

        public override int GetHashCode() {
            return Modulus.GetHashCode();
        }

        #region Properties

        [Key(0)] public string UserId { get; set; }

        [Key(1)] public BigInteger Modulus { get; set; }

        [Key(2)] public byte[] PrivateExponent { get; set; }

        [Key(3)] public BigInteger Exponent { get; set; }

        [Key(4)] public byte[] PasswordHash { get; set; }

        [Key(5)] public byte[] Salt { get; set; }

        /// <summary>
        ///     Initialization vector
        /// </summary>
        [Key(6)]
        public byte[] Iv { get; set; }

        [Key(7)] public DateTime CreationTime { get; set; }

        [Key(8)] public DateTime LastEdited { get; set; }

        [Key(9)] public DateTime Expires { get; set; }

        /// <summary>
        ///     Index of the RsaProvider from <see cref="AsymmetricEncryptionController.ProviderList" />
        /// </summary>
        [Key(10)]
        public int RsaProvider { get; set; }

        #endregion Properties

        // TODO: add signatures
    }
}
