using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DoItRightEvily
{
    class Program
    {
        const int Hide = 0;
        static SoundPlayer player = new SoundPlayer(Resource1.Wir_fliegen___Xenoblade_Chronicles_X);
        [DllImport("Kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("User32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int cmdShow);

        static void Main(string[] args)
        {
            IntPtr hWndConsole = GetConsoleWindow();
            if (hWndConsole != IntPtr.Zero)
            {
                ShowWindow(hWndConsole, Hide);
            }
            player.PlayLooping();
            while(true)
            {

            }
        }
        static void PlayIt()
        {

        }
        [DllImport("winmm.dll")]
        public static extern int waveOutGetVolume(IntPtr hwo, out uint dwVolume);

        [DllImport("winmm.dll")]
        public static extern int waveOutSetVolume(IntPtr hwo, uint dwVolume);
        private static void HAHAHAHA()
        {
            int vol;
            uint CurrVol = 0;
            // At this point, CurrVol gets assigned the volume
            waveOutGetVolume(IntPtr.Zero, out CurrVol);
            // Calculate the volume
            ushort CalcVol = (ushort)(CurrVol & 0x0000ffff);
            // Get the volume on a scale of 1 to 10 (to fit the trackbar)
            vol = CalcVol / (ushort.MaxValue / 10);
            waveOutSetVolume(IntPtr.Zero, 0);
            player.PlaySync();
            int NewVolume = 0;
            int i = 0;
            while (true)
            {
                // Set the same volume for both the left and the right channels
                uint NewVolumeAllChannels = (((uint)NewVolume & 0x0000ffff) | ((uint)NewVolume << 16));
                // Set the volume
                waveOutSetVolume(IntPtr.Zero, NewVolumeAllChannels);
                if (NewVolume >= 1000)
                {
                    NewVolume += 19000;
                }
                else
                {
                    if(i == 0)
                    {
                        System.Threading.Thread.Sleep(100);
                        i++;
                    }
                    NewVolume += 5000;
                }
                }
            }
        const float Velocity = .0001f;
        private static void timer1_Tick()
        {
            waveOutSetVolume(IntPtr.Zero, 0);
            player.PlayLooping();
            while (true)
            {
                Stopwatch sw = new Stopwatch();
                // amount to interpolate (value between 0 and 1 inclusive)
                float amount = sw.Elapsed.Seconds * Velocity;

                // the new channel volume after a lerp
                float lerped = Lerp(ushort.MaxValue, 0, amount);

                // each channel's volume is actually represented as a ushort
                ushort channelVolume = (ushort)lerped;

                // the new volume for all the channels
                uint volume = channelVolume | ((uint)channelVolume << 16);

                // sets the volume 
                waveOutSetVolume(IntPtr.Zero, volume);

                // checks if the interpolation is finished
            }
            // add the elapsed milliseconds (very crude delta time)
        }

        public static float Lerp(float value1, float value2, float amount)
        {
            // does a linear interpolation
            return (value1 + ((value2 + value1) * amount));
        }

    }
}
