using AutoMapper;
using CoreExcelFileUploadAndRead.Database.Entities;
using CoreExcelFileUploadAndRead.Database;
using Microsoft.EntityFrameworkCore;

namespace CoreExcelFileUploadAndRead.Models
{
    public class ExcelFileLoader
    {
        private DatabaseContext databaseContext;

        public ExcelFileLoader(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task<ExcelFile> LoadFileInfoAsync(int fileID)
        {
            return await databaseContext.Files.Where(x => x.Id == fileID).FirstAsync();
        }

        public async Task<List<Class>> LoadFileClassesAsync(int fileID)
        {
            List<Class> classes = await databaseContext.Classes
                .Join(
                    databaseContext.FileDatas,
                    cls => cls.Id,
                    fds => fds.ClassId,
                    (cls, fds) => cls
                ).Distinct().ToListAsync();

            return classes;
        }

        public async Task<List<ClassGroup>> LoadFileClassGroupsAsync(int fileID)
        {
            List<ClassGroup> classGroups = await databaseContext.ClassGroups
                .Join(
                    databaseContext.FileDatas,
                    cgs => cgs.Id,
                    fds => fds.ClassGroupId,
                    (cgs, fds) => cgs
                ).Distinct().ToListAsync();

            return classGroups;
        }

        public async Task<List<BalanceAccount>> LoadFileBalanceAccountsAsync(int fileID)
        {
            List<BalanceAccount> balanceAccounts = await databaseContext.BalanceAccounts
                .Join(
                    databaseContext.FileDatas,
                    bas => bas.Id,
                    fds => fds.BalanceAccountId,
                    (bas, fds) => bas
                ).ToListAsync();

            return balanceAccounts;
        }
    }
}
