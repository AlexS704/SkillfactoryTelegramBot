﻿using VoiceTexterBot.Configuration;
using Telegram.Bot;


namespace VoiceTexterBot.Services
{
    public class AudioFileHandler : IFileHandler
    {
        private readonly AppSettings _appSettings;
        private readonly ITelegramBotClient _telegramBotClient;

        public AudioFileHandler(ITelegramBotClient telegramBotClient, AppSettings appSettings)
        {
            _telegramBotClient = telegramBotClient;
            _appSettings = appSettings;
        }

        public async Task Download(string fileId, CancellationToken ct)
        {
            //Генерируем полный путь файла из конфигурации
            string inputAudioFilePath = Path.Combine(_appSettings.DownloadsFolder,
                $"{_appSettings.AudioFileName}.{_appSettings.InputAudioFormat}");

            using (FileStream destinationStream = File.Create(inputAudioFilePath))
            {
                //Загружаем информацию о файле
                var file = await
                    _telegramBotClient.GetFile(fileId, ct);
                if (file.FilePath == null)
                    return;

                //Скачивание файла
                await
                    _telegramBotClient.DownloadFile(file.FilePath,
                    destinationStream, ct);
            }

            //using (FileStream destinationStream = File.Create(inputAudioFilePath))
            //{
            //    await _telegramBotClient.DownloadFile(fileId, destinationStream, ct);
            //}
        }

        public string Process(string languageCode)
        {
            //Метод пока не реализован
            throw new NotImplementedException();


        }
    }
}
