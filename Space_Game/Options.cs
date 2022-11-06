using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.IO;

namespace Space_Game
{
    public partial class Options : Form
    {
        string jsonPath = "../../settings.json";

        #region Methods and Objects
        /// <summary>
        /// Read settings.json and assign proper CheckStates to each checkBox
        /// </summary>
        private UserSettings loadSettings()
        {
            string json = File.ReadAllText(jsonPath);
            UserSettings userSettings = JsonSerializer.Deserialize<UserSettings>(json);
            checkBox1.CheckState = userSettings.SoundSet.Equals("on") ? CheckState.Checked : CheckState.Unchecked;
            checkBox2.CheckState = userSettings.MusicSet.Equals("on") ? CheckState.Checked : CheckState.Unchecked;
            return userSettings;
        }

        /// <summary>
        /// Set userSettings based on CheckState of sender as CheckBox
        /// </summary>
        /// <param name="sender">The CheckBox control that triggers the Check event</param>
        /// <param name="userSettings">UserSettings reference to modify</param>
        private void setSettings(CheckBox sender, UserSettings userSettings)
        {
            if (sender.Equals(checkBox1))
            {
                userSettings.SoundSet = sender.Checked ? "on" : "off";
            }
            else if (sender.Equals(checkBox2))
            {
                userSettings.MusicSet = sender.Checked ? "on" : "off";
            }

            string json = JsonSerializer.Serialize(userSettings);
            File.WriteAllText(jsonPath, json);
        }

        /// <summary>
        /// Object to be interpreted by JSON
        /// </summary>
        private class UserSettings
        {
            public string SoundSet { get; set; }
            public string MusicSet { get; set; }
        }
        #endregion

        //Have an instance of userSettings to pass to setSettings() when Check is fired and
        //to modify when loadSettings() is called
        UserSettings userSettings;

        public Options()
        {
            InitializeComponent();

            //Prevent resizing
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
        }

        private void Options_Load(object sender, EventArgs e)
        {
            userSettings = loadSettings();
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            setSettings(sender as CheckBox, userSettings);
        }
    }

}
