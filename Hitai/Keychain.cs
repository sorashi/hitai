using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Hitai.ArmorProviders;
using Hitai.AsymmetricEncryption;

namespace Hitai
{
    public class Keychain
    {
        public delegate void KeypairEventDelegate(KeyPair keypair);

        /// <summary>
        ///     Mutex pro datovou složku Keys
        /// </summary>
        private static readonly SemaphoreSlim KeysSemaphore = new SemaphoreSlim(1, 1);

        private static Keychain _instance;
        public List<KeyPair> Keys = new List<KeyPair>();

        private Keychain() {
        }

        public static async Task<Keychain> GetInstance() {
            return _instance ?? (_instance = await LoadAsync());
        }

        private static async Task<Keychain> LoadAsync() {
            return await LoadAsync(Settings.KeychainFolder);
        }

        private static async Task<Keychain> LoadAsync(string folder) {
            await KeysSemaphore.WaitAsync();
            try {
                var keychain = new Keychain();
                if (!Directory.Exists(folder)) return keychain;
                foreach (string file in Directory.GetFiles(folder))
                    keychain.Keys.Add(await KeyPair.LoadAsync(file));
                return keychain;
            }
            finally {
                KeysSemaphore.Release();
            }
        }

        /// <summary>
        ///     Adds a <paramref name="key" /> into the keychain and also saves it into the keychain folder.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<int> AddKeyPair(KeyPair key) {
            await KeysSemaphore.WaitAsync();
            try {
                Keys.Add(key);
                OnKeypairAdded?.Invoke(key);
                if (!Directory.Exists(Settings.KeychainFolder))
                    Directory.CreateDirectory(Settings.KeychainFolder);
                await key.SaveAsync(Path.Combine(Settings.KeychainFolder, $"{key.ShortId}.hit"),
                    new HitaiArmorProvider());
                return Keys.Count;
            }
            finally {
                KeysSemaphore.Release();
            }
        }

        /// <summary>
        ///     Removes the keypair at <paramref name="index" /> and also removes it from the folder.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public async Task<KeyPair> RemoveKeyPair(int index) {
            await KeysSemaphore.WaitAsync();
            try {
                KeyPair removed = Keys[index];
                await RemoveKeyPair(removed);
                return removed;
            }
            finally {
                KeysSemaphore.Release();
            }
        }

        public async Task RemoveKeyPair(KeyPair key) {
            await LockOperation(async () => {
                if (!Keys.Remove(key)) throw new Exception("Keypair was not found");
                string path = Path.Combine(Settings.KeychainFolder, $"{key.ShortId}.hit");
                if (File.Exists(path))
                    File.Delete(path);
                OnKeypairRemoved?.Invoke(key);
                await Task.FromResult(0);
            });
        }

        private async Task LockOperation(Func<Task> operation) {
            await KeysSemaphore.WaitAsync();
            try {
                await operation();
            }
            finally {
                KeysSemaphore.Release();
            }
        }

        public static event KeypairEventDelegate OnKeypairAdded;
        public static event KeypairEventDelegate OnKeypairRemoved;
    }
}
