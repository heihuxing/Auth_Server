using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using System.Web.Http.ModelBinding.Binders;
using System.Web.Http.Validation;
using System.Web.Http.Validation.Providers;
using System.Web.Http.ValueProviders.Providers;
using System.Net;

namespace Enssi
{

    /// <summary>
    /// Represents the <see cref="MediaTypeFormatter"/> class to handle multipart/form-data. 
    /// </summary>
    public class FormMultipartEncodedMediaTypeFormatter : MediaTypeFormatter
    {
        private const string SupportedMediaType = "multipart/form-data";

        /// <summary>
        /// Initializes a new instance of the <see cref="FormMultipartEncodedMediaTypeFormatter"/> class.
        /// </summary>
        public FormMultipartEncodedMediaTypeFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue(SupportedMediaType));
        }

        public override bool CanReadType(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            return true;
        }

        public override bool CanWriteType(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            return false;
        }

        public override async Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (readStream == null) throw new ArgumentNullException(nameof(readStream));

            try
            {
                // load multipart data into memory 
                var multipartProvider = await content.ReadAsMultipartAsync();
                // fill parts into a ditionary for later binding to model
                var modelDictionary = await ToModelDictionaryAsync(multipartProvider);
                //var decompress = Utility.GZipDecompressString(modelDictionary);
                var dejson = JsonConvert.DeserializeObject(modelDictionary, type, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                // bind data to model 
                return dejson; // BindToModel(modelDictionary, type, formatterLogger);
            }
            catch (Exception e)
            {
                if (formatterLogger == null)
                {
                    throw;
                }
                formatterLogger.LogError(string.Empty, e);
                return GetDefaultValueForType(type);
            }
        }

        private async Task<string> ToModelDictionaryAsync(MultipartMemoryStreamProvider multipartProvider)
        {
            var dictionary = new Dictionary<string, object>();

            // iterate all parts 
            foreach (var part in multipartProvider.Contents)
            {
                // unescape the name 
                var name = part.Headers.ContentDisposition.Name.Trim('"');

                // if we have a filename, we treat the part as file upload,
                // otherwise as simple string, model binder will convert strings to other types. 
                if (!string.IsNullOrEmpty(part.Headers.ContentDisposition.FileName))
                {
                    // set null if no content was submitted to have support for [Required]
                    //if (part.Headers.ContentLength.GetValueOrDefault() > 0)
                    //{
                    //    dictionary[name] = new HttpPostedFileMultipart(
                    //        part.Headers.ContentDisposition.FileName.Trim('"'),
                    //        part.Headers.ContentType.MediaType,
                    //        await part.ReadAsStreamAsync()
                    //    );
                    //}
                    //else
                    //{
                    //    dictionary[name] = null;
                    //}
                }
                else
                {
                    return await part.ReadAsStringAsync();
                }
            }

            return null;
        }
    }

    /// <summary>
    /// Represents a file that has uploaded by a client via multipart/form-data. 
    /// </summary>
    public class HttpPostedFileMultipart : HttpPostedFileBase
    {
        private readonly Stream _fileContents;

        public override int ContentLength => (int)_fileContents.Length;
        public override string ContentType { get; }
        public override string FileName { get; }
        public override Stream InputStream => _fileContents;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpPostedFileMultipart"/> class. 
        /// </summary>
        /// <param name="fileName">The fully qualified name of the file on the client</param>
        /// <param name="contentType">The MIME content type of an uploaded file</param>
        /// <param name="fileContents">The contents of the uploaded file.</param>
        public HttpPostedFileMultipart(string fileName, string contentType, Stream fileContents)
        {
            FileName = fileName;
            ContentType = contentType;
            _fileContents = fileContents;
        }
    }
}
