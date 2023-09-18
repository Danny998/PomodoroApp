using Avalonia.Threading;
using LibVLCSharp.Shared;
using NLog;
using PomodoroApp.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroApp.Implementations
{
    public class SoundEffectService : ISoundEffectService
    {
        public void SuccessEffect()
        {
            try
            {
                using var libvlc = new LibVLC(enableDebugLogs: true);
                using var media = new Media(libvlc, new Uri(@"C:\Users\danny\Videos\2022-10-01 08-55-25.mkv"));
                using var mediaplayer = new MediaPlayer(media);
                mediaplayer.SetRole(MediaPlayerRole.Music);
                // media.Dispose();
                Dispatcher.UIThread.Invoke(new Action(() =>
                {

                    mediaplayer.Play();
                }));
            }
            catch (Exception ex)
            {
                var logger = LogManager.GetCurrentClassLogger();
                logger.Error(ex);
            }

        }
    }
}
