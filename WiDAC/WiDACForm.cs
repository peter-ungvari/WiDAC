using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CSCore;
using CSCore.SoundIn;
using CSCore.CoreAudioAPI;
using System.Threading;
using CSCore.Codecs.WAV;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms.VisualStyles;

namespace WiDAC
{
    public partial class WiDACForm : Form
    {
        private const string OutputAudioFileName = "temp_out.wav";
        private const string InputAudioFileName = "temp_in.wav";
        private const string CapturedVideoFileName = "temp_video.mp4";

        private WasapiCapture outputCapture;
        private WasapiCapture inputCapture;
        private WaveWriter outputWaveWriter;
        private WaveWriter inputWaveWriter;
        private bool recording;
        System.Windows.Forms.Timer previewTimer = new System.Windows.Forms.Timer();

        public WiDACForm()
        {
            InitializeComponent();
            InitializeListValues();
            LoadDevices(DataFlow.Capture, inputDeviceComboBox);
            LoadDevices(DataFlow.Render, outputDeviceComboBox);

            previewPanel.BackgroundImageLayout = ImageLayout.Zoom;
            previewTimer.Interval = 300;
            previewTimer.Start();
            previewTimer.Tick += previewTimer_Tick;
            captureCheckBox.Click += captureCheckBox_Click;
            FormClosing += Question;
        }

        private void Question(object sender, CancelEventArgs e)
        {
            DialogResult questionResult = MessageBox.Show(
                "Do you want to save the captured audio and video?\n\n" +
                "Yes: Save captured media to a video file.\n" +
                "No: Discard captured media.\n" +
                "Cancel: Keep capturing.",
                "Save captured media?",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

            if (DialogResult.Cancel == questionResult)
            {
                e.Cancel = true;
                return;
            }

            Stop();

            if (DialogResult.Yes == questionResult)
            {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.AddExtension = true;
                dialog.DefaultExt = "mp4";
                dialog.Filter = "MPEG-4 Files (*.mp4)|*.mp4";
                dialog.RestoreDirectory = true;

                DialogResult saveFileResult = dialog.ShowDialog(this);

                if (saveFileResult != DialogResult.OK)
                {
                    e.Cancel = true;
                    return;
                }

                //TODO Call FFMPEG
                Trace.TraceInformation("audio bitrate: {0}, audio sample rate: {1}, audio input file1: {2}, audio input file2: {3}, video file: {4}, output file: {5}", 
                    bitRateComboBox.SelectedValue, rateComboBox.SelectedValue, OutputAudioFileName, InputAudioFileName, CapturedVideoFileName, dialog.FileName);

            }
        }

        void previewTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                Bitmap previous = (Bitmap)previewPanel.BackgroundImage;
                Bitmap bmpScreenshot = CreateScreenshot();
                previewPanel.BackgroundImage = bmpScreenshot;

                if (previous != null)
                {
                    previous.Dispose();
                }

            }
            catch (ArgumentException ex)
            {
                Trace.TraceError("ArgumentException on preview", ex.StackTrace);
            }

        }

        void captureCheckBox_Click(object sender, EventArgs e)
        {
            if (!recording)
            {
                Start();
                return;
            }

            CancelEventArgs cancel = new CancelEventArgs();
            Question(sender, cancel);

            if (cancel.Cancel)
            {
                captureCheckBox.CheckState = CheckState.Checked;
            }
        }

        private Bitmap CreateScreenshot()
        {
            //Create a new bitmap.
            Screen screen = screenComboBox.SelectedValue as Screen;
            Bitmap bmpScreenshot = new Bitmap(screen.Bounds.Width, screen.Bounds.Height, PixelFormat.Format32bppArgb);

            // Create a graphics object from the bitmap.
            Graphics gfxScreenshot = Graphics.FromImage(bmpScreenshot);

            // Take the screenshot from the upper left corner to the right bottom corner.
            gfxScreenshot.CopyFromScreen(screen.Bounds.X,
                screen.Bounds.Y,
                0,
                0,
                screen.Bounds.Size,
                CopyPixelOperation.SourceCopy);

            gfxScreenshot.Dispose();
            return bmpScreenshot;
        }

        private void SetComboBoxData<T>(ComboBox comboBox, Dictionary<T, string> items)
        {
            comboBox.DisplayMember = "Value";
            comboBox.ValueMember = "Key";
            comboBox.DataSource = new BindingSource(items, null);
        }

        private void InitializeListValues()
        {
            SetComboBoxData(rateComboBox, new Dictionary<int, string>() {
                { 8000, "8 kHz" },
                { 16000, "16 kHz" },
                { 44100, "44.1 kHz" },
                { 48000, "48 kHz" }
            });

            Dictionary<Screen, string> screens = new Dictionary<Screen, string>();
            foreach (Screen screen in Screen.AllScreens)
            {
                screens.Add(screen, String.Format("Offset:({0},{1}) - Resolution:{2}x{3}", screen.Bounds.X, screen.Bounds.Y, screen.Bounds.Width, screen.Bounds.Height));
            }

            SetComboBoxData(screenComboBox, screens);

            SetComboBoxData(frameRateComboBox, new Dictionary<int, string>()
            {
                { 1, "1 fps"},
                { 6, "6 fps"}
            });

            SetComboBoxData(bitRateComboBox, new Dictionary<int, string>() { 
                { 32 * 1024, "32 kbps" },
                { 96 * 1024, "64 kbps" },
                { 192 * 1024, "96 kbps" }
            });

            SetComboBoxData(crfComboBox, new Dictionary<int, string>() { 
                { 18, "High Quality - 18" },
                { 23, "Normal Quality - 23" },
                { 28, "Low Quality - 28" }
            });

        }

        private void LoadDevices(DataFlow dataFlow, ComboBox combobox)
        {
            MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
            MMDevice defaultDevice = (MMDevice)enumerator.GetDefaultAudioEndpoint(dataFlow, Role.Multimedia);

            string defaultDeviceId;
            defaultDevice.GetIdNative(out defaultDeviceId);

            foreach (MMDevice device in enumerator.EnumAudioEndpoints(dataFlow, DeviceState.Active))
            {
                combobox.Items.Add(device);

                string deviceId;
                device.GetIdNative(out deviceId);

                if (deviceId.Equals(defaultDeviceId))
                {
                    combobox.SelectedItem = device;
                }
            }
        }

        private void DeleteFiles(String[] fileNames)
        {
            foreach (var fileName in fileNames)
            {
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
            }
        }

        private void Start()
        {
            if (recording)
            {
                return;
            }

            DeleteFiles(new string[]{InputAudioFileName, OutputAudioFileName, CapturedVideoFileName});

            outputCapture = new WasapiLoopbackCapture();

            outputCapture.Device = outputDeviceComboBox.SelectedItem as MMDevice;
            outputCapture.Initialize();
            outputWaveWriter = new WaveWriter(OutputAudioFileName, outputCapture.WaveFormat);

            outputCapture.DataAvailable +=
                (s, capData) => outputWaveWriter.Write(capData.Data, capData.Offset, capData.ByteCount);
            outputCapture.Start();


            inputCapture = new WasapiCapture();
            inputCapture.Device = inputDeviceComboBox.SelectedItem as MMDevice;
            inputCapture.Initialize();
            inputWaveWriter = new WaveWriter(InputAudioFileName, inputCapture.WaveFormat);

            inputCapture.DataAvailable +=
                (s, capData) => inputWaveWriter.Write(capData.Data, capData.Offset, capData.ByteCount);
            inputCapture.Start();

            recording = true;

            groupBox1.Enabled = false;
            groupBox2.Enabled = false;
            groupBox3.Enabled = false;

            Rectangle screenRectangle = ((Screen) screenComboBox.SelectedValue).Bounds;
            //TODO call FFMPEG
            Trace.TraceInformation("video file: {0}, video crf: {1}, video offset x: {2}, video offset y: {3}",
                CapturedVideoFileName, crfComboBox.SelectedValue, screenRectangle.X, screenRectangle.Y);

        }

        private void Stop()
        {
            if (outputCapture == null)
            {
                return;
            }

            if (!recording)
            {
                return;
            }

            outputCapture.Stop();
            inputCapture.Stop();

            groupBox1.Enabled = true;
            groupBox2.Enabled = true;
            groupBox3.Enabled = true;

            //TODO stop FFMPEG

            if (outputWaveWriter != null)
            {
                outputWaveWriter.Dispose();
            }

            if (inputWaveWriter != null)
            {
                inputWaveWriter.Dispose();
            }

            outputCapture.Dispose();

            inputCapture.Dispose();

            recording = false;
        }

    }
}
