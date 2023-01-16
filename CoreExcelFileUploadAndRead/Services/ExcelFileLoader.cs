using CoreExcelFileUploadAndRead.Database;
using CoreExcelFileUploadAndRead.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoreExcelFileUploadAndRead.Services
{
    public class ExcelFileLoader
    {
        private readonly DatabaseContext _databaseContext;

        public ExcelFileLoader(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<ExcelFile?> LoadFileInfoAsync(int fileID)
        {
            return await _databaseContext.Files.FindAsync(fileID);
        }

        public async Task<List<FileClass>> LoadFileClassesAsync(int fileID)
        {
            List<FileClass> classes = await _databaseContext.Classes
                .Include(x => x.FileDatas)
                .Where(x => x.FileDatas.First().ExcelFileId == fileID)
                .OrderBy(x => x.Title)
                .ToListAsync();

            return classes;
        }

        public async Task<List<ClassGroup>> LoadFileClassGroupsAsync(int fileID)
        {
            List<ClassGroup> classGroups = await _databaseContext.ClassGroups
                .Include(x => x.FileDatas)
                .Where(x => x.FileDatas.First().ExcelFileId == fileID)
                .ToListAsync();

            return classGroups;
        }

        public async Task<List<BalanceAccount>> LoadFileBalanceAccountsAsync(int fileID)
        {
            List<BalanceAccount> balanceAccounts = await _databaseContext.BalanceAccounts
                .Include(x => x.FileData)
                .Where(x => x.FileData.ExcelFileId == fileID)
                .OrderBy(x => x.Number)
                .ToListAsync();

            return balanceAccounts;
        }
    }
}
