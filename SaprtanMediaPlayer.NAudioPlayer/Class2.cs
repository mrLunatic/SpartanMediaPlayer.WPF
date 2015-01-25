using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace SaprtanMediaPlayer.NAudioPlayer
{

    public partial class AudioPlaybackPanel : UserControl
    {
        private IWavePlayer waveOut;
        private string fileName = null;
        private AudioFileReader audioFileReader;
        private Action<float> setVolumeDelegate;



    

        private void OnButtonPlayClick(object sender, EventArgs e)
        {


            switch (waveOut.PlaybackState)
            {
                case PlaybackState.Playing:
                    return;
                case PlaybackState.Paused:
                    waveOut.Play();
                    return;
                case PlaybackState.Stopped:




            }

                waveOut.Stop();

            if (audioFileReader != null)
            {
                audioFileReader.Dispose();
                setVolumeDelegate = null;
                audioFileReader = null;
            }
            if (waveOut != null)
            {
                waveOut.Dispose();
                waveOut = null;
            }

            waveOut = new WaveOut();
            waveOut.PlaybackStopped += OnPlaybackStopped;
            
            audioFileReader = new AudioFileReader(fileName);

            var sampleChannel = new SampleChannel(audioFileReader, true);

            setVolumeDelegate = (vol) => sampleChannel.Volume = vol;

            var postVolumeMeter = new MeteringSampleProvider(sampleChannel);
            postVolumeMeter.StreamVolume += (o, args) => { };
            
            var sampleProvider = postVolumeMeter;
            

    //      labelTotalTime.Text = String.Format("{0:00}:{1:00}", (int)audioFileReader.TotalTime.TotalMinutes, audioFileReader.TotalTime.Seconds);

            waveOut.Init(sampleProvider);
            waveOut.Play();
        }

        private ISampleProvider CreateInputStream(string fileName)
        {
            this.audioFileReader = new AudioFileReader(fileName);

            var sampleChannel = new SampleChannel(audioFileReader, true);

            setVolumeDelegate = (vol) => sampleChannel.Volume = vol;

            var postVolumeMeter = new MeteringSampleProvider(sampleChannel);
            postVolumeMeter.StreamVolume += (sender, args) => { /* Shows scaled volume */ };

            return postVolumeMeter;
        }


        private void CreateWaveOut()
        {
            CloseWaveOut();
            var latency = (int)comboBoxLatency.SelectedItem;
            waveOut = SelectedOutputDevicePlugin.CreateDevice(latency);
            waveOut.PlaybackStopped += OnPlaybackStopped;
        }

        void OnPlaybackStopped(object sender, StoppedEventArgs e)
        {
            groupBoxDriverModel.Enabled = true;
            if (e.Exception != null)
            {
                MessageBox.Show(e.Exception.Message, "Playback Device Error");
            }
            if (audioFileReader != null)
            {
                audioFileReader.Position = 0;
            }
        }

        private void CloseWaveOut()
        {
            if (waveOut != null)
            {
                waveOut.Stop();
            }
            if (audioFileReader != null)
            {
                // this one really closes the file and ACM conversion
                audioFileReader.Dispose();
                setVolumeDelegate = null;
                audioFileReader = null;
            }
            if (waveOut != null)
            {
                waveOut.Dispose();
                waveOut = null;
            }
        }



        private void OnButtonPauseClick(object sender, EventArgs e)
        {
            if (waveOut != null)
            {
                if (waveOut.PlaybackState == PlaybackState.Playing)
                {
                    waveOut.Pause();
                }
            }
        }

        private void OnVolumeSliderChanged(object sender, EventArgs e)
        {
            if (setVolumeDelegate != null)
            {
                setVolumeDelegate(volumeSlider1.Volume);
            }
        }

        private void OnButtonStopClick(object sender, EventArgs e)
        {
            if (waveOut != null)
            {
                waveOut.Stop();
            }
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            if (waveOut != null && audioFileReader != null)
            {
                TimeSpan currentTime = (waveOut.PlaybackState == PlaybackState.Stopped) ? TimeSpan.Zero : audioFileReader.CurrentTime;
                trackBarPosition.Value = Math.Min(trackBarPosition.Maximum, (int)(100 * currentTime.TotalSeconds / audioFileReader.TotalTime.TotalSeconds));
                labelCurrentTime.Text = String.Format("{0:00}:{1:00}", (int)currentTime.TotalMinutes,
                    currentTime.Seconds);
            }
            else
            {
                trackBarPosition.Value = 0;
            }
        }

        private void trackBarPosition_Scroll(object sender, EventArgs e)
        {
            if (waveOut != null)
            {
                audioFileReader.CurrentTime = TimeSpan.FromSeconds(audioFileReader.TotalTime.TotalSeconds * trackBarPosition.Value / 100.0);
            }
        }

        private void OnOpenFileClick(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            string allExtensions = "*.wav;*.aiff;*.mp3;*.aac";
            openFileDialog.Filter = String.Format("All Supported Files|{0}|All Files (*.*)|*.*", allExtensions);
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileName = openFileDialog.FileName;
            }
        }
    }
}
