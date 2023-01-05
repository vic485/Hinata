using System.Text;
using SharpCompress.Archives;
using SharpCompress.Archives.Rar;
using SharpCompress.Archives.Zip;
using SharpCompress.Common;
using SharpCompress.Readers;

namespace Hinata.Core;

public static class Extractor
{
    public static void Extract(string filePath, string outputPath, int? codePage = null, string? password = null,
        bool verbose = false)
    {
        var readerOptions = new ReaderOptions();
        var extractOptions = new ExtractionOptions
        {
            ExtractFullPath = true,
            Overwrite = true,
            //PreserveAttributes = true,
            PreserveFileTime = true
        };
        if (codePage.HasValue)
        {
            var encoding = CodePagesEncodingProvider.Instance.GetEncoding(codePage.Value);
            readerOptions.ArchiveEncoding = new ArchiveEncoding
            {
                CustomDecoder = (data, x, y) => encoding!.GetString(data)
            };
        }

        if (!string.IsNullOrEmpty(password))
            readerOptions.Password = password;

        try
        {
            var ext = Path.GetExtension(filePath);
            switch (ext)
            {
                case ".zip":
                {
                    var archive = ZipArchive.Open(filePath, readerOptions);
                    foreach (var entry in archive.Entries)
                    {
                        if (verbose)
                            Console.WriteLine(entry.Key);

                        entry.WriteToDirectory(outputPath, extractOptions);
                    }

                    break;
                }
                case ".rar":
                {
                    var archive = RarArchive.Open(filePath, readerOptions);
                    foreach (var entry in archive.Entries)
                    {
                        if (verbose)
                            Console.WriteLine(entry.Key);

                        entry.WriteToDirectory(outputPath, extractOptions);
                    }

                    break;
                }
            }
        }
        catch (PasswordProtectedException)
        {
            Console.WriteLine("Archive is password protected.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
