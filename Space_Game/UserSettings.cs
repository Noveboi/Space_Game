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
    internal class UserSettings
    {
        public Color BulletColor { get; set; }
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

    }
}
