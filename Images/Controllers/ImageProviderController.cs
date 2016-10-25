﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Images.Services;

namespace Images.Controllers
{
    public class ImageProviderController : Controller
    {
        private readonly IImageProvider imageProvider;

        public ImageProviderController()
        {
            imageProvider = new IconFinderImageProvider();
        }

        //id = "q=honey&style=handdrawn";
        public ActionResult IconFinder(string q, string style = null)
        {
            var url = BuildIconFinderUrl(q, style);
            var image = imageProvider.GetImage(url);

            if (image == null && !string.IsNullOrWhiteSpace(style))
            {
                url = BuildIconFinderUrl(q);
                image = imageProvider.GetImage(url);
            }

            return image;
        }

        private static string BuildIconFinderUrl(string q, string style = null)
        {
            var urlPartBuilder = new StringBuilder();
            urlPartBuilder.Append($"q={q}");
            if (!string.IsNullOrWhiteSpace(style))
                urlPartBuilder.Append($"&style={style}");
            var urlPart = urlPartBuilder.ToString();

            var url = $"https://www.iconfinder.com/search/?{urlPart}";
            return url;
        }
    }
}