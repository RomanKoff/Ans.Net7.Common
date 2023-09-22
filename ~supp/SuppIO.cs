using System.Security.Cryptography;
using System.Text;

namespace Ans.Net7.Common
{

    public static class SuppIO
    {

        /*
		 * void CreateDirectoryIfNotExists(string path);
		 * void DeleteDirectoryIfExists(string path);
		 * void DeleteFileIfExists(string path);
         * void FileWrite(string filename, string content, EncodingsEnum encoding = EncodingsEnum.UTF8, FileMode mode = FileMode.Create);
		 * void FileWrite(string filename, byte[] content, FileMode mode);
		 * 
		 * string FileRead(string filename, EncodingsEnum encoding = EncodingsEnum.UTF8);
         * string GetCatalogName(int id);
		 * string GetFileName(string path);
		 * string GetFileNameWithoutExtension(string path);
		 * string GetDirectoryName(string path);
		 * string GetFileExtention(string path, bool hasDot);
		 * string[] GetFilenameHalfs(string filename);
		 * 
		 * string GetFileBegin(FileStream stream, int size = 255);
		 * string GetFileBegin(string filename, int size = 255);
		 * string GetFileSHA1(FileStream stream, byte[] salt);
		 * string GetFileSHA1(FileStream stream, string salt);
		 * string GetFileSHA1(FileStream stream);
		 * string GetFileSHA1(string filename, byte[] salt);
		 * string GetFileSHA1(string filename, string salt);
		 * 
		 * string GetSizeOfKB(long size);
		 * string GetSizeOfKB(int size);
		 * 
		 * string GetSafeFilename(string filename);
		 * 
		 * bool HasInvalidPathChars(string path);
		 * bool HasInvalidFileNameChars(string fileName);
		 * 
		 * Encoding GetEncoding(EncodingsEnum encoding);
		 * 
		 * bool IsFilesEqual(FileInfo file1, FileInfo file2);
		 */


        /* methods */


        /// <summary>
        /// Создает директорию, если ее не существует
        /// </summary>
        public static void CreateDirectoryIfNotExists(
            string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }


        /// <summary>
        /// Удаляет директорию и все ее содержимое, если она существует
        /// </summary>
        public static void DeleteDirectoryIfExists(
            string path)
        {
            if (Directory.Exists(path))
                Directory.Delete(path, true);
        }


        /// <summary>
        /// Удаляет файл, если он существует
        /// </summary>
        public static void DeleteFileIfExists(
            string path)
        {
            if (File.Exists(path))
                File.Delete(path);
        }


        public static void FileWrite(
            string filename,
            string content,
            EncodingsEnum encoding = EncodingsEnum.UTF8,
            FileMode mode = FileMode.Create)
        {
            using var fs1 = new FileStream(
                filename, mode);
            using var sw1 = new StreamWriter(
                fs1, GetEncoding(encoding));
            sw1.Write(content);
        }


        public static void FileWrite(
            string filename,
            byte[] content,
            FileMode mode)
        {
            using var fs1 = new FileStream(filename, mode);
            fs1.Write(content, 0, content.Length);
        }


        /* functions */


        public static string FileRead(
            string filename,
            EncodingsEnum encoding = EncodingsEnum.UTF8)
        {
            using var fs1 = new FileStream(
                filename, FileMode.Open, FileAccess.Read, FileShare.Read);
            using var sr1 = new StreamReader(
                fs1, GetEncoding(encoding));
            return sr1.ReadToEnd();
        }


        public static string GetCatalogName(
            int id)
        {
            return string.Format("{0:000/00/00}", id);
        }


        /// <summary>
        /// Возвращает имя файла и расширение из path
        /// </summary>
        public static string GetFileName(
            string path)
        {
            return Path.GetFileName(path);
        }


        /// <summary>
        /// Возвращает имя файла без расширения из path
        /// </summary>
        public static string GetFileNameWithoutExtension(
            string path)
        {
            return Path.GetFileNameWithoutExtension(path);
        }


        /// <summary>
        /// Возвращает имя директории из path
        /// </summary>
        public static string GetDirectoryName(
            string path)
        {
            return Path.GetDirectoryName(path);
        }


        /// <summary>
        /// Возвращает расширение файла из path
        /// </summary>
        public static string GetFileExtention(
            string path,
            bool hasDot)
        {
            string s1 = Path.GetExtension(path).ToLower();
            return (hasDot)
                ? s1 : (!string.IsNullOrEmpty(s1))
                    ? s1[1..] : s1;
        }


        /// <summary>
        /// Возвращает части имени файла разделенные '.'
        /// </summary>
        public static string[] GetFilenameHalfs(
            string filename)
        {
            int i1 = filename.LastIndexOf('.');
            if (i1 == -1)
                return new string[] { filename, string.Empty };
            return new string[] {
                filename[..i1],
                filename[(i1 + 1)..]
            };
        }


        public static string GetFileBegin(
            FileStream stream,
            int size = 255)
        {
            byte[] a1 = new byte[size];
            _ = stream.Read(a1, 0, size);
            return Convert.ToBase64String(a1);
        }


        public static string GetFileBegin(
            string filename,
            int size = 255)
        {
            using var stream1 = new FileStream(
                filename, FileMode.Open, FileAccess.Read, FileShare.Read);
            return GetFileBegin(stream1, size);
        }


        public static string GetFileSHA1(
            FileStream stream,
            byte[] salt)
        {
            using var alg1 = new HMACSHA1(salt);
            try
            {
                byte[] a1 = alg1.ComputeHash(stream);
                return string.Join(
                    "", a1.Select(x => x.ToString("x2"))); // Convert.ToBase64String(result);
            }
            finally { alg1.Clear(); }
        }


        public static string GetFileSHA1(
            FileStream stream,
            string salt)
        {
            return GetFileSHA1(stream, Encoding.Unicode.GetBytes(salt));
        }


        public static string GetFileSHA1(
            FileStream stream)
        {
            byte[] a1 = Array.Empty<byte>();
            return GetFileSHA1(stream, a1);
        }


        public static string GetFileSHA1(
            string filename,
            byte[] salt)
        {
            using var stream1 = new FileStream(
                filename, FileMode.Open, FileAccess.Read, FileShare.Read);
            return GetFileSHA1(stream1, salt);
        }


        public static string GetFileSHA1(
            string filename,
            string salt)
        {
            return GetFileSHA1(filename, Encoding.Unicode.GetBytes(salt));
        }


        public static string GetSizeOfKB(
            long size)
        {
            long l1 = 1;
            if (size >= 1024)
                l1 = (size + 511) / 1024;
            return l1.ToString(Resources.Common.Format_SizeKB).TrimStart();
        }


        public static string GetSizeOfKB(
            int size)
        {
            return GetSizeOfKB((long)size);
        }


        public static string GetSafeFilename(
            string filename)
        {
            /*
			0123456789abcdefghijklmnopqrstuvwxyz_-^~
			#,№ -> n
			—, – -> -			
			*/

            var halfs1 = GetFilenameHalfs(filename);
            var s1 = halfs1[^2] // halfs1[halfs1.Length - 2]
                .ToLower()
                .GetTranslitRuToEn()
                .Replace('#', 'n').Replace('№', 'n')
                .Replace('.', '-')
                .Replace('—', '-').Replace('–', '-')
                .Replace('‘', '_').Replace('’', '_').Replace('‚', '_')
                .Replace('“', '_').Replace('”', '_').Replace('„', '_')
                .Replace('′', '_').Replace('″', '_')
                .Replace('‹', '_').Replace('›', '_')
                .Replace('«', '_').Replace('»', '_');
            var sb1 = new StringBuilder();
            for (int i1 = 0; i1 < s1.Length; ++i1)
            {
                char ch1 = s1[i1];
                int code1 = ch1;
                switch (code1)
                {
                    case > 47 and < 58 or > 96 and < 123 or 45 or 94 or 95 or 126:
                        sb1.Append(ch1);
                        break;
                    case > 126:
                        sb1.Append($"^{code1}");
                        break;
                    default:
                        sb1.Append('_');
                        break;
                };
            }
            var s2 = sb1.ToString();
            if (s2.Length < 5 && _Consts.FORBIDDEN_FILE_NAMES.Contains(s1))
                return $"[{s2}].{halfs1.Last()}";
            var s3 = s2
                .GetReplaceRecursively("__", "_")
                .GetReplaceRecursively("_-", "-")
                .GetReplaceRecursively("-_", "-");
            return $"{s3}.{halfs1.Last()}";
        }


        public static bool HasInvalidPathChars(
            string path)
        {
            return path.IndexOfAny(Path.GetInvalidPathChars()) >= 0;
        }


        public static bool HasInvalidFileNameChars(
            string fileName)
        {
            return fileName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0;
        }


        public static Encoding GetEncoding(
            EncodingsEnum encoding)
        {
            return encoding switch
            {
                EncodingsEnum.WINDOWS1251 => _Consts.ENCODING_WINDOWS1251,
                EncodingsEnum.KOI8R => _Consts.ENCODING_KOI8R,
                EncodingsEnum.CP866 => _Consts.ENCODING_CP866,
                _ => _Consts.ENCODING_UTF8
            };
        }


        /// <summary>
        /// Сравнение файлов
        /// </summary>
        public static bool IsFilesEqual(
            FileInfo file1,
            FileInfo file2)
        {
            if (file1.Length != file2.Length)
                return false;
            using var stream1 = file1.OpenRead();
            using var stream2 = file2.OpenRead();
            if (!GetFileBegin(stream1).Equals(GetFileBegin(stream2)))
                return false;
            if (!GetFileSHA1(stream1).Equals(GetFileSHA1(stream2)))
                return false;
            return true;
        }

    }



    public enum EncodingsEnum
    {
        UTF8,
        WINDOWS1251,
        KOI8R,
        CP866
    }

}
