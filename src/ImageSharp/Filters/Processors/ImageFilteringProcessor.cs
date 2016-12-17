﻿// <copyright file="ImageFilteringProcessor.cs" company="James Jackson-South">
// Copyright (c) James Jackson-South and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace ImageSharp.Processors
{
    using System;

    /// <summary>
    /// Encapsulates methods to alter the pixels of an image. The processor operates on the original source pixels.
    /// </summary>
    /// <typeparam name="TColor">The pixel format.</typeparam>
    /// <typeparam name="TPacked">The packed format. <example>uint, long, float.</example></typeparam>
    public abstract class ImageFilteringProcessor<TColor, TPacked> : ImageProcessor<TColor, TPacked>, IImageFilteringProcessor<TColor, TPacked>
        where TColor : struct, IPackedPixel<TPacked>
        where TPacked : struct, IEquatable<TPacked>
    {
        /// <inheritdoc/>
        public void Apply(ImageBase<TColor, TPacked> source, Rectangle sourceRectangle)
        {
            try
            {
                this.BeforeApply(source, sourceRectangle);

                this.OnApply(source, sourceRectangle);

                this.AfterApply(source, sourceRectangle);
            }
            catch (Exception ex)
            {
                throw new ImageProcessingException($"An error occured when processing the image using {this.GetType().Name}. See the inner exception for more detail.", ex);
            }
        }

        /// <summary>
        /// This method is called before the process is applied to prepare the processor.
        /// </summary>
        /// <param name="source">The source image. Cannot be null.</param>
        /// <param name="sourceRectangle">
        /// The <see cref="Rectangle"/> structure that specifies the portion of the image object to draw.
        /// </param>
        protected virtual void BeforeApply(ImageBase<TColor, TPacked> source, Rectangle sourceRectangle)
        {
        }

        /// <summary>
        /// Applies the process to the specified portion of the specified <see cref="ImageBase{TColor, TPacked}"/> at the specified location
        /// and with the specified size.
        /// </summary>
        /// <param name="source">The source image. Cannot be null.</param>
        /// <param name="sourceRectangle">
        /// The <see cref="Rectangle"/> structure that specifies the portion of the image object to draw.
        /// </param>
        protected abstract void OnApply(ImageBase<TColor, TPacked> source, Rectangle sourceRectangle);

        /// <summary>
        /// This method is called after the process is applied to prepare the processor.
        /// </summary>
        /// <param name="source">The source image. Cannot be null.</param>
        /// <param name="sourceRectangle">
        /// The <see cref="Rectangle"/> structure that specifies the portion of the image object to draw.
        /// </param>
        protected virtual void AfterApply(ImageBase<TColor, TPacked> source, Rectangle sourceRectangle)
        {
        }
    }
}