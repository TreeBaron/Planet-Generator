using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Planet_Generator
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void GenerateTextureButton_Click(object sender, EventArgs e)
        {
            int resolution = -1;
            try
            {
                resolution = Convert.ToInt32(ResolutionTextBox.Text);
            }
            catch
            {
                MessageBox.Show("Enter a valid resolution.");
                return;
            }

            PictureBox.Image = (Image)TextureGen.GeneratePlanet(resolution);
            PlanetNameLabel.Text = PlanetNames.GetRandomPlanetName();
        }
    }
}
