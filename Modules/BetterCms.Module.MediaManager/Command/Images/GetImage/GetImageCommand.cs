﻿using System;
using System.Globalization;

using BetterCms.Core.Mvc.Commands;
using BetterCms.Module.MediaManager.Models;
using BetterCms.Module.MediaManager.Models.Extensions;
using BetterCms.Module.MediaManager.Services;
using BetterCms.Module.MediaManager.ViewModels.Images;
using BetterCms.Module.Root.Mvc;

namespace BetterCms.Module.MediaManager.Command.Images.GetImage
{
    /// <summary>
    /// Command to get media image data.
    /// </summary>
    public class GetImageCommand : CommandBase, ICommand<Guid, ImageViewModel>
    {
        /// <summary>
        /// The tag service.
        /// </summary>
        /// <value>
        /// The tag service.
        /// </value>
        public ITagService TagService { get; set; }

        /// <summary>
        /// Gets or sets the file service.
        /// </summary>
        /// <value>
        /// The file service.
        /// </value>
        public IMediaFileUrlResolver FileUrlResolver { get; set; }

        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="imageId">The image id.</param>
        /// <returns>The view model.</returns>
        public ImageViewModel Execute(Guid imageId)
        {
            var image = Repository.First<MediaImage>(imageId);
            return new ImageViewModel
                {
                    Id = image.Id.ToString(),
                    Caption = image.Caption,
                    Title = image.Title,
                    Description = image.Description,
                    Url = FileUrlResolver.EnsureFullPathUrl(image.PublicUrl),
                    ThumbnailUrl = FileUrlResolver.EnsureFullPathUrl(image.PublicThumbnailUrl),
                    Version = image.Version.ToString(CultureInfo.InvariantCulture),
                    FileName = image.OriginalFileName,
                    FileExtension = image.OriginalFileExtension,
                    FileSize = image.SizeAsText(),
                    ImageWidth = image.Width,
                    ImageHeight = image.Height,
                    OriginalImageWidth = image.OriginalWidth,
                    OriginalImageHeight = image.OriginalHeight,
                    ImageAlign = image.ImageAlign.HasValue ? image.ImageAlign.Value : MediaImageAlign.Center,
                    CropCoordX1 = image.CropCoordX1.HasValue ? image.CropCoordX1.Value : 0,
                    CropCoordY1 = image.CropCoordY1.HasValue ? image.CropCoordY1.Value : 0,
                    CropCoordX2 = image.CropCoordX2.HasValue ? image.CropCoordX2.Value : image.OriginalWidth,
                    CropCoordY2 = image.CropCoordY2.HasValue ? image.CropCoordY2.Value : image.OriginalHeight,
                    OriginalImageUrl = FileUrlResolver.EnsureFullPathUrl(image.PublicOriginallUrl),
                    Tags = TagService.GetMediaTagNames(imageId)
                };
        }
    }
}