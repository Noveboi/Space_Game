using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Space_Game
{
    [Serializable]
    internal class UserControls
    {
        public Keys ExitGame { get; set; }
        public Keys PauseGame { get; set; }
        public Keys Shoot { get; set; }

        public void GrabFromJson()
        {
            string rawJson = File.ReadAllText("../../userControls.json");
            var data = JsonSerializer.Deserialize<UserControls>(rawJson);
            ExitGame = data.ExitGame;
            PauseGame = data.PauseGame;
            Shoot = data.Shoot;
        }

        public void SaveToJson()
        {
            File.WriteAllText("../../userControls.json", JsonSerializer.Serialize(this));
        }

        public void Sync()
        {
            SaveToJson();
            GrabFromJson();
        }
    }
}
