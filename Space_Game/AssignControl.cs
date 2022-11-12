using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Space_Game
{
    /// <summary>
    /// Simple pop-up window that allows the user to press any valid key and and assign
    /// that key to the specified control (controlFunction)
    /// </summary>
    public partial class AssignControl : Form
    {
        string controlFunction;
        UserControls controls = new UserControls();
        Keys lastKeyPressed;

        public AssignControl(string ControlFunction)
        {
            InitializeComponent();
            controlFunction = ControlFunction;
            label2.Text = ControlFunction;
            label1.Text = "Press any valid key\nPress Enter to assign key";

            CheckForInvalidControls();
            controls.GrabFromJson();
        }
        /// <summary>
        /// Revert any control to default settings IF control has an invalid key (KeyCode == 0)
        /// </summary>
        private void CheckForInvalidControls()
        {
            if (controls.Shoot == Keys.None) controls.Shoot = Keys.Space;
            if (controls.PauseGame == Keys.None) controls.PauseGame = Keys.Escape;
            if (controls.ExitGame == Keys.None) controls.ExitGame = Keys.E;
        }

        private void AssignControl_KeyDown(object sender, KeyEventArgs e)
        {
            //Add to controls.json the new key value and close this form.
            if(e.KeyCode == Keys.Enter)
            {
                if (controlFunction == "ExitGame") 
                { 
                    controls.ExitGame = lastKeyPressed;
                    CheckForInvalidControls();
                    controls.SaveToJson(); 
                    Close(); 
                }
                if (controlFunction == "PauseGame")
                {
                    controls.PauseGame = lastKeyPressed;
                    CheckForInvalidControls();
                    controls.SaveToJson();
                    Close();
                }
                if (controlFunction == "Shoot")
                {
                    controls.Shoot = lastKeyPressed;
                    CheckForInvalidControls();
                    controls.SaveToJson();
                    Close();
                }
            }

            lastKeyPressed = e.KeyCode;
            label2.Text = lastKeyPressed.ToString();
        }
    }
}
