// Copyright (c) Six Labors.
// Licensed under the Apache License, Version 2.0.

using System;
#if SUPPORTS_RUNTIME_INTRINSICS
using System.Runtime.Intrinsics.X86;
#endif
using SixLabors.ImageSharp.Formats.Jpeg.Components;
using SixLabors.ImageSharp.Formats.Jpeg.Components.Encoder;
using SixLabors.ImageSharp.Tests.Formats.Jpg.Utils;
using SixLabors.ImageSharp.Tests.TestUtilities;
using Xunit;
using Xunit.Abstractions;

// ReSharper disable InconsistentNaming
namespace SixLabors.ImageSharp.Tests.Formats.Jpg
{
    [Trait("Format", "Jpg")]
    public class HuffmanScanEncoderTests
    {
        private ITestOutputHelper Output { get; }

        public HuffmanScanEncoderTests(ITestOutputHelper output)
        {
            Output = output;
        }

        private static int GetHuffmanEncodingLength_Reference(uint number)
        {
            int bits = 0;
            if (number > 32767)
            {
                number >>= 16;
                bits += 16;
            }
            if (number > 127)
            {
                number >>= 8;
                bits += 8;
            }
            if (number > 7)
            {
                number >>= 4;
                bits += 4;
            }
            if (number > 1)
            {
                number >>= 2;
                bits += 2;
            }
            if (number > 0)
            {
                bits++;
            }
            return bits;
        }

        [Fact]
        public void GetHuffmanEncodingLength_Zero()
        {
            int expected = 0;

            int actual = HuffmanScanEncoder.GetHuffmanEncodingLength(0);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetHuffmanEncodingLength_Random(int seed)
        {
            int maxNumber = 1 << 16;

            var rng = new Random(seed);
            for (int i = 0; i < 1000; i++)
            {
                uint number = (uint)rng.Next(0, maxNumber);

                int expected = GetHuffmanEncodingLength_Reference(number);

                int actual = HuffmanScanEncoder.GetHuffmanEncodingLength(number);

                Assert.Equal(expected, actual);
            }
        }
    }
}
