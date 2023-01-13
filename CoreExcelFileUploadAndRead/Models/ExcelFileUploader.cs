using AutoMapper;
using CoreExcelFileUploadAndRead.Database;
using CoreExcelFileUploadAndRead.Database.Entities;
using ExcelDataReader;
using Microsoft.EntityFrameworkCore;

namespace CoreExcelFileUploadAndRead.Models
{
    public class ExcelFileUploader
    {
        private DatabaseContext databaseContext;
        private IMapper mapper;

        public ExcelFileUploader(DatabaseContext databaseContext, IMapper mapper)
        {
            this.databaseContext = databaseContext;
            this.mapper = mapper;  
        }

        public async Task<List<ExcelFile>> LoadUploadedFilesListAsync()
        {
            return await databaseContext.Files.ToListAsync();
        }

        public async Task<bool> GetExcelFileDataAsync(string fName)
        {
            string fileName = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files"}" + "\\" + fName;

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (FileStream stream = File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                {
                    ExcelFile file = GetExcelReportInfo(reader, fName);
                    int currentFileID = await AddFileInfoAsync(file);
                    int currentClassID = -1;

                    while (reader.Read())
                    {
                        if (reader.GetValue(0) == null)
                        {
                            continue;
                        }

                        string rowFirstValue = reader.GetValue(0).ToString();

                        if (rowFirstValue.Length != 4)
                        {
                            if (rowFirstValue.Length == 2)
                            {
                                await AddClassGroupAsync(reader);
                                continue;
                            }

                            switch (rowFirstValue)
                            {
                                case "ПО КЛАССУ":
                                    await UpdateClassValuesAsync(reader, currentClassID);
                                    continue;
                                case "БАЛАНС":
                                    await UpdateFileValuesAsync(reader, currentFileID);
                                    continue;
                                default:
                                    currentClassID = await AddClassAsync(reader);
                                    continue;
                            }
                        }

                        await AddBalanceAccountValues(reader, currentFileID, currentClassID);
                    }
                }
            }

            return true;
        }

        public ExcelFile GetExcelReportInfo(IExcelDataReader reader, string fileName)
        {
            ExcelFile fileInfo = new ExcelFile();
            fileInfo.Name = fileName;

            int rowCounter = 0;

            while (reader.Read() && rowCounter < 7)
            {
                switch (rowCounter)
                {
                    case 0:
                        fileInfo.BankName = reader.GetString(0);
                        break;
                    case 1:
                        fileInfo.Title = reader.GetString(0);
                        break;
                    case 2:
                        fileInfo.PeriodStart = GetDate(reader.GetString(0).Substring(12, 10));
                        fileInfo.PeriodEnd = GetDate(reader.GetString(0).Substring(26, 10));
                        break;
                    case 3:
                        fileInfo.Subject = reader.GetString(0);
                        break;
                    case 5:
                        fileInfo.PrintTime = reader.GetDateTime(0);
                        fileInfo.Currency = reader.GetString(6);
                        break;
                }

                rowCounter++;
            }

            return fileInfo;
        }

        private DateTime GetDate(string date)
        {
            return DateTime.ParseExact(date, "dd.MM.yyyy",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None);
        }

        public async Task<int> AddFileInfoAsync(ExcelFile file)
        {
            databaseContext.Files.Add(file);
            await databaseContext.SaveChangesAsync();

            return file.Id;
        }

        public async Task AddClassGroupAsync(IExcelDataReader reader)
        {
            ClassGroup classGroup = mapper.Map<ClassGroup>(reader);

            databaseContext.ClassGroups.Add(classGroup);
            await databaseContext.SaveChangesAsync();
        }

        public async Task<int> AddClassAsync(IExcelDataReader reader)
        {
            Class cls = mapper.Map<Class>(reader);

            databaseContext.Classes.Add(cls);
            await databaseContext.SaveChangesAsync();

            return cls.Id;
        }

        public async Task UpdateClassValuesAsync(IExcelDataReader reader, int currentClassID)
        {
            Class cls = await databaseContext.Classes.Where(x => x.Id == currentClassID).FirstAsync();
            
            cls.OpeningBalanceActive = mapper.Map<Class>(reader).OpeningBalanceActive;
            cls.OpeningBalancePassive = mapper.Map<Class>(reader).OpeningBalancePassive;
            cls.TurnoverDebit = mapper.Map<Class>(reader).TurnoverDebit;
            cls.TurnoverCredit = mapper.Map<Class>(reader).TurnoverCredit;
            cls.ClosingBalanceActive = mapper.Map<Class>(reader).ClosingBalanceActive;
            cls.ClosingBalancePassive = mapper.Map<Class>(reader).ClosingBalancePassive;

            await databaseContext.SaveChangesAsync();
        }

        public async Task UpdateFileValuesAsync(IExcelDataReader reader, int currentFileID)
        {
            ExcelFile file = await databaseContext.Files.Where(x => x.Id == currentFileID).FirstAsync();

            file.OpeningBalanceActive = mapper.Map<ExcelFile>(reader).OpeningBalanceActive;
            file.OpeningBalancePassive = mapper.Map<ExcelFile>(reader).OpeningBalancePassive;
            file.TurnoverDebit = mapper.Map<ExcelFile>(reader).TurnoverDebit;
            file.TurnoverCredit = mapper.Map<ExcelFile>(reader).TurnoverCredit;
            file.ClosingBalanceActive = mapper.Map<ExcelFile>(reader).ClosingBalanceActive;
            file.ClosingBalancePassive = mapper.Map<ExcelFile>(reader).ClosingBalancePassive;

            await databaseContext.SaveChangesAsync();
        }

        public async Task AddBalanceAccountValues(IExcelDataReader reader, int currentFileID, int currentClassID)
        {
            BalanceAccount balance = mapper.Map<BalanceAccount>(reader);

            databaseContext.BalanceAccounts.Add(balance);
            await databaseContext.SaveChangesAsync();

            databaseContext.FileDatas.Add(new FileData() 
            { 
                BalanceAccountId = balance.Id, 
                ClassId = currentClassID,
                FileId = currentFileID
            });
            await databaseContext.SaveChangesAsync();
        }
    }
}
