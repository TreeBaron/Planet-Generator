﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Planet_Generator
{
    public partial class MainMenu : Form
    {
        Dictionary<string, Settings> SettingsDictionary;

        public MainMenu()
        {
            InitializeComponent();
        }

        private void GenerateTextureButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
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

            // regen settings to avoid object ref problems since gen can change settings objects
            SettingsDictionary = Settings.GetSettingsDictionary(resolution);

            var settings = SettingsDictionary[(string)SettingsComboBox.SelectedItem];

            settings.Resolution = resolution;

            settings.Upscale = dynamicScale;

            settings.AtmosphereThickness = atmosphereThickness;

            PictureBox.Image = (Image)TextureGen.GeneratePlanet(settings);
            PlanetNameLabel.Text = PlanetNames.GetRandomPlanetName();


            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"completed.wav");
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(path);
            player.Play();

            this.WindowState = FormWindowState.Normal;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (PictureBox.Image != null)
            {
                SaveFileDialog saveFileDialogue = new SaveFileDialog();
                saveFileDialogue.Filter = "Png Image|*.png";
                saveFileDialogue.Title = "Save your Planet";
                saveFileDialogue.FileName = PlanetNameLabel.Text;
                saveFileDialogue.ShowDialog();

                // If the file name is not an empty string open it for saving.
                if (saveFileDialogue.FileName != "")
                {
                    // Saves the Image via a FileStream created by the OpenFile method.
                    System.IO.FileStream fs =
                        (System.IO.FileStream)saveFileDialogue.OpenFile();
                    PictureBox.Image.Save(fs,
                              System.Drawing.Imaging.ImageFormat.Png);
                    fs.Close();
                }
            }
            else
            {
                MessageBox.Show("You must first generate an image before you can save it.");
            }
        }

        private void MainMenu_Load(object sender, EventArgs e)
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

            SettingsDictionary = Settings.GetSettingsDictionary(resolution);

            foreach(var key in SettingsDictionary.Keys)
            {
                SettingsComboBox.Items.Add(key);
            }
            SettingsComboBox.SelectedIndex = 0;
        }

        private void CreditsButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Created By John Dodd\nSound Effects By Kenneth Cooney");
        }

        private void ImageLoadButton_Click(object sender, EventArgs e)
        {
            try
            {
                var openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    var filePath = openFileDialog.FileName;

                    var bitmap = new Bitmap(filePath);

                    PictureBox.Image = bitmap;

                    PictureBox.Refresh();

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

                    // regen settings to avoid object ref problems since gen can change settings objects
                    SettingsDictionary = Settings.GetSettingsDictionary(resolution);

                    var selectedSettings = SettingsDictionary[(string)SettingsComboBox.SelectedItem];

                    var settings = Settings.GetSettingsFromImage(resolution, bitmap, selectedSettings);

                    settings.Resolution = resolution;

                    settings.Upscale = dynamicScale;

                    settings.AtmosphereThickness = atmosphereThickness;

                    PictureBox.Image = (Image)TextureGen.GeneratePlanet(settings);
                    PlanetNameLabel.Text = PlanetNames.GetRandomPlanetName();


                    string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"completed.wav");
                    System.Media.SoundPlayer player = new System.Media.SoundPlayer(path);
                    player.Play();

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Something went wrong.");
            }
        }
    }
}