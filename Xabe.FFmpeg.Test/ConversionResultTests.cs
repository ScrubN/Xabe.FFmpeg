﻿using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Xabe.FFmpeg.Test
{
    public class ConversionResultTests
    {
        [Fact]
        public async Task ConversionResultTest()
        {
            string outputPath = Path.ChangeExtension(Path.GetTempFileName(), ".mp4");

            IMediaInfo info = await MediaInfo.Get(Resources.SubtitleSrt);

            ConversionResult result = await (await Conversion.ToMp4(Resources.Mp4WithAudio, outputPath)).Start();

            Assert.True(result.Result);
            Assert.NotNull(result.MediaInfo.Value);
            Assert.True(result.StartTime != DateTime.MinValue);
            Assert.True(result.EndTime != DateTime.MinValue);
            Assert.True(result.Duration > TimeSpan.FromMilliseconds(1));
        }
    }
}
