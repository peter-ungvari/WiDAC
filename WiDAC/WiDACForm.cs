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
using System.Runtime.InteropServices;
using NReco.VideoConverter;

namespace WiDAC
{
    public partial class WiDACForm : Form
    {
        private WasapiCapture outputCapture;
        private WasapiCapture inputCapture;
        private WaveWriter outputWaveWriter;
        private WaveWriter inputWaveWriter;
        private bool recording;
        private Stopwatch stopwatch = Stopwatch.StartNew();
        private long frameCount;
        System.Windows.Forms.Timer recordingTimer = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer previewTimer = new System.Windows.Forms.Timer();
        MemoryStream videoInputStream = new MemoryStream(1024 * 1024 * 50); //50M buffer
        private ConvertLiveMediaTask videoTask;

        public WiDACForm()
        {
            InitializeComponent();
            InitializeListValues();
            LoadDevices(DataFlow.Capture, inputDeviceComboBox);
            LoadDevices(DataFlow.Render, outputDeviceComboBox);

            previewPanel.BackgroundImageLayout = ImageLayout.Zoom;

            recordingTimer.Interval = 40;

            previewTimer.Interval = 500;
            previewTimer.Start();

            recordingTimer.Tick += RecordingTimerTick;
            previewTimer.Tick += previewTimer_Tick;
            captureCheckBox.Click += captureCheckBox_Click;

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
            }
            else
            {
                Stop();
            }
        }

        private void RecordingTimerTick(object sender, EventArgs e)
        {
            if (!recording)
            {
                return;
            }

            if (frameCount < stopwatch.ElapsedMilliseconds / 1000.0 * (int)frameRateComboBox.SelectedValue)
            {
                try
                {
                    ++frameCount;

                    Bitmap bmpScreenshot = CreateScreenshot();

                    //bmpScreenshot.Save(videoInputStream, ImageFormat.Bmp);
                    /*var bd = bmpScreenshot.LockBits(new Rectangle(0, 0, bmpScreenshot.Width, bmpScreenshot.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
                    byte[] buf = new byte[bd.Stride * bmpScreenshot.Height];
                    Marshal.Copy(bd.Scan0, buf, 0, buf.Length);*/
                    
                    //var ms = new MemoryStream();
                    //bmpScreenshot.Save(ms, ImageFormat.);
                    var buf = new byte[] {0,0,0,0};//ms.ToArray();
                    
                    videoTask.Write(buf, 0, buf.Length);
                    //bmpScreenshot.UnlockBits(bd);

                    bmpScreenshot.Dispose();
                }
                catch (ArgumentException ex)
                {
                    Trace.TraceError("ArgumentException on capture", ex.StackTrace);
                }

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
                screens.Add(screen, String.Format("{0} [{1}x{2}]", screen.DeviceName, screen.Bounds.Width, screen.Bounds.Height));
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

            SetComboBoxData(videoBitrateComboBox, new Dictionary<int, string>() { 
                { 200 * 1024, "400 kbps" },
                { 400 * 1024, "600 kbps" },
                { 800 * 1024, "800 kbps" }
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

        private void Start()
        {
            if (recording)
            {
                return;
            }

            FFMpegConverter ffMpeg = new FFMpegConverter();
            videoTask = ffMpeg.ConvertLiveMedia(videoInputStream, Format.raw_video, "out.avi", Format.avi, new ConvertSettings()
            {
                VideoCodec = "libx264",
                VideoFrameRate = (int)frameRateComboBox.SelectedValue,
                CustomInputArgs = "-pix_fmt bgr24",
                CustomOutputArgs = "-crf 23"
            });

            stopwatch.Restart();
            recordingTimer.Start();
            frameCount = 0;

            

            //videoTask.Start();

            outputCapture = new WasapiLoopbackCapture();

            outputCapture.Device = outputDeviceComboBox.SelectedItem as MMDevice;
            outputCapture.Initialize();
            outputWaveWriter = new WaveWriter("out.wav", outputCapture.WaveFormat);

            outputCapture.DataAvailable +=
                (s, capData) => outputWaveWriter.Write(capData.Data, capData.Offset, capData.ByteCount);
            outputCapture.Start();


            inputCapture = new WasapiCapture();
            inputCapture.Device = inputDeviceComboBox.SelectedItem as MMDevice;
            inputCapture.Initialize();
            inputWaveWriter = new WaveWriter("in.wav", inputCapture.WaveFormat);

            inputCapture.DataAvailable +=
                (s, capData) => inputWaveWriter.Write(capData.Data, capData.Offset, capData.ByteCount);
            inputCapture.Start();

            recording = true;

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

            recordingTimer.Stop();
            stopwatch.Restart();
            outputCapture.Stop();
            inputCapture.Stop();

            videoInputStream.Dispose();
            //videoTask.Stop(false);

            if (inputWaveWriter != null)
            {
                inputWaveWriter.Dispose();
            }

            if (inputWaveWriter != null)
            {
                inputWaveWriter.Dispose();
            }

            outputCapture.Dispose();

            inputCapture.Dispose();

            videoBitrateComboBox.Dispose();

            recording = false;
        }

        private void WiDACForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Stop();
        }
    }
}
