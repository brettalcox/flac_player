using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;

namespace flac_test
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		protected IrrKlang.ISoundEngine irrKlangEngine;
		protected IrrKlang.ISound currentlyPlayingSound;

		private System.Windows.Forms.Button SelectFileButton;
		private System.Windows.Forms.Button PauseButton;
		private System.Windows.Forms.TrackBar volumeTrackBar;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox filenameTextBox;
        private ListBox listBox1;

        private string filepath;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

            // create irrklang sound engine

            filepath = "C:\\Users\\BA042808\\Music\\test";
            loadFileNames();

			irrKlangEngine = new IrrKlang.ISoundEngine();
			playSelectedFile();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.filenameTextBox = new System.Windows.Forms.TextBox();
            this.SelectFileButton = new System.Windows.Forms.Button();
            this.PauseButton = new System.Windows.Forms.Button();
            this.volumeTrackBar = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.volumeTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // filenameTextBox
            // 
            this.filenameTextBox.Location = new System.Drawing.Point(24, 24);
            this.filenameTextBox.Name = "filenameTextBox";
            this.filenameTextBox.ReadOnly = true;
            this.filenameTextBox.Size = new System.Drawing.Size(601, 20);
            this.filenameTextBox.TabIndex = 0;
            // 
            // SelectFileButton
            // 
            this.SelectFileButton.Location = new System.Drawing.Point(631, 20);
            this.SelectFileButton.Name = "SelectFileButton";
            this.SelectFileButton.Size = new System.Drawing.Size(32, 24);
            this.SelectFileButton.TabIndex = 1;
            this.SelectFileButton.Text = "...";
            this.SelectFileButton.Click += new System.EventHandler(this.SelectFileButton_Click);
            // 
            // PauseButton
            // 
            this.PauseButton.Location = new System.Drawing.Point(184, 309);
            this.PauseButton.Name = "PauseButton";
            this.PauseButton.Size = new System.Drawing.Size(75, 23);
            this.PauseButton.TabIndex = 2;
            this.PauseButton.Text = "Pause";
            this.PauseButton.Click += new System.EventHandler(this.PauseButton_Click);
            // 
            // volumeTrackBar
            // 
            this.volumeTrackBar.Location = new System.Drawing.Point(24, 301);
            this.volumeTrackBar.Maximum = 100;
            this.volumeTrackBar.Name = "volumeTrackBar";
            this.volumeTrackBar.Size = new System.Drawing.Size(136, 45);
            this.volumeTrackBar.SmallChange = 5;
            this.volumeTrackBar.TabIndex = 3;
            this.volumeTrackBar.TickFrequency = 10;
            this.volumeTrackBar.Scroll += new System.EventHandler(this.volumeTrackBar_Scroll);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(24, 277);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Volume:";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 71);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(651, 173);
            this.listBox1.TabIndex = 6;
            this.listBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBox1_MouseDoubleClick);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(675, 360);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.volumeTrackBar);
            this.Controls.Add(this.PauseButton);
            this.Controls.Add(this.SelectFileButton);
            this.Controls.Add(this.filenameTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Test - irrKlang";
            ((System.ComponentModel.ISupportInitialize)(this.volumeTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

        void loadFileNames()
        {
            listBox1.Items.Clear();
            DirectoryInfo dinfo = new DirectoryInfo(filepath);//"C:\\Users\\BA042808\\Music\\test");
            FileInfo[] Files = dinfo.GetFiles("*.mp3");

            foreach( FileInfo file in Files )
            {
                listBox1.Items.Add(file.Name);
            }
        }

		// plays filename selected in edit box
		void playSelectedFile()
		{
			// stop currently playing sound

			if (currentlyPlayingSound != null)
				currentlyPlayingSound.Stop();

            // start new sound

            currentlyPlayingSound = irrKlangEngine.Play2D(filepath + "\\" + listBox1.SelectedItem as String, true);

			// update controls to display the playing file

			UpdatePauseButtonText();
			
            volumeTrackBar.Value = 100;
		}


		// pauses or unpauses the currently playing sound
		private void PauseButton_Click(object sender, System.EventArgs e)
		{
			if (currentlyPlayingSound != null)
			{
				currentlyPlayingSound.Paused = !currentlyPlayingSound.Paused;
				UpdatePauseButtonText();
			}
		}


		// Updates the text on the pause button
		private void UpdatePauseButtonText()
		{
			if (currentlyPlayingSound != null)
			{
				if (currentlyPlayingSound.Paused)
					PauseButton.Text = "Play";
				else
					PauseButton.Text = "Pause";
			}
			else
				PauseButton.Text = "";
		}


		// Sets new volume of currently playing sound
		private void volumeTrackBar_Scroll(object sender, System.EventArgs e)
		{
			if (currentlyPlayingSound != null)
			{
				currentlyPlayingSound.Volume = volumeTrackBar.Value / 100.0f;
			}
		}


		// selects a new file to play
		private void SelectFileButton_Click(object sender, System.EventArgs e)
		{
			System.Windows.Forms.FolderBrowserDialog dialog = new
				System.Windows.Forms.FolderBrowserDialog();

			//dialog.Filter = "All playable files (*.flac;*.mp3;*.ogg;*.wav;*.mod;*.it;*.xm;*.it;*.s3d)|*.flac;*.mp3;*.ogg;*.wav;*.mod;*.it;*.xm;*.it;*.s3d";
		    //dialog.FilterIndex = 0;

			if (dialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
			{
				filenameTextBox.Text = dialog.SelectedPath;
                filepath = dialog.SelectedPath;
                loadFileNames();
				playSelectedFile();
			}
		}

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            playSelectedFile();
        }
    }
}
