using AutoMapper;
using CoreExcelFileUploadAndRead.Database;
using CoreExcelFileUploadAndRead.Database.Entities;
using ExcelDataReader;
using Microsoft.EntityFrameworkCore;

namespace CoreExcelFileUploadAndRead.Services
{
    public class ExcelFileUploader
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IMapper _mapper;

        public ExcelFileUploader(DatabaseContext databaseContext, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
        }

        //getting all uploaded files from the db
        public async Task<List<ExcelFile>> LoadUploadedFilesListAsync()
        {
            return await _databaseContext.Files.ToListAsync();
        }

        //reading file line by line
        public async Task<bool> GetExcelFileDataAsync(string fName)
        {
            string fileName = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files"}" + "\\" + fName;

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (FileStream stream = File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                {
                    //reading and uploading file info including its header
                    ExcelFile file = GetExcelReportInfo(reader, fName);
                    ExcelFile currentFile = await AddFileInfoAsync(file);
                    FileClass currentClass = new FileClass();

                    while (reader.Read())
                    {
                        //if line first cell is empty - skip 
                        if (reader.GetValue(0) == null)
                        {
                            continue;
                        }

                        string rowFirstValue = reader.GetValue(0).ToString();

                        //identifying row type by its first cell content value
                        if (rowFirstValue.Length != 4)
                        {
                            if (rowFirstValue.Length == 2)
                            {
                                await AddClassGroupAsync(reader, currentFile.Id);
                                continue;
                            }

                            switch (rowFirstValue)
                            {
                                case "ПО КЛАССУ":
                                    await UpdateClassValuesAsync(reader, currentClass.Id);
                                    continue;
                                case "БАЛАНС":
                                    await UpdateFileValuesAsync(reader, currentFile.Id);
                                    continue;
                                default:
                                    currentClass = await AddClassAsync(reader);
                                    continue;
                            }
                        }

                        await AddBalanceAccountValues(reader, currentFile, currentClass);
                    }
                }
            }

            return true;
        }

        //reading file header by template
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

        public async Task<ExcelFile> AddFileInfoAsync(ExcelFile file)
        {
            _databaseContext.Files.Add(file);
            await _databaseContext.SaveChangesAsync();

            return file;
        }

        //adding new class group and updating fields that are included in this group
        public async Task AddClassGroupAsync(IExcelDataReader reader, int currentFileID)
        {
            ClassGroup classGroup = _mapper.Map<ClassGroup>(reader);

            _databaseContext.ClassGroups.Add(classGroup);
            await _databaseContext.SaveChangesAsync();

            string classGroupNum = classGroup.Number.ToString();

            await _databaseContext.FileDatas
                .Include(x => x.BalanceAccount)
                .Where(x => x.BalanceAccount.Number.ToString().Substring(0, 2) == classGroupNum
                    && x.ExcelFile.Id == currentFileID)
                .ForEachAsync(value => {
                    value.ClassGroup = classGroup;
                });

            await _databaseContext.SaveChangesAsync();
        }

        public async Task<FileClass> AddClassAsync(IExcelDataReader reader)
        {
            FileClass cls = _mapper.Map<FileClass>(reader);

            _databaseContext.Classes.Add(cls);
            await _databaseContext.SaveChangesAsync();

            return cls;
        }

        public async Task UpdateClassValuesAsync(IExcelDataReader reader, int currentClassID)
        {
            FileClass updatedCls = _mapper.Map<FileClass>(reader);

            FileClass cls = await _databaseContext.Classes.FindAsync(currentClassID);
            cls.OpeningBalanceActive = updatedCls.OpeningBalanceActive;
            cls.OpeningBalancePassive = updatedCls.OpeningBalancePassive;
            cls.TurnoverDebit = updatedCls.TurnoverDebit;
            cls.TurnoverCredit = updatedCls.TurnoverCredit;
            cls.ClosingBalanceActive = updatedCls.ClosingBalanceActive;
            cls.ClosingBalancePassive = updatedCls.ClosingBalancePassive;

            await _databaseContext.SaveChangesAsync();
        }

        public async Task UpdateFileValuesAsync(IExcelDataReader reader, int currentFileID)
        {
            ExcelFile updatedFile = _mapper.Map<ExcelFile>(reader);

            ExcelFile file = await _databaseContext.Files.FindAsync(currentFileID);
            file.OpeningBalanceActive = updatedFile.OpeningBalanceActive;
            file.OpeningBalancePassive = updatedFile.OpeningBalancePassive;
            file.TurnoverDebit = updatedFile.TurnoverDebit;
            file.TurnoverCredit = updatedFile.TurnoverCredit;
            file.ClosingBalanceActive = updatedFile.ClosingBalanceActive;
            file.ClosingBalancePassive = updatedFile.ClosingBalancePassive;

            await _databaseContext.SaveChangesAsync();
        }

        public async Task AddBalanceAccountValues(IExcelDataReader reader, ExcelFile currentFile, FileClass currentClass)
        {
            FileData dataRow = new FileData()
            {
                Class = currentClass,
                ExcelFile = currentFile
            };

            _databaseContext.FileDatas.Add(dataRow);
            await _databaseContext.SaveChangesAsync();

            BalanceAccount balance = _mapper.Map<BalanceAccount>(reader);
            balance.FileData = dataRow;

            _databaseContext.BalanceAccounts.Add(balance);
            await _databaseContext.SaveChangesAsync();
        }
    }
}
