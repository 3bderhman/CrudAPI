using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudAPI.BL.Helper
{
    public static class Files
    {
        public static string UpbloadFile(IFormFile File, string FolderName)
        {
            string FolderPath = Path.Combine(Directory.GetCurrentDirectory(), @"Files", FolderName);
            string FileName = Guid.NewGuid() + Path.GetFileName(File.FileName);
            string FinalPath = Path.Combine(FolderPath, FileName);
            using(var stream = new FileStream(FinalPath, FileMode.Create))
            {   
                File.CopyTo(stream);
            }
            return FileName;
        }
        public static string DeleteFile(string FolderName, string FileName)
        {
            var CurrentDirectiory = Path.Combine(Directory.GetCurrentDirectory(), @"Files", FolderName, FileName);
            if (File.Exists(CurrentDirectiory))
            {
                File.Delete(CurrentDirectiory);
            }
            return "Done";
        }
    }
}
