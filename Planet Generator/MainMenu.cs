using System;
using System.Drawing;
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

            int atmosphereThickness = -1;
            try
            {
                atmosphereThickness = Convert.ToInt32(AtmosphereTextBox.Text);
            }
            catch
            {
                MessageBox.Show("Enter a valid atmosphere thickness.");
                return;
            }

            var dynamicScale = Upscale.Checked;

            var settings = Settings.GetEarthSettings(resolution);

            settings.Upscale = dynamicScale;

            settings.AtmosphereThickness = atmosphereThickness;

            PictureBox.Image = (Image)TextureGen.GeneratePlanet(settings);
            PlanetNameLabel.Text = PlanetNames.GetRandomPlanetName();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialogue = new SaveFileDialog();
            saveFileDialogue.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Png Image|*.png";
            saveFileDialogue.Title = "Save your Planet";
            saveFileDialogue.FileName = PlanetNameLabel.Text;
            saveFileDialogue.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialogue.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.
                System.IO.FileStream fs =
                    (System.IO.FileStream)saveFileDialogue.OpenFile();
                // Saves the Image in the appropriate ImageFormat based upon the
                // File type selected in the dialog box.
                // NOTE that the FilterIndex property is one-based.
                switch (saveFileDialogue.FilterIndex)
                {
                    case 1:
                        PictureBox.Image.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;

                    case 2:
                        PictureBox.Image.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Bmp);
                        break;

                    case 3:
                        PictureBox.Image.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Png);
                        break;
                }

                fs.Close();
            }
        }
    }
}