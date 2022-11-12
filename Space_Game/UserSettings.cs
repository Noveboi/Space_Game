using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace Space_Game
{
    /// <summary>
    /// Provides C# to JSON interaction for the game settings that can be changed by the user
    /// </summary>
    [Serializable]
    internal class UserSettings
    {
        public string BulletColor { get; set; }
        public int GameTime { get; set; }
        public int EnemyDifficulty { get; set; }

        public void GrabFromJson()
        {
            string rawJson = File.ReadAllText("../../settings.json");
            var data = JsonSerializer.Deserialize<UserSettings>(rawJson);

            BulletColor = data.BulletColor;
            GameTime = data.GameTime;
            EnemyDifficulty = data.EnemyDifficulty;
        }

        public void SaveToJson()
        {
            File.WriteAllText("../../settings.json",JsonSerializer.Serialize(this));
        }

        public void Sync()
        {
            SaveToJson();
            GrabFromJson();
        }

    }
}
