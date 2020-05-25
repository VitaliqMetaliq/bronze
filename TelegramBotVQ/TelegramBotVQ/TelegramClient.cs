using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Diagnostics;
using System.IO;
using Telegram.Bot;
using System.Collections.ObjectModel;
using Newtonsoft.Json.Linq;

namespace TelegramBotVQ
{
    class TelegramClient
    {
        /// <summary>
        /// Окно программы
        /// </summary>
        private MainWindow window;
        /// <summary>
        /// Экземпляр класса TelegramBotClient
        /// </summary>
        private TelegramBotClient myBot;
        /// <summary>
        /// Массив сообщений
        /// </summary>
        public ObservableCollection<MessageLog> Logs { get; set; }
        /// <summary>
        /// Словарь областных центров
        /// </summary>
        public Dictionary<string, string> CitiesDB { get; set; }

        #region PROXY variables
        //private WebProxy proxy;
        //private HttpClientHandler httpClientHandler;
        //private HttpClient hc;
        #endregion

        public TelegramClient(MainWindow w)
        {
            this.window = w;

            #region PROXY INIT
            //proxy = new WebProxy() { Address = new Uri($"http://77.87.240.74:3128"), UseDefaultCredentials = false };
            //httpClientHandler = new HttpClientHandler() { Proxy = proxy };
            //hc = new HttpClient(httpClientHandler);
            #endregion

            //this.myBot = new TelegramBotClient(File.ReadAllText(@"D:\Skillbox\My HomeWorks\token\token.txt"), hc);
            this.myBot = new TelegramBotClient(File.ReadAllText(@"D:\Skillbox\My HomeWorks\token\token.txt"));
            Logs = new ObservableCollection<MessageLog>();
            CitiesDB = new Dictionary<string, string>();
            LoadCities(@"CitiesDB.csv");
            myBot.OnMessage += MyBot_OnMessage;
            myBot.StartReceiving();
        }

        private void MyBot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            //string text = $"{DateTime.Now.ToShortDateString()}: {e.Message.Chat.FirstName} {e.Message.Chat.Id} {e.Message.Text}";
            //Debug.WriteLine($"{text} Тип сообщения: {e.Message.Type.ToString()}");
            //if (e.Message.Text == null) return;
            //else if(e.Message.Text == "Привет")
            //{
            //    myBot.SendTextMessageAsync(e.Message.Chat.Id, $"Здравствуй, {e.Message.Chat.FirstName}");
            //}
            //else if(e.Message.Text == "Погода")
            //{
            //    GetWeather(e.Message.Chat.Id);
            //}
            //else if(e.Message.Text == "Дата")
            //{
            //    myBot.SendTextMessageAsync(e.Message.Chat.Id, $"Сегодня {DateTime.Now.ToLongDateString()}");
            //}
            //else myBot.SendTextMessageAsync(e.Message.Chat.Id, $"Доступные команды: \n1.Привет\n2.Дата\n3.Погода");

            if (e.Message.Text == null) return;
            else if (e.Message.Text == "/start")
            {
                myBot.SendTextMessageAsync(e.Message.Chat.Id, $"Здравствуй, {e.Message.Chat.FirstName}.\nВведи название города и получи\nинформацию о погоде");
            }
            else
            {
                bool flag = false;
                foreach (KeyValuePair<string, string> pair in CitiesDB)
                {
                    if (e.Message.Text == pair.Key)
                    {
                        GetWeather(e.Message.Chat.Id, pair.Key, pair.Value);
                        flag = true;
                        break;
                    }
                }
                if (flag == false) myBot.SendTextMessageAsync(e.Message.Chat.Id, $"Указанный город отсутствует в базе\nили не существует");
            }
            var messageText = e.Message.Text;

            window.Dispatcher.Invoke(() =>
            {
                Logs.Add(new MessageLog(e.Message.Chat.FirstName, e.Message.Chat.Id, messageText, DateTime.Now));
            });
        }
        /// <summary>
        /// Метод получения информации о погоде для указанного города
        /// </summary>
        /// <param name="id"></param>
        /// <param name="city"></param>
        public void GetWeather(long id, string city, string cityTranslite)
        {
            WebClient wc = new WebClient() { Encoding = Encoding.UTF8 };
            string url = $@"https://api.openweathermap.org/data/2.5/weather?q={cityTranslite}&APPID=ddd41ff292298b061dbb04d2339c1d8c&units=metric";
            var r = wc.DownloadString(url);
            dynamic TempInfo = JObject.Parse(r)["main"].ToArray();
            dynamic WindInfo = JObject.Parse(r)["wind"].ToArray();
            myBot.SendTextMessageAsync(id, $"В городе {city} {TempInfo[0].First} градусов Цельсия\nПо ощущениям {Convert.ToInt32(TempInfo[1].First)}\nВетер {WindInfo[0].First} м/с");
        }

        //public void SendMessage()
        //{

        //}

        /// <summary>
        /// Метод загрузки городов в словарь
        /// </summary>
        /// <param name="path">Путь к .csv файлу</param>
        public void LoadCities(string path)
        {
            using(StreamReader sr = new StreamReader(path))
            {
                while(!sr.EndOfStream)
                {
                    string[] args = sr.ReadLine().Split('\t');
                    this.CitiesDB.Add(args[0], args[1]);
                }
            }
        }
    }
}
