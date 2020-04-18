﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xabe.FFmpeg
{
    public partial class Conversion
    {
        /// <summary>
        ///     Extract audio from file
        /// </summary>
        /// <param name="inputPath">Input path</param>
        /// <param name="outputPath">Output video stream</param>
        /// <returns>Conversion result</returns>
        public static IConversion ExtractAudio(string inputPath, string outputPath)
        {
            IMediaInfo info = MediaInfo.Get(inputPath).GetAwaiter().GetResult();

            IAudioStream audioStream = info.AudioStreams.FirstOrDefault();

            return New()
                .AddStream(audioStream)
                .SetAudioBitrate(audioStream.Bitrate)
                .SetOutput(outputPath);
        }

        /// <summary>
        ///     Add audio stream to video file
        /// </summary>
        /// <param name="videoPath">Video</param>
        /// <param name="audioPath">Audio</param>
        /// <param name="outputPath">Output file</param>
        /// <returns>Conversion result</returns>
        public static IConversion AddAudio(string videoPath, string audioPath, string outputPath)
        {
            IMediaInfo videoInfo = MediaInfo.Get(videoPath).GetAwaiter().GetResult();

            IMediaInfo audioInfo = MediaInfo.Get(audioPath).GetAwaiter().GetResult();

            return New()
                .AddStream(videoInfo.VideoStreams.FirstOrDefault())
                .AddStream(audioInfo.AudioStreams.FirstOrDefault())
                .AddStream(videoInfo.SubtitleStreams.ToArray())
                .SetOutput(outputPath);
        }
    }
}
