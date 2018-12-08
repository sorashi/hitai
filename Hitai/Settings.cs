using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Hitai
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Settings
    {
        public static readonly string SavePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "Hitai\\settings.json");

        [JsonProperty("keychainFolder", DefaultValueHandling = DefaultValueHandling.Include)]
        public static string KeychainFolder { get; set; } = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "Hitai\\keychain\\");

        /// <summary>
        ///     Loads the settings file from the default path
        /// </summary>
        /// <param name="loadNew">
        ///     Load a new save if there is no save found. Otherwise throws a
        ///     <see cref="FileNotFoundException" />
        /// </param>
        /// <returns></returns>
        public static async Task<Settings> LoadAsync(bool loadNew = true) {
            if (!SaveExists()) {
                if (!loadNew) throw new FileNotFoundException("Settings file not found");
                var settings = new Settings();
                await settings.SaveAsync();
                return settings;
            }

            using (var sr = new StreamReader(SavePath)) {
                string contents = await sr.ReadToEndAsync();
                return JsonConvert.DeserializeObject<Settings>(contents);
            }
        }

        public async Task SaveAsync() {
            using (var sw = new StreamWriter(SavePath)) {
                string json = JsonConvert.SerializeObject(this, Formatting.Indented);
                await sw.WriteAsync(json);
            }
        }

        public static bool SaveExists() {
            return File.Exists(SavePath);
        }
    }
}
